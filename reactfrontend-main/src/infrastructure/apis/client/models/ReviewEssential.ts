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
 * @interface ReviewEssential
 */
export interface ReviewEssential {
    /**
     * 
     * @type {string}
     * @memberof ReviewEssential
     */
    userId?: string;
    /**
     * 
     * @type {string}
     * @memberof ReviewEssential
     */
    comment?: string | null;
}

/**
 * Check if a given object implements the ReviewEssential interface.
 */
export function instanceOfReviewEssential(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function ReviewEssentialFromJSON(json: any): ReviewEssential {
    return ReviewEssentialFromJSONTyped(json, false);
}

export function ReviewEssentialFromJSONTyped(json: any, ignoreDiscriminator: boolean): ReviewEssential {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'userId': !exists(json, 'userId') ? undefined : json['userId'],
        'comment': !exists(json, 'comment') ? undefined : json['comment'],
    };
}

export function ReviewEssentialToJSON(value?: ReviewEssential | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'userId': value.userId,
        'comment': value.comment,
    };
}
