import { useIntl } from "react-intl";
import { isUndefined } from "lodash";
import { IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TablePagination, TableRow, TextField } from "@mui/material";
import { DataLoadingContainer } from "../../LoadingDisplay";
import { useBookTableController } from "./BookTable.controller";
import { BookDTO } from "@infrastructure/apis/client";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { BookAddDialog } from "../../Dialogs/BookAddDialog";
import { useAppSelector } from "@application/store";
import React, { useState } from "react";
import { BookUpdateDTO } from "@infrastructure/apis/client";
import EditBookDialog from '@presentation/components/ui/Dialogs/BookAddDialog/EditBookDialog';
import { useBookAddDialogController } from "../../Dialogs/BookAddDialog/BookAddDialog.controller";

/**
 * This hook returns a header for the table with translated columns.
 */
const useHeader = (): { key: keyof BookDTO, name: string }[] => {
    const { formatMessage } = useIntl();

    return [
        { key: "title", name: formatMessage({ id: "globals.title" }) },
        { key: "isbn", name: formatMessage({ id: "globals.ISBN" }) },
        //{ key: "genre", name: formatMessage({ id: "globals.genre" }) },
        { key: "description", name: formatMessage({ id: "globals.description" }) },
        { key: "pages", name: formatMessage({ id: "globals.pages" }) }
    ]
};

/**
 * The values in the table are organized as rows so this function takes the entries and creates the row values ordering them according to the order map.
 */
const getRowValues = (entries: BookDTO[] | null | undefined, orderMap: { [key: string]: number }) =>
    entries?.map(
        entry => {
            return {
                entry: entry,
                data: Object.entries(entry).filter(([e]) => !isUndefined(orderMap[e])).sort(([a], [b]) => orderMap[a] - orderMap[b]).map(([key, value]) => { return { key, value } })
            }
        });
/**
 * Creates the user table.
 */
export const BookTable = () => {
    const { userId: ownUserId } = useAppSelector(x => x.profileReducer);
    const { formatMessage } = useIntl();
    const header = useHeader();
    const orderMap = header.reduce((acc, e, i) => { return { ...acc, [e.key]: i } }, {}) as { [key: string]: number }; // Get the header column order.
    const { handleChangePage, handleChangePageSize, pagedData, isError, isLoading, tryReload, labelDisplay, remove, update } = useBookTableController(); // Use the controller hook.
    const rowValues = getRowValues(pagedData?.data, orderMap); // Get the row values.
    const [searchQuery, setSearchQuery] = useState('');
    const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSearchQuery(event.target.value);
    };

    // Filter book data based on search query
    const filteredRowValues = rowValues?.filter(({ entry }) =>
        (entry.title ?? '').toLowerCase().includes(searchQuery.toLowerCase())
    );


    const { open } = useBookAddDialogController();

    return <DataLoadingContainer isError={isError} isLoading={isLoading} tryReload={tryReload}> {/* Wrap the table into the loading container because data will be fetched from the backend and is not immediately available.*/}
        <BookAddDialog /> {/* Add the button to open the user add modal. */}
        <TextField // Search input field
                label="Search Books"
                value={searchQuery}
                onChange={handleSearchChange}
                fullWidth
                margin="normal"
            />
        {!isUndefined(pagedData) && !isUndefined(pagedData?.totalCount) && !isUndefined(pagedData?.page) && !isUndefined(pagedData?.pageSize) &&
            <TablePagination // Use the table pagination to add the navigation between the table pages.
                component="div"
                count={pagedData.totalCount} // Set the entry count returned from the backend.
                page={pagedData.totalCount !== 0 ? pagedData.page - 1 : 0} // Set the current page you are on.
                onPageChange={handleChangePage} // Set the callback to change the current page.
                rowsPerPage={pagedData.pageSize} // Set the current page size.
                onRowsPerPageChange={handleChangePageSize} // Set the callback to change the current page size. 
                labelRowsPerPage={formatMessage({ id: "labels.itemsPerPage" })}
                labelDisplayedRows={labelDisplay}
                showFirstButton
                showLastButton
            />}

        <TableContainer component={Paper}>
            <Table>
                <TableHead>
                    <TableRow>
                        {
                            header.map(e => <TableCell key={`header_${String(e.key)}`}>{e.name}</TableCell>) // Add the table header.
                        }
                        <TableCell>{formatMessage({ id: "labels.actions" })}</TableCell> {/* Add additional header columns if needed. */}
                    </TableRow>
                </TableHead>
                <TableBody>
                    {
                        filteredRowValues?.map(({ data, entry }, rowIndex) => <TableRow key={`row_${rowIndex + 1}`}>
                            {data.map((keyValue, index) => <TableCell key={`cell_${rowIndex + 1}_${index + 1}`}>{keyValue.value}</TableCell>)} {/* Add the row values. */}
                            <TableCell> {/* Add other cells like action buttons. */}
                                {entry.id !== ownUserId && (
                                    <>
                                        <IconButton color="error" onClick={() => remove(entry.id || '')}>
                                            <DeleteIcon color="error" fontSize='small' />
                                        </IconButton>
                                        <IconButton color="primary" onClick={open}>
                                            <EditBookDialog initialData={entry}/>
                                        </IconButton>
                                    </>
                                )}
                            </TableCell>
                        </TableRow>)
                    }
                </TableBody>
            </Table>
            
        </TableContainer>
    </DataLoadingContainer >
}