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
import type { AuthorAddDTO } from './AuthorAddDTO';
import {
    AuthorAddDTOFromJSON,
    AuthorAddDTOFromJSONTyped,
    AuthorAddDTOToJSON,
} from './AuthorAddDTO';
import type { GenreAddDTO } from './GenreAddDTO';
import {
    GenreAddDTOFromJSON,
    GenreAddDTOFromJSONTyped,
    GenreAddDTOToJSON,
} from './GenreAddDTO';
import type { RatingEssential } from './RatingEssential';
import {
    RatingEssentialFromJSON,
    RatingEssentialFromJSONTyped,
    RatingEssentialToJSON,
} from './RatingEssential';
import type { ReviewEssential } from './ReviewEssential';
import {
    ReviewEssentialFromJSON,
    ReviewEssentialFromJSONTyped,
    ReviewEssentialToJSON,
} from './ReviewEssential';

/**
 * 
 * @export
 * @interface BookDTO
 */
export interface BookDTO {
    /**
     * 
     * @type {string}
     * @memberof BookDTO
     */
    id?: string;
    /**
     * 
     * @type {string}
     * @memberof BookDTO
     */
    title?: string | null;
    /**
     * 
     * @type {AuthorAddDTO}
     * @memberof BookDTO
     */
    author?: AuthorAddDTO;
    /**
     * 
     * @type {string}
     * @memberof BookDTO
     */
    authorId?: string | null;
    /**
     * 
     * @type {GenreAddDTO}
     * @memberof BookDTO
     */
    genre?: GenreAddDTO;
    /**
     * 
     * @type {string}
     * @memberof BookDTO
     */
    genreId?: string | null;
    /**
     * 
     * @type {string}
     * @memberof BookDTO
     */
    description?: string | null;
    /**
     * 
     * @type {number}
     * @memberof BookDTO
     */
    pages?: number;
    /**
     * 
     * @type {Array<RatingEssential>}
     * @memberof BookDTO
     */
    ratings?: Array<RatingEssential> | null;
    /**
     * 
     * @type {Array<ReviewEssential>}
     * @memberof BookDTO
     */
    reviews?: Array<ReviewEssential> | null;
}

/**
 * Check if a given object implements the BookDTO interface.
 */
export function instanceOfBookDTO(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function BookDTOFromJSON(json: any): BookDTO {
    return BookDTOFromJSONTyped(json, false);
}

export function BookDTOFromJSONTyped(json: any, ignoreDiscriminator: boolean): BookDTO {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'id': !exists(json, 'id') ? undefined : json['id'],
        'title': !exists(json, 'title') ? undefined : json['title'],
        'author': !exists(json, 'author') ? undefined : AuthorAddDTOFromJSON(json['author']),
        'authorId': !exists(json, 'authorId') ? undefined : json['authorId'],
        'genre': !exists(json, 'genre') ? undefined : GenreAddDTOFromJSON(json['genre']),
        'genreId': !exists(json, 'genreId') ? undefined : json['genreId'],
        'description': !exists(json, 'description') ? undefined : json['description'],
        'pages': !exists(json, 'pages') ? undefined : json['pages'],
        'ratings': !exists(json, 'ratings') ? undefined : (json['ratings'] === null ? null : (json['ratings'] as Array<any>).map(RatingEssentialFromJSON)),
        'reviews': !exists(json, 'reviews') ? undefined : (json['reviews'] === null ? null : (json['reviews'] as Array<any>).map(ReviewEssentialFromJSON)),
    };
}

export function BookDTOToJSON(value?: BookDTO | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'id': value.id,
        'title': value.title,
        'author': AuthorAddDTOToJSON(value.author),
        'authorId': value.authorId,
        'genre': GenreAddDTOToJSON(value.genre),
        'genreId': value.genreId,
        'description': value.description,
        'pages': value.pages,
        'ratings': value.ratings === undefined ? undefined : (value.ratings === null ? null : (value.ratings as Array<any>).map(RatingEssentialToJSON)),
        'reviews': value.reviews === undefined ? undefined : (value.reviews === null ? null : (value.reviews as Array<any>).map(ReviewEssentialToJSON)),
    };
}

