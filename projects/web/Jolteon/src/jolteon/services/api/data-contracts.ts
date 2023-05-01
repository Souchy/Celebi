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

export interface AccountInfo {
  accessByIp?: IpAccess[] | null;
  displayName?: string | null;
  name?: string | null;
  familyName?: string | null;
  picture?: string | null;
  /** @format int32 */
  currency?: number;
  ownedModels?: IID[] | null;
  transactions?: string[] | null;
}

/** @format  */
export enum ActorType {
  Source = "Source",
  Target = "Target",
}

/** @format  */
export enum BoardTargetType {
  Cell = "Cell",
  Creature = "Creature",
}

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

export interface CharacteristicId {
  /** @format int32 */
  id?: number;
}

export interface CharacteristicType {
  category?: CharacteristicCategory;
  /** @format int32 */
  localId?: number;
  baseName?: string | null;
  conditions?: ICondition[] | null;
  statValueType?: StatValueType;
  id?: CharacteristicId;
  nameID?: IID;
}

/** @format  */
export enum ConditionComparatorType {
  EQ = "EQ",
  NE = "NE",
  GT = "GT",
  GE = "GE",
  LT = "LT",
  LE = "LE",
}

/** @format  */
export enum ConditionGroupType {
  AND = "AND",
  OR = "OR",
}

/** @format  */
export enum Direction8Type {
  Top = "top",
  Topright = "topright",
  Right = "right",
  Bottomright = "bottomright",
  Bottom = "bottom",
  Bottomleft = "bottomleft",
  Left = "left",
  Topleft = "topleft",
}

/** @format  */
export enum Direction9Type {
  Center = "center",
  Top = "top",
  Topright = "topright",
  Right = "right",
  Bottomright = "bottomright",
  Bottom = "bottom",
  Bottomleft = "bottomleft",
  Left = "left",
  Topleft = "topleft",
}

export interface ICondition {
  actorType?: ActorType;
  comparator?: ConditionComparatorType;
  groupType?: ConditionGroupType;
  children?: IConditionIEntityList;
}

export interface IConditionIEntityList {
  allowDuplicates?: boolean;
  values?: ICondition[] | null;
  entityUid?: IID;
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

export interface IpAccess {
  ipAddress?: string | null;
  /** @format date-time */
  lastAccess?: string;
  verified?: boolean;
  refreshToken?: string | null;
}

/** @format  */
export enum MoveType {
  Walk = "Walk",
  Translate = "Translate",
  Teleport = "Teleport",
  Carry = "Carry",
  Throw = "Throw",
}

/** @format  */
export enum ResourceEnum {
  Life = "Life",
  Mana = "Mana",
  Movement = "Movement",
  Summon = "Summon",
  Rage = "Rage",
  Shield = "Shield",
}

/** @format  */
export enum ResourceProperty {
  InitialMax = "InitialMax",
  Current = "Current",
  Max = "Max",
  Missing = "Missing",
  Regen = "Regen",
  Percent = "Percent",
  MissingPercent = "MissingPercent",
}

export interface ShopCurrency {
  mongoID?: string;
  /** @format int32 */
  amount?: number;
  /** @format int32 */
  price?: number;
  isAvailable?: boolean;
}

export interface ShopProduct {
  mongoID?: string;
  modelID?: IID;
  /** @format int32 */
  currencyPrice?: number;
  isAvailable?: boolean;
  /** @format int32 */
  limitPerAccount?: number;
}

/** @format  */
export enum StatValueType {
  Simple = "Simple",
  Bool = "Bool",
  Variant = "Variant",
}

/** @format  */
export enum StatusPriorityType {
  System = "System",
  Passive = "Passive",
  Status = "Status",
}

/** @format  */
export enum TeamRelationType {
  Ally = "Ally",
  Enemy = "Enemy",
  Self = "Self",
}

/** @format  */
export enum TowerDirectionType {
  Up = "up",
  Down = "down",
  Both = "both",
}

/** @format  */
export enum TriggerType {
  OnFightStart = "OnFightStart",
  OnFightEnd = "OnFightEnd",
  OnRoundStart = "OnRoundStart",
  OnRoundEnd = "OnRoundEnd",
  OnTurnStart = "OnTurnStart",
  OnTurnEnd = "OnTurnEnd",
  OnTurnPass = "OnTurnPass",
  OnCreatureSpellCast = "OnCreatureSpellCast",
  OnEffect = "OnEffect",
  OnCreatureWalkEnterCell = "OnCreatureWalkEnterCell",
  OnCreatureWalkExitCell = "OnCreatureWalkExitCell",
  OnCreatureWalkStopCell = "OnCreatureWalkStopCell",
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

/** @format  */
export enum ZoneType {
  Point = "point",
  Line = "line",
  Diagonal = "diagonal",
  Cross = "cross",
  Xcross = "xcross",
  Star = "star",
  CrossHalf = "crossHalf",
  XcrossHalf = "xcrossHalf",
  Circle = "circle",
  CircleHalf = "circleHalf",
  Square = "square",
  SquareHalf = "squareHalf",
  Rectangle = "rectangle",
  Ellipse = "ellipse",
  EllipseHalf = "ellipseHalf",
}
