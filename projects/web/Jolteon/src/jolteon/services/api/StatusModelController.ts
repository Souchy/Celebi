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

import { DeleteResult, IStatusModel, ReplaceOneResult, StatusModel } from "./data-contracts";
import { ContentType, HttpClient, HttpResponse, RequestParams } from "./http-client";

export class StatusModelController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags StatusModelController
   * @name GetAll
   * @request GET:/models/status/all
   * @response `200` `(IStatusModel)[]` Success
   */
  public getAll(params: RequestParams = {}): Promise<HttpResponse<IStatusModel[], any>> {
    return this.request<IStatusModel[], any>({
      path: `/models/status/all`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags StatusModelController
   * @name GetStatus
   * @request GET:/models/status/status/{id}
   * @response `200` `IStatusModel` Success
   */
  public getStatus(id: string, params: RequestParams = {}): Promise<HttpResponse<IStatusModel, any>> {
    return this.request<IStatusModel, any>({
      path: `/models/status/status/${id}`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags StatusModelController
   * @name PutStatus
   * @request PUT:/models/status/status/{id}
   * @response `200` `ReplaceOneResult` Success
   */
  public putStatus(
    id: string,
    data: StatusModel,
    params: RequestParams = {},
  ): Promise<HttpResponse<ReplaceOneResult, any>> {
    return this.request<ReplaceOneResult, any>({
      path: `/models/status/status/${id}`,
      method: "PUT",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags StatusModelController
   * @name DeleteStatus
   * @request DELETE:/models/status/status/{id}
   * @response `200` `DeleteResult` Success
   */
  public deleteStatus(id: string, params: RequestParams = {}): Promise<HttpResponse<DeleteResult, any>> {
    return this.request<DeleteResult, any>({
      path: `/models/status/status/${id}`,
      method: "DELETE",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags StatusModelController
   * @name PostStatus
   * @request POST:/models/status/status
   * @response `200` `void` Success
   */
  public postStatus(data: StatusModel, params: RequestParams = {}): Promise<HttpResponse<void, any>> {
    return this.request<void, any>({
      path: `/models/status/status`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  }
}
