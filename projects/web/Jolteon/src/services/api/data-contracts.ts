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

/** @format  */
export enum CharacteristicCategory {
  Resource = "Resource",
  Affinity = "Affinity",
  Resistance = "Resistance",
  State = "State",
  Contextual = "Contextual",
  Other = "Other",
  Spell = "Spell",
  SpellModel = "SpellModel",
  Status = "Status",
}

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

export interface ObjectId {
  /** @format int32 */
  timestamp?: number;
  /**
   * @deprecated
   * @format int32
   */
  machine?: number;
  /**
   * @deprecated
   * @format int32
   */
  pid?: number;
  /**
   * @deprecated
   * @format int32
   */
  increment?: number;
  /** @format date-time */
  creationTime?: string;
}

export interface ShopCurrency {
  mongoID?: ObjectId;
  /** @format int32 */
  amount?: number;
  /** @format int32 */
  price?: number;
  isAvailable?: boolean;
}

export interface ShopProduct {
  mongoID?: ObjectId;
  modelID?: IID;
  /** @format int32 */
  currencyPrice?: number;
  isAvailable?: boolean;
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
