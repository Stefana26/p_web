import { useIntl } from "react-intl";
import { isUndefined } from "lodash";
import { IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TablePagination, TableRow, TextField } from "@mui/material";
import { DataLoadingContainer } from "../../LoadingDisplay";
import { useAuthorTableController } from "./AuthorTable.controller";
import { AuthorDTO } from "@infrastructure/apis/client";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { AuthorAddDialog } from "../../Dialogs/AuthorAddDialog";
import { useAppSelector } from "@application/store";
import React, { useState } from "react";
import { AuthorUpdateDTO } from "@infrastructure/apis/client";
import EditAuthorDialog from '@presentation/components/ui/Dialogs/AuthorAddDialog/EditAuthorDialog';
import { useAuthorAddDialogController } from "../../Dialogs/AuthorAddDialog/AuthorAddDialog.controller";

/**
 * This hook returns a header for the table with translated columns.
 */
const useHeader = (): { key: keyof AuthorDTO, name: string }[] => {
    const { formatMessage } = useIntl();

    return [
        { key: "name", name: formatMessage({ id: "globals.name" }) },
        { key: "nationality", name: formatMessage({ id: "globals.nationality" }) },
        { key: "dateOfBirth", name: formatMessage({ id: "globals.dateOfBirth" }) },
        { key: "biography", name: formatMessage({ id: "globals.biography" }) }
    ]
};

/**
 * The values in the table are organized as rows so this function takes the entries and creates the row values ordering them according to the order map.
 */
const getRowValues = (entries: AuthorDTO[] | null | undefined, orderMap: { [key: string]: number }) =>
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
export const AuthorTable = () => {
    const { userId: ownUserId } = useAppSelector(x => x.profileReducer);
    const { formatMessage } = useIntl();
    const header = useHeader();
    const orderMap = header.reduce((acc, e, i) => { return { ...acc, [e.key]: i } }, {}) as { [key: string]: number }; // Get the header column order.
    const { handleChangePage, handleChangePageSize, pagedData, isError, isLoading, tryReload, labelDisplay, remove, update } = useAuthorTableController(); // Use the controller hook.
    const rowValues = getRowValues(pagedData?.data, orderMap); // Get the row values.
    const [searchQuery, setSearchQuery] = useState('');
    const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSearchQuery(event.target.value);
    };

    // Filter author data based on search query
    const filteredRowValues = rowValues?.filter(({ entry }) =>
        (entry.name ?? '').toLowerCase().includes(searchQuery.toLowerCase())
    );


    const { open } = useAuthorAddDialogController();

    return <DataLoadingContainer isError={isError} isLoading={isLoading} tryReload={tryReload}> {/* Wrap the table into the loading container because data will be fetched from the backend and is not immediately available.*/}
        <AuthorAddDialog /> {/* Add the button to open the user add modal. */}
        <TextField // Search input field
                label="Search Authors"
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
                                            <EditAuthorDialog initialData={entry}/>
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