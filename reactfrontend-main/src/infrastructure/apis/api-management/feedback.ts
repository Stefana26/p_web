import { useAppSelector } from "@application/store";
import { FeedbackApi} from "../client";
import { FeedbackAddDTO } from "../client/models";
import { getAuthenticationConfiguration } from "@infrastructure/utils/userUtils";

/**
 * Use constants to identify mutations and queries.
 */
const addFeedbackMutationKey = "addFeedbackMutation";


/**
 * Returns the an object with the callbacks that can be used for the React Query API, in this case to manage the user API.
 */
export const useFeedbackApi = () => {
    const { token } = useAppSelector(x => x.profileReducer); // You can use the data form the Redux storage. 
    const config = getAuthenticationConfiguration(token); // Use the token to configure the authentication header.

    const addFeedback = (feedback: FeedbackAddDTO) => new FeedbackApi(config).apiFeedbackAddPost({ feedbackAddDTO: feedback });

    return {
        addFeedback: { // Return the mutation object.
            key: addFeedbackMutationKey, // Add the key to identify the mutation.
            mutation: addFeedback // Add the mutation callback.
        }
    }
}