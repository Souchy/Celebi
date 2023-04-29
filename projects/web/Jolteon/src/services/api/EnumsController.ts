/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

import { CharacteristicCategory } from "./data-contracts";
import { HttpClient, RequestParams } from "./http-client";

export class EnumsController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetEnumsControllerCharacteristicCategory
   * @request GET:/models/EnumsController/characteristicCategory
   * @response `200` `(CharacteristicCategory)[]` Success
   */
  getEnumsControllerCharacteristicCategory = (params: RequestParams = {}) =>
    this.request<CharacteristicCategory[], any>({
      path: `/models/EnumsController/characteristicCategory`,
      method: "GET",
      format: "json",
      ...params,
    });
}
