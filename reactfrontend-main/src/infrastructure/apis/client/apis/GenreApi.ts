/* tslint:disable */
/* eslint-disable */
/**
 * MobyLab Web App
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import * as runtime from '../runtime';
import type {
  GenreAddDTO,
  GenreDTOPagedResponseRequestResponse,
  GenreDTORequestResponse,
  GenreUpdateDTO,
  RequestResponse,
} from '../models';
import {
    GenreAddDTOFromJSON,
    GenreAddDTOToJSON,
    GenreDTOPagedResponseRequestResponseFromJSON,
    GenreDTOPagedResponseRequestResponseToJSON,
    GenreDTORequestResponseFromJSON,
    GenreDTORequestResponseToJSON,
    GenreUpdateDTOFromJSON,
    GenreUpdateDTOToJSON,
    RequestResponseFromJSON,
    RequestResponseToJSON,
} from '../models';

export interface ApiGenreAddPostRequest {
    genreAddDTO?: GenreAddDTO;
}

export interface ApiGenreDeleteIdDeleteRequest {
    id: string;
}

export interface ApiGenreGetByIdIdGetRequest {
    id: string;
}

export interface ApiGenreGetPageGetRequest {
    search?: string;
    page?: number;
    pageSize?: number;
}

export interface ApiGenreUpdatePutRequest {
    genreUpdateDTO?: GenreUpdateDTO;
}

/**
 * 
 */
export class GenreApi extends runtime.BaseAPI {

    /**
     */
    async apiGenreAddPostRaw(requestParameters: ApiGenreAddPostRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Genre/Add`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: GenreAddDTOToJSON(requestParameters.genreAddDTO),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiGenreAddPost(requestParameters: ApiGenreAddPostRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiGenreAddPostRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiGenreDeleteIdDeleteRaw(requestParameters: ApiGenreDeleteIdDeleteRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        if (requestParameters.id === null || requestParameters.id === undefined) {
            throw new runtime.RequiredError('id','Required parameter requestParameters.id was null or undefined when calling apiGenreDeleteIdDelete.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Genre/Delete/{id}`.replace(`{${"id"}}`, encodeURIComponent(String(requestParameters.id))),
            method: 'DELETE',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiGenreDeleteIdDelete(requestParameters: ApiGenreDeleteIdDeleteRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiGenreDeleteIdDeleteRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiGenreGetByIdIdGetRaw(requestParameters: ApiGenreGetByIdIdGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GenreDTORequestResponse>> {
        if (requestParameters.id === null || requestParameters.id === undefined) {
            throw new runtime.RequiredError('id','Required parameter requestParameters.id was null or undefined when calling apiGenreGetByIdIdGet.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Genre/GetById/{id}`.replace(`{${"id"}}`, encodeURIComponent(String(requestParameters.id))),
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => GenreDTORequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiGenreGetByIdIdGet(requestParameters: ApiGenreGetByIdIdGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GenreDTORequestResponse> {
        const response = await this.apiGenreGetByIdIdGetRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiGenreGetPageGetRaw(requestParameters: ApiGenreGetPageGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GenreDTOPagedResponseRequestResponse>> {
        const queryParameters: any = {};

        if (requestParameters.search !== undefined) {
            queryParameters['Search'] = requestParameters.search;
        }

        if (requestParameters.page !== undefined) {
            queryParameters['Page'] = requestParameters.page;
        }

        if (requestParameters.pageSize !== undefined) {
            queryParameters['PageSize'] = requestParameters.pageSize;
        }

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Genre/GetPage`,
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => GenreDTOPagedResponseRequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiGenreGetPageGet(requestParameters: ApiGenreGetPageGetRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GenreDTOPagedResponseRequestResponse> {
        const response = await this.apiGenreGetPageGetRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiGenreUpdatePutRaw(requestParameters: ApiGenreUpdatePutRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Genre/Update`,
            method: 'PUT',
            headers: headerParameters,
            query: queryParameters,
            body: GenreUpdateDTOToJSON(requestParameters.genreUpdateDTO),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiGenreUpdatePut(requestParameters: ApiGenreUpdatePutRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiGenreUpdatePutRaw(requestParameters, initOverrides);
        return await response.value();
    }

}