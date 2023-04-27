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

export interface ICreatureModel {
  skins?: IIDIEntitySet;
  baseStats?: IID;
  growthStats?: IID;
  baseSpells?: IIDIEntitySet;
  baseStatusPassives?: IIDIEntitySet;
  entityUid?: IID;
}

export type IID = object;

export interface IIDIEntitySet {
  allowDuplicates?: boolean;
  values?: IID[] | null;
  entityUid?: IID;
}

export interface WeatherForecast {
  /** @format date */
  date?: string;
  /** @format int32 */
  temperatureC?: number;
  /** @format int32 */
  temperatureF?: number;
  summary?: string | null;
}
