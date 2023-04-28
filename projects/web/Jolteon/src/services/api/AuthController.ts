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
   * @request GET:/auth/ping
   * @response `200` `string` Success
   */
  getPing = (params: RequestParams = {}) =>
    this.request<string, any>({
      path: `/auth/ping`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags AuthController
   * @name GetPrivatePring
   * @request GET:/auth/privatePring
   * @response `200` `string` Success
   */
  getPrivatePring = (params: RequestParams = {}) =>
    this.request<string, any>({
      path: `/auth/privatePring`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags AuthController
   * @name GetPping
   * @request GET:/auth/pping
   * @response `200` `void` Success
   */
  getPping = (params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/auth/pping`,
      method: "GET",
      ...params,
    });
  /**
   * No description
   *
   * @tags AuthController
   * @name GetAaing
   * @request GET:/auth/aaing
   * @response `200` `void` Success
   */
  getAaing = (params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/auth/aaing`,
      method: "GET",
      ...params,
    });
  /**
   * No description
   *
   * @tags AuthController
   * @name PostGoogle
   * @request POST:/auth/google
   * @response `200` `void` Success
   */
  postGoogle = (params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/auth/google`,
      method: "POST",
      ...params,
    });
}
