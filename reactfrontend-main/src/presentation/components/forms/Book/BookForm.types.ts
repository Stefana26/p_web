import { FormController } from "../FormController";
import {
    UseFormHandleSubmit,
    UseFormRegister,
    FieldErrorsImpl,
    DeepRequired,
    UseFormGetValues
} from "react-hook-form";
import { SelectChangeEvent } from "@mui/material";

export type BookFormModel = {
    title: string;
    ISBN: string;
    author: string;
    genre: string;
    description: string;
    pages: number;
};

export type BookFormState = {
    errors: FieldErrorsImpl<DeepRequired<BookFormModel>>;
    values: BookFormModel;
};

export type BookFormActions = {
    register: UseFormRegister<BookFormModel>;
    handleSubmit: UseFormHandleSubmit<BookFormModel>;
    getValues: UseFormGetValues<BookFormModel>;
    submit: (body: BookFormModel) => void;
    //selectRole: (value: SelectChangeEvent<UserRoleEnum>) => void;
};
export type BookFormComputed = {
    defaultValues: BookFormModel,
    isSubmitting: boolean
};

export type BookFormController = FormController<BookFormState, BookFormActions, BookFormComputed>;