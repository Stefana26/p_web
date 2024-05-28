import {
    Button,
    CircularProgress,
    FormControl,
    FormHelperText,
    FormLabel,
    Grid,
    Stack,
    OutlinedInput,
    Select,
    MenuItem,
    Checkbox
} from "@mui/material";
import * as React from 'react';
import { FormattedMessage, useIntl } from "react-intl";
import { useFeedbackFormController } from "./FeedbackForm.controller";
import { isEmpty, isUndefined } from "lodash";
import { ContentCard } from "@presentation/components/ui/ContentCard";
import PropTypes from 'prop-types';
import { styled } from '@mui/material/styles';
import { useEffect } from "react";
import { IconContainerProps } from '@mui/material/Rating';
import Rating from '@mui/material/Rating';
import AcUnitIcon from '@mui/icons-material/AcUnit';
import BeachAccessIcon from '@mui/icons-material/BeachAccess';
import CloudIcon from '@mui/icons-material/Cloud';
import Brightness5Icon from '@mui/icons-material/Brightness5';
import WbSunnyIcon from '@mui/icons-material/WbSunny';

export const FeedbackForm = (props: { onSubmit?: () => void }) => {
    const { formatMessage } = useIntl();
    const { state, actions, computed } = useFeedbackFormController(props.onSubmit);

    

const customIcons: {
    [index: string]: {
        icon: React.ReactElement;
        label: string;
    };
} = {
    1: {
        icon: <AcUnitIcon style={{ fontSize: 40 }} />,
        label: 'Cold',
    },
    2: {
        icon: <BeachAccessIcon style={{ fontSize: 40 }} />,
        label: 'Cool',
    },
    3: {
        icon: <CloudIcon style={{ fontSize: 40 }} />,
        label: 'Neutral',
    },
    4: {
        icon: <Brightness5Icon style={{ fontSize: 40 }} />,
        label: 'Warm',
    },
    5: {
        icon: <WbSunnyIcon style={{ fontSize: 40 }} />,
        label: 'Hot',
    },
};

    function IconContainer(props: IconContainerProps) {
        const { value, ...other } = props;
        return <span {...other}>{customIcons[value].icon}</span>;
    }

    return (
        <form onSubmit={actions.handleSubmit(actions.submit)}>
            <Stack spacing={4} style={{ width: "100%" }}>
                <ContentCard>
                    <div style={{ textAlign: "center", marginBottom: "20px", fontSize: "24px", fontWeight: "bold", color: "#333" }}>
                        {formatMessage({ id: "globals.feedbackTitle" })}
                    </div>
                    <Grid container item direction="column" xs={12} columnSpacing={4}>
                        {/* TITLE */}
                        <Grid container item direction="column" xs={6} md={6}>
                            <div><br /></div>
                            <FormControl fullWidth error={!isUndefined(state.errors.q1)}>
                                <FormLabel required>
                                    <FormattedMessage id="globals.q1" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("q1")}
                                    placeholder={formatMessage({ id: "globals.placeholders.textInput" })}
                                    autoComplete="none"
                                />
                                <FormHelperText hidden={isUndefined(state.errors.q1)}>
                                    {state.errors.q1?.message}
                                </FormHelperText>
                            </FormControl>
                            <div><br /></div>
                        </Grid>
                        <div><br /></div>
                        {/* RATING */}
                        <Grid container item direction="column" xs={6} md={6}>
                            <div><br /></div>
                            <FormControl fullWidth error={!isUndefined(state.errors.q2)}>
                                <FormLabel required>
                                    <FormattedMessage id="globals.q2" />
                                </FormLabel>
                                <div style={{ display: 'flex', justifyContent: 'center' }}>
                                    <Rating
                                        name="highlight-selected-only"
                                        IconContainerComponent={IconContainer}
                                        getLabelText={(value: number) => customIcons[value].label}
                                        highlightSelectedOnly
                                        value={Number(actions.watch("q2"))}
                                        onChange={(event, newValue) => {
                                            if (newValue !== null) {
                                                actions.setValue("q2", newValue.toString(), {
                                                    shouldValidate: true,
                                                });
                                            }
                                        }}
                                    />
                                </div>
                                <FormHelperText hidden={isUndefined(state.errors.q2)}>
                                    {state.errors.q2?.message}
                                </FormHelperText>
                            </FormControl>
                            <div><br /></div>
                        </Grid>
                        <div><br /></div>
                        {/* COMMENT */}
                        <Grid container item direction="column" xs={6} md={6}>
                            <div><br /></div>
                            <FormControl fullWidth error={!isUndefined(state.errors.q3)}>
                                <FormLabel required>
                                    <FormattedMessage id="globals.q3" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("q3")}
                                    placeholder={formatMessage({ id: "globals.placeholders.textInput" })}
                                    autoComplete="none"
                                />
                                <FormHelperText hidden={isUndefined(state.errors.q3)}>
                                    {state.errors.q3?.message}
                                </FormHelperText>
                            </FormControl>
                            <div><br /></div>
                        </Grid>
                        <div><br /></div>
                       {/* CHECKBOX */}
                       <FormControl fullWidth error={!isUndefined(state.errors.q4)}>
                        <FormLabel required>
                            <FormattedMessage id="globals.q4" />
                        </FormLabel>
                        <div>
                            <Checkbox
                                {...actions.register("q4")}
                                checked={actions.watch("q4") === "true"}
                                onChange={(e) => {
                                    if (e.target.checked) {
                                        actions.setValue("q4", "true", {
                                            shouldValidate: true,
                                        });
                                    } else {
                                        actions.setValue("q4", "false", {
                                            shouldValidate: true,
                                        });
                                    }
                                }}
                            />
                            <span>{formatMessage({ id: "globals.a1" })}</span>
                        </div>
                        <div>
                            <Checkbox
                                {...actions.register("q4")}
                                checked={actions.watch("q4") === "false"}
                                onChange={(e) => {
                                    if (e.target.checked) {
                                        actions.setValue("q4", "false", {
                                            shouldValidate: true,
                                        });
                                    } else {
                                        actions.setValue("q4", "true", {
                                            shouldValidate: true,
                                        });
                                    }
                                }}
                            />
                            <span>{formatMessage({ id: "globals.a2" })}</span>
                        </div>
                        <FormHelperText hidden={isUndefined(state.errors.q4)}>
                            {state.errors.q4?.message}
                        </FormHelperText>
                    </FormControl>
                    <div><br /></div>



                    <div><br /></div>
                    <Grid container item direction="row" xs={12} className="padding-top-sm">
                        <Grid container item direction="column" xs={12} md={7}></Grid>
                        <Grid container item direction="column" xs={5}>
                            <Button type="submit" disabled={!isEmpty(state.errors) || computed.isSubmitting}>
                                {!computed.isSubmitting && <FormattedMessage id="globals.submit" />}
                                {computed.isSubmitting && <CircularProgress />}
                            </Button>
                        </Grid>
                    </Grid>
                    </Grid>
                </ContentCard>
            </Stack>
        </form>
    );
};
