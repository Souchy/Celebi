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

export interface Affinity {
  element?: ElementType;
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
export enum BoardTargetType {
  Cell = "Cell",
  Creature = "Creature",
}

export interface BooleanIValue {
  value?: boolean;
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

export interface CharacteristicIdIStatKeyValuePair {
  key?: CharacteristicId;
  value?: IStat;
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

export interface Contextual {
  category?: CharacteristicCategory;
  /** @format int32 */
  localId?: number;
  baseName?: string | null;
  conditions?: ICondition[] | null;
  statValueType?: StatValueType;
  id?: CharacteristicId;
  nameID?: IID;
}

export interface DeleteResult {
  /** @format int64 */
  deletedCount?: number;
  isAcknowledged?: boolean;
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

/** @format  */
export enum ElementType {
  None = "None",
  Water = "Water",
  Fire = "Fire",
  Earth = "Earth",
  Air = "Air",
  True = "True",
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
  nameId?: IID;
  descriptionId?: IID;
  skins?: IIDIEntitySet;
  baseStats?: IID;
  growthStats?: IID;
  baseSpells?: IIDIEntitySet;
  baseStatusPassives?: IIDIEntitySet;
  entityUid?: IID;
}

export interface IEffect {
  sourceCondition?: ICondition;
  targetFilter?: ICondition;
  zone?: IZone;
  triggers?: ITriggerIEntityList;
  modelUid?: IID;
  entityUid?: IID;
  fightUid?: IID;
  effectIds?: IIDIEntityList;
}

export type IID = object;

export interface IIDIEntityList {
  allowDuplicates?: boolean;
  values?: IID[] | null;
  entityUid?: IID;
}

export interface IIDIEntitySet {
  allowDuplicates?: boolean;
  values?: IID[] | null;
  entityUid?: IID;
}

export interface ISpellModel {
  nameId?: IID;
  descriptionId?: IID;
  sourceCondition?: ICondition;
  targetFilter?: ICondition;
  costs?: Record<string, number | null>;
  properties?: SpellProperties;
  rangeZoneMin?: IZone;
  rangeZoneMax?: IZone;
  entityUid?: IID;
  effectIds?: IIDIEntityList;
}

export interface IStat {
  statId?: CharacteristicId;
  entityUid?: IID;
}

export interface IStats {
  entityUid?: IID;
  keys?: CharacteristicId[] | null;
  values?: IStat[] | null;
  pairs?: CharacteristicIdIStatKeyValuePair[] | null;
}

export interface IStatusModel {
  nameId?: IID;
  descriptionId?: IID;
  delay?: Int32IValue;
  duration?: Int32IValue;
  canBeUnbewitched?: BooleanIValue;
  priority?: StatusPriorityTypeIValue;
  entityUid?: IID;
  effectIds?: IIDIEntityList;
}

export interface IStringEntity {
  value?: string | null;
  entityUid?: IID;
}

export type IStringEntityFilterDefinition = object;

export interface ITrigger {
  type?: TriggerType;
  orderType?: TriggerOrderType;
  zone?: IZone;
  triggererFilter?: ICondition;
  holderCondition?: ICondition;
}

export interface ITriggerIEntityList {
  allowDuplicates?: boolean;
  values?: ITrigger[] | null;
  entityUid?: IID;
}

export interface IVector2 {
  /** @format int32 */
  x?: number;
  /** @format int32 */
  z?: number;
}

export interface IVector3 {
  /** @format int32 */
  x?: number;
  /** @format int32 */
  z?: number;
  /** @format int32 */
  y?: number;
}

export interface IVector3IValue {
  value?: IVector3;
}

export interface IZone {
  zoneType?: ZoneTypeIValue;
  size?: IVector3IValue;
  negative?: boolean;
  worldOrigin?: ActorType;
  worldOffset?: IVector2;
  localOrigin?: Direction9Type;
  rotation?: Rotation4Type;
  canRotate?: BooleanIValue;
  /** @format int32 */
  sizeIndexExtendFromSource?: number;
  children?: IZoneIEntityList;
}

export interface IZoneIEntityList {
  allowDuplicates?: boolean;
  values?: IZone[] | null;
  entityUid?: IID;
}

export interface Int32IValue {
  /** @format int32 */
  value?: number;
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

export interface OtherProperty {
  category?: CharacteristicCategory;
  /** @format int32 */
  localId?: number;
  baseName?: string | null;
  conditions?: ICondition[] | null;
  statValueType?: StatValueType;
  id?: CharacteristicId;
  nameID?: IID;
}

export interface ReplaceOneResult {
  isAcknowledged?: boolean;
  isModifiedCountAvailable?: boolean;
  /** @format int64 */
  matchedCount?: number;
  /** @format int64 */
  modifiedCount?: number;
  upsertedId?: string | null;
}

export interface Resistance {
  element?: ElementType;
  category?: CharacteristicCategory;
  /** @format int32 */
  localId?: number;
  baseName?: string | null;
  conditions?: ICondition[] | null;
  statValueType?: StatValueType;
  id?: CharacteristicId;
  nameID?: IID;
}

export interface Resource {
  resType?: ResourceEnum;
  resProp?: ResourceProperty;
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

/** @format  */
export enum Rotation4Type {
  Top = "top",
  Right = "right",
  Bottom = "bottom",
  Left = "left",
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

export interface SpellModelProperty {
  category?: CharacteristicCategory;
  /** @format int32 */
  localId?: number;
  baseName?: string | null;
  conditions?: ICondition[] | null;
  statValueType?: StatValueType;
  id?: CharacteristicId;
  nameID?: IID;
}

export interface SpellProperties {
  maxCharges?: Int32IValue;
  maxCastsPerTurn?: Int32IValue;
  maxCastsPerTarget?: Int32IValue;
  cooldownInitial?: Int32IValue;
  cooldownGlobal?: Int32IValue;
  cooldown?: Int32IValue;
  minRange?: Int32IValue;
  maxRange?: Int32IValue;
  castInDiagonal?: BooleanIValue;
  castInLine?: BooleanIValue;
  needLos?: BooleanIValue;
}

export interface SpellProperty {
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
export enum StatValueType {
  Simple = "Simple",
  Bool = "Bool",
  Variant = "Variant",
}

export interface State {
  category?: CharacteristicCategory;
  /** @format int32 */
  localId?: number;
  baseName?: string | null;
  conditions?: ICondition[] | null;
  statValueType?: StatValueType;
  id?: CharacteristicId;
  nameID?: IID;
}

export interface StatusModelProperty {
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
export enum StatusPriorityType {
  System = "System",
  Passive = "Passive",
  Status = "Status",
}

export interface StatusPriorityTypeIValue {
  value?: StatusPriorityType;
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
export enum TriggerOrderType {
  Before = "Before",
  Apply = "Apply",
  After = "After",
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

export interface ZoneTypeIValue {
  value?: ZoneType;
}
