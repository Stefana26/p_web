type InterceptorPros = {
    onRequest?: (args: [input: RequestInfo | URL, init?: RequestInit | undefined]) => [input: RequestInfo | URL, init?: RequestInit | undefined];
    onResponse?: (response: Response) => Response | Promise<Response>;
    onResponseError?: (error: any) => any;
};

const { fetch: originalFetch } = window;

/**
 * This hook sets the fetch interceptor to intercept HTTP requests and do some additional actions on it.
 */
export const useInterceptor = (props: InterceptorPros) => {
    if (window.fetch === originalFetch) {
        window.fetch = async (...args) => {
            let resource = args[0];
            let config = args[1];

            if (props.onRequest) {
                try {
                    [resource, config] = props.onRequest(args);
                } catch (e) {
                    console.error("Error in onRequest interceptor:", e);
                    throw e;
                }
            }

            console.log("Fetch request:", { resource, config });

            try {
                const response = await originalFetch(resource, config);
                console.log("Fetch response:", response);

                if (props.onResponse) {
                    return props.onResponse(response);
                }
                return response;
            } catch (error: any) {
                console.error("Fetch error:", error);

                if (props.onResponseError) {
                    try {
                        return props.onResponseError(error);
                    } catch (e) {
                        console.error("Error in onResponseError interceptor:", e);
                        throw e;
                    }
                } else {
                    throw error;
                }
            }
        };

        console.log("Fetch interceptor set!");
    }
};
