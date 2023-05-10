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

import { DeleteResult, ISpellModel, ReplaceOneResult, SpellModel } from "./data-contracts";
import { ContentType, HttpClient, HttpResponse, RequestParams } from "./http-client";

export class SpellModelController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags SpellModelController
   * @name GetAll
   * @request GET:/models/spells/all
   * @response `200` `(ISpellModel)[]` Success
   */
  public getAll(params: RequestParams = {}): Promise<HttpResponse<ISpellModel[], any>> {
    return this.request<ISpellModel[], any>({
      path: `/models/spells/all`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags SpellModelController
   * @name GetSpell
   * @request GET:/models/spells/spell/{id}
   * @response `200` `ISpellModel` Success
   */
  public getSpell(id: string, params: RequestParams = {}): Promise<HttpResponse<ISpellModel, any>> {
    return this.request<ISpellModel, any>({
      path: `/models/spells/spell/${id}`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags SpellModelController
   * @name PutSpell
   * @request PUT:/models/spells/spell/{id}
   * @response `200` `ReplaceOneResult` Success
   */
  public putSpell(
    id: string,
    data: SpellModel,
    params: RequestParams = {},
  ): Promise<HttpResponse<ReplaceOneResult, any>> {
    return this.request<ReplaceOneResult, any>({
      path: `/models/spells/spell/${id}`,
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
   * @tags SpellModelController
   * @name DeleteSpell
   * @request DELETE:/models/spells/spell/{id}
   * @response `200` `DeleteResult` Success
   */
  public deleteSpell(id: string, params: RequestParams = {}): Promise<HttpResponse<DeleteResult, any>> {
    return this.request<DeleteResult, any>({
      path: `/models/spells/spell/${id}`,
      method: "DELETE",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags SpellModelController
   * @name PostSpell
   * @request POST:/models/spells/spell
   * @response `200` `void` Success
   */
  public postSpell(data: SpellModel, params: RequestParams = {}): Promise<HttpResponse<void, any>> {
    return this.request<void, any>({
      path: `/models/spells/spell`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags SpellModelController
   * @name PostNew
   * @request POST:/models/spells/new
   * @response `200` `void` Success
   */
  public postNew(params: RequestParams = {}): Promise<HttpResponse<void, any>> {
    return this.request<void, any>({
      path: `/models/spells/new`,
      method: "POST",
      ...params,
    });
  }
}
