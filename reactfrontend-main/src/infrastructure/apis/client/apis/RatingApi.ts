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
  RatingAddDTO,
  RatingDTORequestResponse,
  RatingUpdateDTO,
  RequestResponse,
} from '../models';
import {
    RatingAddDTOFromJSON,
    RatingAddDTOToJSON,
    RatingDTORequestResponseFromJSON,
    RatingDTORequestResponseToJSON,
    RatingUpdateDTOFromJSON,
    RatingUpdateDTOToJSON,
    RequestResponseFromJSON,
    RequestResponseToJSON,
} from '../models';

export interface ApiRatingAddPostRequest {
    ratingAddDTO?: RatingAddDTO;
}

export interface ApiRatingDeleteIdDeleteRequest {
    id: string;
}

export interface ApiRatingGetByIdIdGetRequest {
    id: string;
}

export interface ApiRatingUpdatePutRequest {
    ratingUpdateDTO?: RatingUpdateDTO;
}

/**
 * 
 */
export class RatingApi extends runtime.BaseAPI {

    /**
     */
    async apiRatingAddPostRaw(requestParameters: ApiRatingAddPostRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Rating/Add`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: RatingAddDTOToJSON(requestParameters.ratingAddDTO),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiRatingAddPost(requestParameters: ApiRatingAddPostRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiRatingAddPostRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiRatingDeleteIdDeleteRaw(requestParameters: ApiRatingDeleteIdDeleteRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        if (requestParameters.id === null || requestParameters.id === undefined) {
            throw new runtime.RequiredError('id','Required parameter requestParameters.id was null or undefined when calling apiRatingDeleteIdDelete.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Rating/Delete/{id}`.replace(`{${"id"}}`, encodeURIComponent(String(requestParameters.id))),
            method: 'DELETE',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiRatingDeleteIdDelete(requestParameters: ApiRatingDeleteIdDeleteRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiRatingDeleteIdDeleteRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiRatingGetByIdIdGetRaw(requestParameters: ApiRatingGetByIdIdGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RatingDTORequestResponse>> {
        if (requestParameters.id === null || requestParameters.id === undefined) {
            throw new runtime.RequiredError('id','Required parameter requestParameters.id was null or undefined when calling apiRatingGetByIdIdGet.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Rating/GetById/{id}`.replace(`{${"id"}}`, encodeURIComponent(String(requestParameters.id))),
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RatingDTORequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiRatingGetByIdIdGet(requestParameters: ApiRatingGetByIdIdGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RatingDTORequestResponse> {
        const response = await this.apiRatingGetByIdIdGetRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiRatingUpdatePutRaw(requestParameters: ApiRatingUpdatePutRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Rating/Update`,
            method: 'PUT',
            headers: headerParameters,
            query: queryParameters,
            body: RatingUpdateDTOToJSON(requestParameters.ratingUpdateDTO),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiRatingUpdatePut(requestParameters: ApiRatingUpdatePutRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiRatingUpdatePutRaw(requestParameters, initOverrides);
        return await response.value();
    }

}