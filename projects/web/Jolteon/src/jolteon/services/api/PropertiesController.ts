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

import {
  Affinity,
  Contextual,
  OtherProperty,
  Resistance,
  Resource,
  SpellModelProperty,
  SpellProperty,
  State,
  StatusModelProperty,
} from "./data-contracts";
import { HttpClient, HttpResponse, RequestParams } from "./http-client";

export class PropertiesController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags PropertiesController
   * @name GetCharacResource
   * @request GET:/models/properties/charac/resource
   * @response `200` `(Resource)[]` Success
   */
  public getCharacResource(params: RequestParams = {}): Promise<HttpResponse<Resource[], any>> {
    return this.request<Resource[], any>({
      path: `/models/properties/charac/resource`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags PropertiesController
   * @name GetCharacAffinity
   * @request GET:/models/properties/charac/affinity
   * @response `200` `(Affinity)[]` Success
   */
  public getCharacAffinity(params: RequestParams = {}): Promise<HttpResponse<Affinity[], any>> {
    return this.request<Affinity[], any>({
      path: `/models/properties/charac/affinity`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags PropertiesController
   * @name GetCharacResistance
   * @request GET:/models/properties/charac/resistance
   * @response `200` `(Resistance)[]` Success
   */
  public getCharacResistance(params: RequestParams = {}): Promise<HttpResponse<Resistance[], any>> {
    return this.request<Resistance[], any>({
      path: `/models/properties/charac/resistance`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags PropertiesController
   * @name GetCharacContextual
   * @request GET:/models/properties/charac/contextual
   * @response `200` `(Contextual)[]` Success
   */
  public getCharacContextual(params: RequestParams = {}): Promise<HttpResponse<Contextual[], any>> {
    return this.request<Contextual[], any>({
      path: `/models/properties/charac/contextual`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags PropertiesController
   * @name GetCharacOther
   * @request GET:/models/properties/charac/other
   * @response `200` `(OtherProperty)[]` Success
   */
  public getCharacOther(params: RequestParams = {}): Promise<HttpResponse<OtherProperty[], any>> {
    return this.request<OtherProperty[], any>({
      path: `/models/properties/charac/other`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags PropertiesController
   * @name GetCharacSpellmodel
   * @request GET:/models/properties/charac/spellmodel
   * @response `200` `(SpellModelProperty)[]` Success
   */
  public getCharacSpellmodel(params: RequestParams = {}): Promise<HttpResponse<SpellModelProperty[], any>> {
    return this.request<SpellModelProperty[], any>({
      path: `/models/properties/charac/spellmodel`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags PropertiesController
   * @name GetCharacSpell
   * @request GET:/models/properties/charac/spell
   * @response `200` `(SpellProperty)[]` Success
   */
  public getCharacSpell(params: RequestParams = {}): Promise<HttpResponse<SpellProperty[], any>> {
    return this.request<SpellProperty[], any>({
      path: `/models/properties/charac/spell`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags PropertiesController
   * @name GetCharacState
   * @request GET:/models/properties/charac/state
   * @response `200` `(State)[]` Success
   */
  public getCharacState(params: RequestParams = {}): Promise<HttpResponse<State[], any>> {
    return this.request<State[], any>({
      path: `/models/properties/charac/state`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags PropertiesController
   * @name GetCharacStatusmodel
   * @request GET:/models/properties/charac/statusmodel
   * @response `200` `(StatusModelProperty)[]` Success
   */
  public getCharacStatusmodel(params: RequestParams = {}): Promise<HttpResponse<StatusModelProperty[], any>> {
    return this.request<StatusModelProperty[], any>({
      path: `/models/properties/charac/statusmodel`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
}
