import { FeedbackFormController, FeedbackFormModel } from "./FeedbackForm.types";
import { yupResolver } from "@hookform/resolvers/yup";
import { useIntl } from "react-intl";
import * as yup from "yup";
import { isUndefined } from "lodash";
import { useForm } from "react-hook-form";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useUserApi } from "@infrastructure/apis/api-management";
import { useFeedbackApi } from "@infrastructure/apis/api-management/feedback";
import { useCallback, useEffect } from "react";
import { SelectChangeEvent } from "@mui/material";
import { useAppRouter } from "@infrastructure/hooks/useAppRouter";
//import { AmountEnum } from "@infrastructure/apis/client";

/**
 * Use a function to return the default values of the form and the validation schema.
 * You can add other values as the default, for example when populating the form with data to update an entity in the backend.
 */
const getDefaultValues = (initialData?: FeedbackFormModel) => {
    const defaultValues = {
        q1: "",
        q2: "0",
        q3: "",
        q4: "false"
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
const useInitFeedbackForm = () => {
    const { formatMessage } = useIntl();
    const defaultValues = getDefaultValues();

    const schema = yup.object().shape({
        q1: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.q1",
                    }),
                }))
            .default(defaultValues.q1),
        q2: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.q2",
                    }),
                }))
            .default(defaultValues.q2),
        q3: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.q3",
                    }),
                }))
            .default(defaultValues.q3)
    });

    const resolver = yupResolver(schema);

    return { defaultValues, resolver };
}

/**
 * Create a controller hook for the form and return any data that is necessary for the form.
 */
export const useFeedbackFormController = (onSubmit?: () => void): FeedbackFormController => {
    const { defaultValues, resolver } = useInitFeedbackForm();
    //const { redirectToLogin } = useAppRouter();
    const { addFeedback: { mutation, key: mutationKey } } = useFeedbackApi();
    const { mutateAsync: add, status } = useMutation({
        mutationKey: [mutationKey],
        mutationFn: mutation
    });
    const queryClient = useQueryClient();
    const submit = useCallback((data: FeedbackFormModel) => // Create a submit callback to send the form data to the backend.
    add(data).then(() => {
        queryClient.invalidateQueries({ queryKey: [mutationKey] }); // If the form submission succeeds then some other queries need to be refreshed so invalidate them to do a refresh.

        if (onSubmit) {
            onSubmit();
        }
    }), [add, queryClient, mutationKey, onSubmit]);

    const {
        register,
        handleSubmit,
        watch,
        setValue,
        formState: { errors }
    } = useForm<FeedbackFormModel>({ // Use the useForm hook to get callbacks and variables to work with the form.
        defaultValues, // Initialize the form with the default values.
        resolver // Add the validation resolver.
    });


    const selectRating = useCallback((newValue: number) => { // Update the type of newValue to number
        console.log(newValue);
        setValue("q2", newValue.toString(), {
            shouldValidate: true,
        });
        //console.log(event.target.value);
    }, [setValue]);


    return {
        actions: {
            handleSubmit, // Add the form submit handle with the correct type.
            submit, // Add the submit handle that needs to be passed to the submit handle.
            register, // Add the variable register to bind the form fields in the UI with the form variables.
            watch, // Add a watch on the variables, this function can be used to watch changes on variables if it is needed in some locations.
            selectRating,
            setValue
        },
        computed: {
            defaultValues,
            isSubmitting: status === "pending" // Return if the form is still submitting or nit.
        },
        state: {
            errors // Return what errors have occurred when validating the form input.
        }
    }
}