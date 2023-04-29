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
import { HttpClient, RequestParams } from "./http-client";

export class ShopController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags ShopController
   * @name GetShopControllerProducts
   * @request GET:/meta/ShopController/products
   * @response `200` `(ShopProduct)[]` Success
   */
  getShopControllerProducts = (params: RequestParams = {}) =>
    this.request<ShopProduct[], any>({
      path: `/meta/ShopController/products`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags ShopController
   * @name GetShopControllerCurrencies
   * @request GET:/meta/ShopController/currencies
   * @response `200` `(ShopCurrency)[]` Success
   */
  getShopControllerCurrencies = (params: RequestParams = {}) =>
    this.request<ShopCurrency[], any>({
      path: `/meta/ShopController/currencies`,
      method: "GET",
      format: "json",
      ...params,
    });
}
