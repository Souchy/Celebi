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
import { ContentType, HttpClient, HttpResponse, RequestParams } from "./http-client";

export class CreatureModelController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags CreatureModelController
   * @name GetCreatures
   * @request GET:/models/creature/creatures
   * @response `200` `(ICreatureModel)[]` Success
   */
  public getCreatures(params: RequestParams = {}): Promise<HttpResponse<ICreatureModel[], any>> {
    return this.request<ICreatureModel[], any>({
      path: `/models/creature/creatures`,
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
   * @request GET:/models/creature/{id}
   * @response `200` `ICreatureModel` Success
   */
  public getCreature(id: string, params: RequestParams = {}): Promise<HttpResponse<ICreatureModel, any>> {
    return this.request<ICreatureModel, any>({
      path: `/models/creature/${id}`,
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
   * @request PUT:/models/creature/{id}
   * @response `200` `void` Success
   */
  public putCreature(id: string, data: ICreatureModel, params: RequestParams = {}): Promise<HttpResponse<void, any>> {
    return this.request<void, any>({
      path: `/models/creature/${id}`,
      method: "PUT",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags CreatureModelController
   * @name DeleteCreature
   * @request DELETE:/models/creature/{id}
   * @response `200` `void` Success
   */
  public deleteCreature(id: string, params: RequestParams = {}): Promise<HttpResponse<void, any>> {
    return this.request<void, any>({
      path: `/models/creature/${id}`,
      method: "DELETE",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags CreatureModelController
   * @name PostCreate
   * @request POST:/models/creature/create
   * @response `200` `void` Success
   */
  public postCreate(data: ICreatureModel, params: RequestParams = {}): Promise<HttpResponse<void, any>> {
    return this.request<void, any>({
      path: `/models/creature/create`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  }
}
