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

import { ConsumableProduct, CurrencyProduct, ModelProduct } from "./data-contracts";
import { HttpClient, HttpResponse, RequestParams } from "./http-client";

export class ShopController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags ShopController
   * @name GetModelProducts
   * @request GET:/meta/shop/modelProducts
   * @response `200` `(ModelProduct)[]` Success
   */
  public getModelProducts(params: RequestParams = {}): Promise<HttpResponse<ModelProduct[], any>> {
    return this.request<ModelProduct[], any>({
      path: `/meta/shop/modelProducts`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags ShopController
   * @name GetConsumableProducts
   * @request GET:/meta/shop/consumableProducts
   * @response `200` `(ConsumableProduct)[]` Success
   */
  public getConsumableProducts(params: RequestParams = {}): Promise<HttpResponse<ConsumableProduct[], any>> {
    return this.request<ConsumableProduct[], any>({
      path: `/meta/shop/consumableProducts`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags ShopController
   * @name GetCurrencyProducts
   * @request GET:/meta/shop/currencyProducts
   * @response `200` `(CurrencyProduct)[]` Success
   */
  public getCurrencyProducts(params: RequestParams = {}): Promise<HttpResponse<CurrencyProduct[], any>> {
    return this.request<CurrencyProduct[], any>({
      path: `/meta/shop/currencyProducts`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags ShopController
   * @name PostBuyModel
   * @request POST:/meta/shop/buyModel
   * @response `200` `ModelProduct` Success
   */
  public postBuyModel(
    query?: {
      id?: string;
    },
    params: RequestParams = {},
  ): Promise<HttpResponse<ModelProduct, any>> {
    return this.request<ModelProduct, any>({
      path: `/meta/shop/buyModel`,
      method: "POST",
      query: query,
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags ShopController
   * @name PostBuyConsumable
   * @request POST:/meta/shop/buyConsumable
   * @response `200` `ModelProduct` Success
   */
  public postBuyConsumable(
    query?: {
      id?: string;
    },
    params: RequestParams = {},
  ): Promise<HttpResponse<ModelProduct, any>> {
    return this.request<ModelProduct, any>({
      path: `/meta/shop/buyConsumable`,
      method: "POST",
      query: query,
      format: "json",
      ...params,
    });
  }
}
