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

import { DeleteResult, IStats, ReplaceOneResult } from "./data-contracts";
import { ContentType, HttpClient, HttpResponse, RequestParams } from "./http-client";

export class StatsController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags StatsController
   * @name GetAll
   * @request GET:/models/stats/all
   * @response `200` `(IStats)[]` Success
   */
  public getAll(params: RequestParams = {}): Promise<HttpResponse<IStats[], any>> {
    return this.request<IStats[], any>({
      path: `/models/stats/all`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags StatsController
   * @name GetStats
   * @request GET:/models/stats/stats/{id}
   * @response `200` `IStats` Success
   */
  public getStats(id: string, params: RequestParams = {}): Promise<HttpResponse<IStats, any>> {
    return this.request<IStats, any>({
      path: `/models/stats/stats/${id}`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags StatsController
   * @name PutStats
   * @request PUT:/models/stats/stats/{id}
   * @response `200` `ReplaceOneResult` Success
   */
  public putStats(id: string, data: IStats, params: RequestParams = {}): Promise<HttpResponse<ReplaceOneResult, any>> {
    return this.request<ReplaceOneResult, any>({
      path: `/models/stats/stats/${id}`,
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
   * @tags StatsController
   * @name DeleteStats
   * @request DELETE:/models/stats/stats/{id}
   * @response `200` `DeleteResult` Success
   */
  public deleteStats(id: string, params: RequestParams = {}): Promise<HttpResponse<DeleteResult, any>> {
    return this.request<DeleteResult, any>({
      path: `/models/stats/stats/${id}`,
      method: "DELETE",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags StatsController
   * @name PostStats
   * @request POST:/models/stats/stats
   * @response `200` `void` Success
   */
  public postStats(data: IStats, params: RequestParams = {}): Promise<HttpResponse<void, any>> {
    return this.request<void, any>({
      path: `/models/stats/stats`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  }
}
