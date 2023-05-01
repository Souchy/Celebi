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

import { AccountInfo } from "./data-contracts";
import { HttpClient, HttpResponse, RequestParams } from "./http-client";

export class AuthController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags AuthController
   * @name GetPing
   * @request GET:/meta/auth/ping
   * @response `200` `string` Success
   */
  public getPing(params: RequestParams = {}): Promise<HttpResponse<string, any>> {
    return this.request<string, any>({
      path: `/meta/auth/ping`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags AuthController
   * @name GetPrivatePing
   * @request GET:/meta/auth/privatePing
   * @response `200` `string` Success
   */
  public getPrivatePing(params: RequestParams = {}): Promise<HttpResponse<string, any>> {
    return this.request<string, any>({
      path: `/meta/auth/privatePing`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags AuthController
   * @name PostDisplayName
   * @request POST:/meta/auth/displayName
   * @response `200` `boolean` Success
   */
  public postDisplayName(
    query?: {
      displayname?: string;
    },
    params: RequestParams = {},
  ): Promise<HttpResponse<boolean, any>> {
    return this.request<boolean, any>({
      path: `/meta/auth/displayName`,
      method: "POST",
      query: query,
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags AuthController
   * @name GetAccountInfo
   * @request GET:/meta/auth/accountInfo
   * @response `200` `AccountInfo` Success
   */
  public getAccountInfo(params: RequestParams = {}): Promise<HttpResponse<AccountInfo, any>> {
    return this.request<AccountInfo, any>({
      path: `/meta/auth/accountInfo`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags AuthController
   * @name PostIdentitySignup
   * @request POST:/meta/auth/identitySignup
   * @response `200` `AccountInfo` Success
   */
  public postIdentitySignup(
    query: {
      displayName: string;
      /** @format email */
      email: string;
      pass: string;
    },
    params: RequestParams = {},
  ): Promise<HttpResponse<AccountInfo, any>> {
    return this.request<AccountInfo, any>({
      path: `/meta/auth/identitySignup`,
      method: "POST",
      query: query,
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags AuthController
   * @name PostIdentitySignin
   * @request POST:/meta/auth/identitySignin
   * @response `200` `AccountInfo` Success
   */
  public postIdentitySignin(
    query: {
      /** @format email */
      email: string;
      pass: string;
    },
    params: RequestParams = {},
  ): Promise<HttpResponse<AccountInfo, any>> {
    return this.request<AccountInfo, any>({
      path: `/meta/auth/identitySignin`,
      method: "POST",
      query: query,
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags AuthController
   * @name PostIdentitySignout
   * @request POST:/meta/auth/identitySignout
   * @response `200` `void` Success
   */
  public postIdentitySignout(params: RequestParams = {}): Promise<HttpResponse<void, any>> {
    return this.request<void, any>({
      path: `/meta/auth/identitySignout`,
      method: "POST",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags AuthController
   * @name PostIdentitySigninExternal
   * @request POST:/meta/auth/identitySigninExternal
   * @response `200` `void` Success
   */
  public postIdentitySigninExternal(params: RequestParams = {}): Promise<HttpResponse<void, any>> {
    return this.request<void, any>({
      path: `/meta/auth/identitySigninExternal`,
      method: "POST",
      ...params,
    });
  }
}
