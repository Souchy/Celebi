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

import { ICreatureModel } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class CreatureModelController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags CreatureModelController
   * @name GetCreatureModelController
   * @request GET:/models/CreatureModelController
   * @response `200` `(ICreatureModel)[]` Success
   */
  getCreatureModelController = (params: RequestParams = {}) =>
    this.request<ICreatureModel[], any>({
      path: `/models/CreatureModelController`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags CreatureModelController
   * @name PostCreatureModelController
   * @request POST:/models/CreatureModelController
   * @response `200` `void` Success
   */
  postCreatureModelController = (data: ICreatureModel, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/models/CreatureModelController`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags CreatureModelController
   * @name GetCreatureModelController2
   * @request GET:/models/CreatureModelController/{id}
   * @originalName getCreatureModelController
   * @duplicate
   * @response `200` `ICreatureModel` Success
   */
  getCreatureModelController2 = (id: string, params: RequestParams = {}) =>
    this.request<ICreatureModel, any>({
      path: `/models/CreatureModelController/${id}`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags CreatureModelController
   * @name PutCreatureModelController
   * @request PUT:/models/CreatureModelController/{id}
   * @response `200` `void` Success
   */
  putCreatureModelController = (id: string, data: ICreatureModel, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/models/CreatureModelController/${id}`,
      method: "PUT",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags CreatureModelController
   * @name DeleteCreatureModelController
   * @request DELETE:/models/CreatureModelController/{id}
   * @response `200` `void` Success
   */
  deleteCreatureModelController = (id: string, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/models/CreatureModelController/${id}`,
      method: "DELETE",
      ...params,
    });
}
