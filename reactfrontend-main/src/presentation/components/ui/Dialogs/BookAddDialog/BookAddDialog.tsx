import { Button, Dialog, DialogContent, DialogTitle } from "@mui/material";
import { useBookAddDialogController } from "./BookAddDialog.controller";
import { BookForm } from "@presentation/components/forms/Book/BookForm";
import { useIntl } from "react-intl";

/**
 * This component wraps the user add form into a modal dialog.
 */
export const BookAddDialog = () => {
  const { open, close, isOpen } = useBookAddDialogController();
  const { formatMessage } = useIntl();

  return <div>
    <Button variant="outlined" onClick={open}>
      {formatMessage({ id: "labels.addBook" })}
    </Button>
    <Dialog
      open={isOpen}
      onClose={close}>
      <DialogTitle>
        {formatMessage({ id: "labels.addBook" })}
      </DialogTitle>
      <DialogContent>
        <BookForm onSubmit={close} />
      </DialogContent>
    </Dialog>
  </div>
};