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

import { DeleteResult, ICreatureModel, ReplaceOneResult } from "./data-contracts";
import { ContentType, HttpClient, HttpResponse, RequestParams } from "./http-client";

export class CreatureModelController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags CreatureModelController
   * @name GetAll
   * @request GET:/models/creatures/all
   * @response `200` `(ICreatureModel)[]` Success
   */
  public getAll(params: RequestParams = {}): Promise<HttpResponse<ICreatureModel[], any>> {
    return this.request<ICreatureModel[], any>({
      path: `/models/creatures/all`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags CreatureModelController
   * @name GetCreature
   * @request GET:/models/creatures/creature/{id}
   * @response `200` `ICreatureModel` Success
   */
  public getCreature(id: string, params: RequestParams = {}): Promise<HttpResponse<ICreatureModel, any>> {
    return this.request<ICreatureModel, any>({
      path: `/models/creatures/creature/${id}`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags CreatureModelController
   * @name PutCreature
   * @request PUT:/models/creatures/creature/{id}
   * @response `200` `ReplaceOneResult` Success
   */
  public putCreature(
    id: string,
    data: ICreatureModel,
    params: RequestParams = {},
  ): Promise<HttpResponse<ReplaceOneResult, any>> {
    return this.request<ReplaceOneResult, any>({
      path: `/models/creatures/creature/${id}`,
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
   * @tags CreatureModelController
   * @name DeleteCreature
   * @request DELETE:/models/creatures/creature/{id}
   * @response `200` `DeleteResult` Success
   */
  public deleteCreature(id: string, params: RequestParams = {}): Promise<HttpResponse<DeleteResult, any>> {
    return this.request<DeleteResult, any>({
      path: `/models/creatures/creature/${id}`,
      method: "DELETE",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags CreatureModelController
   * @name PostCreature
   * @request POST:/models/creatures/creature
   * @response `200` `void` Success
   */
  public postCreature(data: ICreatureModel, params: RequestParams = {}): Promise<HttpResponse<void, any>> {
    return this.request<void, any>({
      path: `/models/creatures/creature`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  }
}
