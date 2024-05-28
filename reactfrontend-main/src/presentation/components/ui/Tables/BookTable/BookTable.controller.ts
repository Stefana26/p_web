import { useTableController } from "../Table.controller";
import { useBookApi } from "@infrastructure/apis/api-management";
import { BookUpdateDTO } from "@infrastructure/apis/client";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useCallback } from "react";
import { usePaginationController } from "../Pagination.controller";
import { toast } from "react-toastify";

/**
 * This is controller hook manages the table state including the pagination and data retrieval from the backend.
 */
export const useBookTableController = () => {
    const { getBooks: { key: queryKey, query }, deleteBook: { key: deleteBookKey, mutation: deleteBook }, updateBook: { key: updateBookKey, mutation: updateBook } } = useBookApi(); // Use the API hook.
    const queryClient = useQueryClient(); // Get the query client.
    const { page, pageSize, setPagination } = usePaginationController(); // Get the pagination state.
    const { data, isError, isLoading } = useQuery({
        queryKey: [queryKey, page, pageSize],
        queryFn: () => query({ page, pageSize })
    }); // Retrieve the table page from the backend via the query hook.
    const { mutateAsync: deleteMutation } = useMutation({
        mutationKey: [deleteBookKey],
        mutationFn: deleteBook
    }); // Use a mutation to remove an entry.
    // const remove = useCallback(
    //     (id: string) => deleteMutation(id).then(() => queryClient.invalidateQueries({ queryKey: [queryKey] })),
    //     [queryClient, deleteMutation, queryKey]); // Create the callback to remove an entry.
    const remove = useCallback(
        (id: string) => {
            deleteMutation(id)
                .then(() => {
                    queryClient.invalidateQueries({ queryKey: [queryKey] });
                    toast.success('book deleted successfully');
                })
                .catch((error) => {
                    toast.error(`Failed to delete book: ${error.message}`);
                });
        },
        [queryClient, deleteMutation, queryKey]
    );
    const { mutateAsync: updateMutation } = useMutation({
        mutationKey: [updateBookKey],
        mutationFn: updateBook
    });
    const update = useCallback(
        (book: BookUpdateDTO) => updateMutation(book).then(() => queryClient.invalidateQueries({ queryKey: [queryKey] })),
        [queryClient, updateMutation, queryKey]); // Create the callback to update an entry.
    

    const tryReload = useCallback(
        () => queryClient.invalidateQueries({ queryKey: [queryKey] }),
        [queryClient, queryKey]); // Create a callback to try reloading the data for the table via query invalidation.

    const tableController = useTableController(setPagination, data?.response?.pageSize); // Adapt the pagination for the table.

    return { // Return the controller state and actions.
        ...tableController,
        tryReload,
        pagedData: data?.response,
        isError,
        isLoading,
        remove,
        update
    };
}