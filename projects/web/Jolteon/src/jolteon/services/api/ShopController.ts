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

import { ShopCurrency, ShopProduct } from "./data-contracts";
import { HttpClient, HttpResponse, RequestParams } from "./http-client";

export class ShopController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags ShopController
   * @name GetProducts
   * @request GET:/meta/shop/products
   * @response `200` `(ShopProduct)[]` Success
   */
  public getProducts(params: RequestParams = {}): Promise<HttpResponse<ShopProduct[], any>> {
    return this.request<ShopProduct[], any>({
      path: `/meta/shop/products`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags ShopController
   * @name GetCurrencies
   * @request GET:/meta/shop/currencies
   * @response `200` `(ShopCurrency)[]` Success
   */
  public getCurrencies(params: RequestParams = {}): Promise<HttpResponse<ShopCurrency[], any>> {
    return this.request<ShopCurrency[], any>({
      path: `/meta/shop/currencies`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
}
