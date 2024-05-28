import {
    Button,
    CircularProgress,
    FormControl,
    FormHelperText,
    FormLabel,
    Grid,
    Stack,
    OutlinedInput
} from "@mui/material";
import { FormattedMessage, useIntl } from "react-intl";
import { useAuthorFormController } from "./AuthorForm.controller";
import { ContentCard } from "@presentation/components/ui/ContentCard";
import { isEmpty, isUndefined } from "lodash";
import { AuthorFormModel } from "./AuthorForm.types";

/**
 * Here we declare the register form component.
 */
type onSubmitType = () => void;


export const AuthorForm: React.FC<{ onSubmit: onSubmitType; initialData?: AuthorFormModel }> = ({ onSubmit, initialData }) => {
    const { formatMessage } = useIntl();
    const isUpdating = initialData !== undefined;
    console.log(initialData);
    const { state, actions, computed } = useAuthorFormController(onSubmit, initialData, isUpdating)// Use the controller.

    return <form onSubmit={actions.handleSubmit(actions.submit)}> {/* Wrap your form into a form tag and use the handle submit callback to validate the form and call the data submission. */}
        <Stack spacing={4} style={{ width: "100%" }}>
            <ContentCard title={formatMessage({ id: "globals.name" })}>
                {/* Name Field */}
                <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl 
                                fullWidth
                                error={!isUndefined(state.errors.name)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.name" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("name", { required: "Name is required" })}
                                    placeholder={formatMessage({ id: "globals.placeholders.textInput" }, { fieldName: formatMessage({ id: "globals.name" }) })}
                                />
                                <FormHelperText
                                    hidden={isUndefined(state.errors.name)}
                                >
                                    {state.errors.name?.message}
                                </FormHelperText>
                            </FormControl>
                     </Grid>

                     <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl 
                                fullWidth
                                error={!isUndefined(state.errors.nationality)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.nationality" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("nationality", { required: "Nationality is required" })}
                                    placeholder={formatMessage({ id: "globals.placeholders.textInput" }, { fieldName: formatMessage({ id: "globals.nationality" }) })}
                                />
                                <FormHelperText
                                    hidden={isUndefined(state.errors.nationality)}
                                >
                                    {state.errors.nationality?.message}
                                </FormHelperText>
                            </FormControl>
                    </Grid>
                    
                    <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl 
                                fullWidth
                                error={!isUndefined(state.errors.dateOfBirth)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.dateOfBirth" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("dateOfBirth", { required: "Date of Birth is required" })}
                                    placeholder={formatMessage({ id: "globals.placeholders.textInput" }, { fieldName: formatMessage({ id: "globals.dateOfBirth" }) })}
                                />
                                <FormHelperText
                                    hidden={isUndefined(state.errors.dateOfBirth)}
                                >
                                    {state.errors.dateOfBirth?.message}
                                </FormHelperText>
                            </FormControl>
                    </Grid>

                    <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl 
                                fullWidth
                                error={!isUndefined(state.errors.biography)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.biography" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("biography", { required: "Biography is required" })}
                                    placeholder={formatMessage({ id: "globals.placeholders.textInput" }, { fieldName: formatMessage({ id: "globals.biography" }) })}
                                />
                                <FormHelperText
                                    hidden={isUndefined(state.errors.biography)}
                                >
                                    {state.errors.biography?.message}
                                </FormHelperText>
                            </FormControl>
                    </Grid>
                    
            </ContentCard>
            <Grid container item direction="row" xs={12} className="padding-top-sm">
                <Grid container item direction="column" xs={12} md={7}></Grid>
                <Grid container item direction="column" xs={5}>
                    <Button type="submit" disabled={!isEmpty(state.errors) || computed.isSubmitting}> {/* Add a button with type submit to call the submission callback if the button is a descended of the form element. */}
                        {!computed.isSubmitting && <FormattedMessage id="globals.submit" />}
                        {computed.isSubmitting && <CircularProgress />}
                    </Button>
                </Grid>
            </Grid>
        </Stack>
    </form>
};