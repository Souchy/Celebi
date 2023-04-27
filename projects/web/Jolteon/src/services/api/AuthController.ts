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

import { HttpClient, RequestParams } from "./http-client";

export class AuthController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags AuthController
   * @name GetPing
   * @request GET:/ping
   * @response `200` `string` Success
   */
  getPing = (params: RequestParams = {}) =>
    this.request<string, any>({
      path: `/ping`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags AuthController
   * @name GetPrivatePring
   * @request GET:/privatePring
   * @response `200` `string` Success
   */
  getPrivatePring = (params: RequestParams = {}) =>
    this.request<string, any>({
      path: `/privatePring`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags AuthController
   * @name PostGoogle
   * @request POST:/google
   * @response `200` `void` Success
   */
  postGoogle = (params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/google`,
      method: "POST",
      ...params,
    });
}
