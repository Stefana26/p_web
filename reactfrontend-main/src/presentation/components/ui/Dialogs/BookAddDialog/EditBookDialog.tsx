import React, { useState, useEffect } from 'react';
import { Dialog, DialogTitle, DialogContent, DialogActions, Button, TextField } from '@mui/material';
import { BookUpdateDTO, BookDTO } from '@infrastructure/apis/client';
import { useBookAddDialogController } from "./BookAddDialog.controller";
import { BookForm } from "@presentation/components/forms/Book/BookForm"; 
import { useIntl } from "react-intl";
import { useDialogController } from '../Dialog.controller';
import EditIcon from '@mui/icons-material/Edit';
const EditBookDialog: React.FC<{ initialData: BookDTO }> = ({ initialData }) => {
    console.log('ce naiba');
   
    const { close, isOpen, open } = useDialogController(); // Destructure open, close, and isOpen from the controller
    console.log(isOpen);
    return (
        <div>
            <EditIcon onClick={open} /> {/* Use EditIcon to trigger dialog */}
            <Dialog open={isOpen} onClose={close}>
                <DialogContent>
                    <BookForm onSubmit={close} initialData={initialData}/>
                </DialogContent>
            </Dialog>
        </div>
    );
};

export default EditBookDialog;

