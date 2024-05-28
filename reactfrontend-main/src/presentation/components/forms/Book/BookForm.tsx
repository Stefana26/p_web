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
import { useBookFormController } from "./BookForm.controller";
import { ContentCard } from "@presentation/components/ui/ContentCard";
import { isEmpty, isUndefined } from "lodash";
import { BookFormModel } from "./BookForm.types";

/**
 * Here we declare the register form component.
 */
type onSubmitType = () => void;


export const BookForm: React.FC<{ onSubmit: onSubmitType; initialData?: BookFormModel }> = ({ onSubmit, initialData }) => {
    const { formatMessage } = useIntl();
    const isUpdating = initialData !== undefined;
    const { state, actions, computed } = useBookFormController(undefined, initialData, isUpdating)// Use the controller.

    return <form onSubmit={actions.handleSubmit(actions.submit)}> {/* Wrap your form into a form tag and use the handle submit callback to validate the form and call the data submission. */}
        <Stack spacing={4} style={{ width: "100%" }}>
            <ContentCard title={formatMessage({ id: "globals.title" })}>
                {/* Name Field */}
                <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl 
                                fullWidth
                                error={!isUndefined(state.errors.title)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.title" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("title", { required: "Title is required" })}
                                    placeholder={formatMessage({ id: "globals.placeholders.textInput" }, { fieldName: formatMessage({ id: "globals.title" }) })}
                                />
                                <FormHelperText
                                    hidden={isUndefined(state.errors.title)}
                                >
                                    {state.errors.title?.message}
                                </FormHelperText>
                            </FormControl>
                     </Grid>
                     <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl 
                                fullWidth
                                error={!isUndefined(state.errors.title)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.author" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("author", { required: "Author is required" })}
                                    placeholder={formatMessage({ id: "globals.placeholders.textInput" }, { fieldName: formatMessage({ id: "globals.author" }) })}
                                />
                                <FormHelperText
                                    hidden={isUndefined(state.errors.author)}
                                >
                                    {state.errors.author?.message}
                                </FormHelperText>
                            </FormControl>
                    </Grid>
                    <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl 
                                fullWidth
                                error={!isUndefined(state.errors.title)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.ISBN" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("ISBN", { required: "ISBN is required" })}
                                    placeholder={formatMessage({ id: "globals.placeholders.textInput" }, { fieldName: formatMessage({ id: "globals.ISBN" }) })}
                                />
                                <FormHelperText
                                    hidden={isUndefined(state.errors.author)}
                                >
                                    {state.errors.author?.message}
                                </FormHelperText>
                            </FormControl>
                    </Grid>
                    <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl 
                                fullWidth
                                error={!isUndefined(state.errors.title)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.genre" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("genre", { required: "Genre is required" })}
                                    placeholder={formatMessage({ id: "globals.placeholders.textInput" }, { fieldName: formatMessage({ id: "globals.genre" }) })}
                                />
                                <FormHelperText
                                    hidden={isUndefined(state.errors.genre)}
                                >
                                    {state.errors.genre?.message}
                                </FormHelperText>
                            </FormControl>
                    </Grid>
                    
                    
                    <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl 
                                fullWidth
                                error={!isUndefined(state.errors.title)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.pages" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("pages", { required: "pages is required" })}
                                    placeholder={formatMessage({ id: "globals.placeholders.textInput" }, { fieldName: formatMessage({ id: "globals.pages" }) })}
                                />
                                <FormHelperText
                                    hidden={isUndefined(state.errors.author)}
                                >
                                    {state.errors.author?.message}
                                </FormHelperText>
                            </FormControl>
                    </Grid>

                    <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl 
                                fullWidth
                                error={!isUndefined(state.errors.title)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.description" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("description", { required: "Description is required" })}
                                    placeholder={formatMessage({ id: "globals.placeholders.textInput" }, { fieldName: formatMessage({ id: "globals.description" }) })}
                                />
                                <FormHelperText
                                    hidden={isUndefined(state.errors.description)}
                                >
                                    {state.errors.description?.message}
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