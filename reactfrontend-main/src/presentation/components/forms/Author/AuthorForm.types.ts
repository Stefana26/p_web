import { FormController } from "../FormController";
import {
    UseFormHandleSubmit,
    UseFormRegister,
    FieldErrorsImpl,
    DeepRequired,
    UseFormGetValues
} from "react-hook-form";
import { SelectChangeEvent } from "@mui/material";

export type AuthorFormModel = {
    id?: string;
    name: string;
    nationality: string;
    dateOfBirth: string;
    biography: string;
};

export type AuthorFormState = {
    errors: FieldErrorsImpl<DeepRequired<AuthorFormModel>>;
    values: AuthorFormModel;
};

export type AuthorFormActions = {
    register: UseFormRegister<AuthorFormModel>;
    handleSubmit: UseFormHandleSubmit<AuthorFormModel>;
    getValues: UseFormGetValues<AuthorFormModel>;
    submit: (body: AuthorFormModel) => void;
};

export type AuthorFormComputed = {
    defaultValues: AuthorFormModel,
    isSubmitting: boolean
};

export type AuthorFormController = FormController<AuthorFormState, AuthorFormActions, AuthorFormComputed>;