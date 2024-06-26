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

import { exists, mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface BookAddDTO
 */
export interface BookAddDTO {
    /**
     * 
     * @type {string}
     * @memberof BookAddDTO
     */
    title?: string | null;
    /**
     * 
     * @type {string}
     * @memberof BookAddDTO
     */
    isbn?: string | null;
    /**
     * 
     * @type {string}
     * @memberof BookAddDTO
     */
    author?: string | null;
    /**
     * 
     * @type {string}
     * @memberof BookAddDTO
     */
    genre?: string | null;
    /**
     * 
     * @type {string}
     * @memberof BookAddDTO
     */
    description?: string | null;
    /**
     * 
     * @type {number}
     * @memberof BookAddDTO
     */
    pages?: number;
}

/**
 * Check if a given object implements the BookAddDTO interface.
 */
export function instanceOfBookAddDTO(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function BookAddDTOFromJSON(json: any): BookAddDTO {
    return BookAddDTOFromJSONTyped(json, false);
}

export function BookAddDTOFromJSONTyped(json: any, ignoreDiscriminator: boolean): BookAddDTO {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'title': !exists(json, 'title') ? undefined : json['title'],
        'isbn': !exists(json, 'isbn') ? undefined : json['isbn'],
        'author': !exists(json, 'author') ? undefined : json['author'],
        'genre': !exists(json, 'genre') ? undefined : json['genre'],
        'description': !exists(json, 'description') ? undefined : json['description'],
        'pages': !exists(json, 'pages') ? undefined : json['pages'],
    };
}

export function BookAddDTOToJSON(value?: BookAddDTO | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'title': value.title,
        'isbn': value.isbn,
        'author': value.author,
        'genre': value.genre,
        'description': value.description,
        'pages': value.pages,
    };
}

