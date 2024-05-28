import { BookFormController, BookFormModel } from "./BookForm.types";
import { yupResolver } from "@hookform/resolvers/yup";
import { useIntl } from "react-intl";
import * as yup from "yup";
import { isUndefined } from "lodash";
import { useForm } from "react-hook-form";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useBookApi } from "@infrastructure/apis/api-management";
import { useCallback } from "react";
import { useAppRouter } from "@infrastructure/hooks/useAppRouter";
import { useAppDispatch } from "@application/store";
import { setToken } from "@application/state-slices";
import { UserRoleEnum } from "@infrastructure/apis/client";
import { SelectChangeEvent } from "@mui/material";
import { toast } from "react-toastify";

/**
 * Use a function to return the default values of the form and the validation schema.
 * You can add other values as the default, for example when populating the form with data to update an entity in the backend.
 */
const getDefaultValues = (initialData?: { title: string }) => {
    const defaultValues = {
        title: "",
        ISBN: "",
        author: "",
        genre: "",
        description: "",
        pages: 0
    };

    if (!isUndefined(initialData)) {
        return {
            ...defaultValues,
            ...initialData,
        };
    }

    return defaultValues;
};

/**
 * Create a hook to get the validation schema.
 */
const useInitBookForm = () => {
    const { formatMessage } = useIntl();
    const defaultValues = getDefaultValues();

    const schema = yup.object().shape({ // Use yup to build the validation schema of the form.
        title: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({ id: "globals.title" }),
                }))
            .default(defaultValues.title),
        ISBN: yup.string()
        .required(formatMessage(
            { id: "globals.validations.requiredField" },
            {
                fieldName: formatMessage({ id: "globals.ISBN" }),
            }))
        .default(defaultValues.ISBN),
        author: yup.string() // This field should be a string.
            .required(formatMessage( // Use formatMessage to get the translated error message.
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({ // Format the message with other translated strings.
                        id: "globals.author",
                    }),
                })) // The field is required and needs a error message when it is empty.
            .default(defaultValues.author), // Add a default value for the field.
        genre: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.genre",
                    }),
                }))
            .default(defaultValues.genre),
        description: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.description",
                    }),
                }))
            .default(defaultValues.description),
        pages: yup.number()
        .required(formatMessage(
            { id: "globals.validations.requiredField" },
            {
                fieldName: formatMessage({
                    id: "globals.pages",
                }),
            }))
        .default(defaultValues.pages)
    });

    const resolver = yupResolver(schema); // Get the resolver.

    return { defaultValues, resolver }; // Return the default values and the resolver.
}

/**
 * Create a controller hook for the form and return any data that is necessary for the form.
 */
export const useBookFormController = (onSubmit?: () => void, initialData?: BookFormModel, isUpdating?: boolean): BookFormController => {
    const { formatMessage } = useIntl();
    const { defaultValues, resolver } = useInitBookForm();

    const { addBook: { mutation: addMutation, key: addMutationKey }, updateBook: { mutation: updateMutation, key: updateMutationKey } } = useBookApi();
    const queryClient = useQueryClient();
    const { mutateAsync: add, status: addStatus } = useMutation({
        mutationKey: [addMutationKey],
        mutationFn: addMutation,
        onSuccess: () => {
            if (onSubmit) onSubmit(); // Call onSubmit callback if provided
            toast.success('Book registered successfully');
        },
        onError: (error) => {
            console.log(error);
            toast.error(`Registration failed: ${error.message}`);
        }
    });

    const { mutateAsync: update, status: updateStatus } = useMutation({
        mutationKey: [updateMutationKey],
        mutationFn: updateMutation,
        onSuccess: () => {
            if (onSubmit) onSubmit(); // Call onSubmit callback if provided
            toast.success('Book updated successfully');
        },
        onError: (error) => {
            toast.error(`Update failed: ${error.message}`);
        }
    });

    const submit = useCallback(async (data: BookFormModel) => {
        try {
            if (isUpdating) {
                // If data contains an id, it means we're updating an existing book
                await update(data);
            } else {
                // If data does not contain an id, it means we're adding a new book
                await add(data);
            }
            //queryClient.invalidateQueries(addMutationKey); // Invalidate query to refresh data
        } catch (error) {
            // Error handling
        }
    }, [add, update, queryClient, addMutationKey]);

    const {
        register,
        handleSubmit,
        formState: { errors },
        getValues,
    } = useForm<BookFormModel>({
        defaultValues,
        resolver
    });

    return {
        actions: {
            handleSubmit,
            register,
            getValues,
            submit
        },
        computed: {
            defaultValues,
            isSubmitting: addStatus === 'pending' || updateStatus === 'pending'
        },
        state: {
            errors,
            values: getValues()
        }
    };
};
