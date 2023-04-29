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
   * @name GetPrivatePing
   * @request GET:/auth/privatePing
   * @response `200` `string` Success
   */
  getPrivatePing = (params: RequestParams = {}) =>
    this.request<string, any>({
      path: `/auth/privatePing`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags AuthController
   * @name PostSignUp
   * @request POST:/auth/signUp
   * @response `200` `boolean` Success
   */
  postSignUp = (params: RequestParams = {}) =>
    this.request<boolean, any>({
      path: `/auth/signUp`,
      method: "POST",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags AuthController
   * @name PostSignIn
   * @request POST:/auth/signIn
   * @response `200` `void` Success
   */
  postSignIn = (params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/auth/signIn`,
      method: "POST",
      ...params,
    });
}
