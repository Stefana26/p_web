import React, { useState, useEffect } from 'react';
import { Dialog, DialogTitle, DialogContent, DialogActions, Button, TextField } from '@mui/material';
import { AuthorUpdateDTO, AuthorDTO } from '@infrastructure/apis/client';
import { useAuthorAddDialogController } from "./AuthorAddDialog.controller";
import { AuthorForm } from "@presentation/components/forms/Author/AuthorForm"; 
import { useIntl } from "react-intl";
import { useDialogController } from '../Dialog.controller';
import EditIcon from '@mui/icons-material/Edit';
const EditAuthorDialog: React.FC<{ initialData: AuthorDTO }> = ({ initialData }) => {
    const { close, isOpen, open } = useDialogController(); // Destructure open, close, and isOpen from the controller
    return (
        <div>
            <EditIcon onClick={open} /> {/* Use EditIcon to trigger dialog */}
            <Dialog open={isOpen} onClose={close}>
                <DialogContent>
                    <AuthorForm onSubmit={close} initialData={initialData}/>
                </DialogContent>
            </Dialog>
        </div>
    );
};

export default EditAuthorDialog;
