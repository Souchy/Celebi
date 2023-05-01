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

import { CharacteristicId, CharacteristicType } from "./data-contracts";
import { HttpClient, HttpResponse, RequestParams } from "./http-client";

export class PropertiesController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags PropertiesController
   * @name GetCharacCharacteristicId
   * @request GET:/models/properties/charac/characteristicId
   * @response `200` `(CharacteristicId)[]` Success
   */
  public getCharacCharacteristicId(params: RequestParams = {}): Promise<HttpResponse<CharacteristicId[], any>> {
    return this.request<CharacteristicId[], any>({
      path: `/models/properties/charac/characteristicId`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags PropertiesController
   * @name GetCharacCharacType
   * @request GET:/models/properties/charac/characType
   * @response `200` `(CharacteristicType)[]` Success
   */
  public getCharacCharacType(params: RequestParams = {}): Promise<HttpResponse<CharacteristicType[], any>> {
    return this.request<CharacteristicType[], any>({
      path: `/models/properties/charac/characType`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
}
