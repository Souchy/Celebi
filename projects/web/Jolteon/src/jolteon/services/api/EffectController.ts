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

import { DeleteResult, IEffect, ReplaceOneResult } from "./data-contracts";
import { ContentType, HttpClient, HttpResponse, RequestParams } from "./http-client";

export class EffectController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags EffectController
   * @name GetAll
   * @request GET:/models/effects/all
   * @response `200` `(IEffect)[]` Success
   */
  public getAll(params: RequestParams = {}): Promise<HttpResponse<IEffect[], any>> {
    return this.request<IEffect[], any>({
      path: `/models/effects/all`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EffectController
   * @name GetEffect
   * @request GET:/models/effects/effect/{id}
   * @response `200` `IEffect` Success
   */
  public getEffect(id: string, params: RequestParams = {}): Promise<HttpResponse<IEffect, any>> {
    return this.request<IEffect, any>({
      path: `/models/effects/effect/${id}`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EffectController
   * @name PutEffect
   * @request PUT:/models/effects/effect/{id}
   * @response `200` `ReplaceOneResult` Success
   */
  public putEffect(
    id: string,
    data: IEffect,
    params: RequestParams = {},
  ): Promise<HttpResponse<ReplaceOneResult, any>> {
    return this.request<ReplaceOneResult, any>({
      path: `/models/effects/effect/${id}`,
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
   * @tags EffectController
   * @name DeleteEffect
   * @request DELETE:/models/effects/effect/{id}
   * @response `200` `DeleteResult` Success
   */
  public deleteEffect(id: string, params: RequestParams = {}): Promise<HttpResponse<DeleteResult, any>> {
    return this.request<DeleteResult, any>({
      path: `/models/effects/effect/${id}`,
      method: "DELETE",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EffectController
   * @name PostEffect
   * @request POST:/models/effects/effect
   * @response `200` `void` Success
   */
  public postEffect(data: IEffect, params: RequestParams = {}): Promise<HttpResponse<void, any>> {
    return this.request<void, any>({
      path: `/models/effects/effect`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  }
}
