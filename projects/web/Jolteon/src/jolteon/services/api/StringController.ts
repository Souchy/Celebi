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

import {
  DeleteResult,
  I18NType,
  IStringEntity,
  IStringEntityFilterDefinition,
  ReplaceOneResult,
  StringEntity,
} from "./data-contracts";
import { ContentType, HttpClient, HttpResponse, RequestParams } from "./http-client";

export class StringController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags StringController
   * @name GetAll
   * @request GET:/models/strings/all
   * @response `200` `(IStringEntity)[]` Success
   */
  public getAll(
    query?: {
      lang?: I18NType;
    },
    params: RequestParams = {},
  ): Promise<HttpResponse<IStringEntity[], any>> {
    return this.request<IStringEntity[], any>({
      path: `/models/strings/all`,
      method: "GET",
      query: query,
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags StringController
   * @name GetFiltered
   * @request GET:/models/strings/filtered
   * @response `200` `(IStringEntity)[]` Success
   */
  public getFiltered(
    data: IStringEntityFilterDefinition,
    query?: {
      lang?: I18NType;
    },
    params: RequestParams = {},
  ): Promise<HttpResponse<IStringEntity[], any>> {
    return this.request<IStringEntity[], any>({
      path: `/models/strings/filtered`,
      method: "GET",
      query: query,
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags StringController
   * @name GetString
   * @request GET:/models/strings/string/{id}
   * @response `200` `IStringEntity` Success
   */
  public getString(
    id: string,
    query?: {
      lang?: I18NType;
    },
    params: RequestParams = {},
  ): Promise<HttpResponse<IStringEntity, any>> {
    return this.request<IStringEntity, any>({
      path: `/models/strings/string/${id}`,
      method: "GET",
      query: query,
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags StringController
   * @name PutString
   * @request PUT:/models/strings/string/{id}
   * @response `200` `ReplaceOneResult` Success
   */
  public putString(
    id: string,
    data: StringEntity,
    query?: {
      lang?: I18NType;
    },
    params: RequestParams = {},
  ): Promise<HttpResponse<ReplaceOneResult, any>> {
    return this.request<ReplaceOneResult, any>({
      path: `/models/strings/string/${id}`,
      method: "PUT",
      query: query,
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags StringController
   * @name DeleteString
   * @request DELETE:/models/strings/string/{id}
   * @response `200` `DeleteResult` Success
   */
  public deleteString(id: string, params: RequestParams = {}): Promise<HttpResponse<DeleteResult, any>> {
    return this.request<DeleteResult, any>({
      path: `/models/strings/string/${id}`,
      method: "DELETE",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags StringController
   * @name PostString
   * @request POST:/models/strings/string
   * @response `200` `void` Success
   */
  public postString(data: StringEntity, params: RequestParams = {}): Promise<HttpResponse<void, any>> {
    return this.request<void, any>({
      path: `/models/strings/string`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  }
}
