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

export class TestController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags TestController
   * @name GetPing
   * @request GET:/test/ping
   * @response `200` `string` Success
   */
  getPing = (params: RequestParams = {}) =>
    this.request<string, any>({
      path: `/test/ping`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags TestController
   * @name GetPrivatePring
   * @request GET:/test/privatePring
   * @response `200` `string` Success
   */
  getPrivatePring = (params: RequestParams = {}) =>
    this.request<string, any>({
      path: `/test/privatePring`,
      method: "GET",
      format: "json",
      ...params,
    });
}
