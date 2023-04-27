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

import { WeatherForecast } from "./data-contracts";
import { HttpClient, RequestParams } from "./http-client";

export class WeatherForecastController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags WeatherForecastController
   * @name GetWeatherForecast
   * @request GET:/WeatherForecastController
   * @response `200` `(WeatherForecast)[]` Success
   */
  getWeatherForecast = (params: RequestParams = {}) =>
    this.request<WeatherForecast[], any>({
      path: `/WeatherForecastController`,
      method: "GET",
      format: "json",
      ...params,
    });
}
