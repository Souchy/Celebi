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
  nameModelUid?: IID;
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

export interface ConsumableDrop {
  productId?: string;
  /** @format int32 */
  weight?: number;
}

export interface ConsumableProduct {
  type?: ConsumableType;
  drops?: ConsumableDrop[] | null;
  _id?: string;
  /** @format int32 */
  currency?: number;
  isAvailableInShop?: boolean;
  isAvailableInDrop?: boolean;
  /** @format int32 */
  dropWeight?: number;
  limitType?: LimitType;
  /** @format int32 */
  limitPerAccount?: number;
}

/** @format  */
export enum ConsumableType {
  All = "All",
  Random = "Random",
}

export interface Contextual {
  category?: CharacteristicCategory;
  /** @format int32 */
  localId?: number;
  baseName?: string | null;
  conditions?: ICondition[] | null;
  statValueType?: StatValueType;
  id?: CharacteristicId;
  nameModelUid?: IID;
}

export interface CreatureModel {
  entityUid?: string;
  modelUid?: IID;
  nameId?: string;
  descriptionId?: string;
  skins?: ObjectIdIEntitySet;
  baseStats?: string;
  growthStats?: string;
  baseSpells?: IIDIEntitySet;
  baseStatusPassives?: IIDIEntitySet;
}

export interface CurrencyProduct {
  /** @format float */
  price?: number;
  _id?: string;
  /** @format int32 */
  currency?: number;
  isAvailableInShop?: boolean;
  isAvailableInDrop?: boolean;
  /** @format int32 */
  dropWeight?: number;
  limitType?: LimitType;
  /** @format int32 */
  limitPerAccount?: number;
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

export interface Effect {
  entityUid?: string;
  modelUid?: IID;
  fightUid?: string;
  boardTargetType?: BoardTargetType;
  sourceCondition?: ICondition;
  targetFilter?: ICondition;
  zone?: IZone;
  triggers?: ITriggerIEntityList;
  effectIds?: ObjectIdIEntityList;
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

/** @format  */
export enum I18NType {
  Fr = "fr",
  En = "en",
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
  entityUid?: string;
}

export interface ICreatureModel {
  nameId?: string;
  descriptionId?: string;
  skins?: ObjectIdIEntitySet;
  baseStats?: string;
  growthStats?: string;
  baseSpells?: IIDIEntitySet;
  baseStatusPassives?: IIDIEntitySet;
  modelUid?: IID;
  entityUid?: string;
}

export type ICreatureModelFilterDefinition = object;

export interface IEffect {
  sourceCondition?: ICondition;
  targetFilter?: ICondition;
  zone?: IZone;
  triggers?: ITriggerIEntityList;
  modelUid?: IID;
  entityUid?: string;
  fightUid?: string;
  effectIds?: ObjectIdIEntityList;
}

export interface IID {
  value?: string;
}

export interface IIDIEntitySet {
  allowDuplicates?: boolean;
  values?: IID[] | null;
  entityUid?: string;
}

export interface ISpellModel {
  nameId?: string;
  descriptionId?: string;
  sourceCondition?: ICondition;
  targetFilter?: ICondition;
  costs?: Record<string, number | null>;
  stats?: string;
  rangeZoneMin?: IZone;
  rangeZoneMax?: IZone;
  modelUid?: IID;
  entityUid?: string;
  effectIds?: ObjectIdIEntityList;
}

export interface IStat {
  statId?: CharacteristicId;
  entityUid?: string;
}

export interface IStats {
  entityUid?: string;
  keys?: CharacteristicId[] | null;
  values?: IStat[] | null;
  pairs?: CharacteristicIdIStatKeyValuePair[] | null;
}

export interface IStatusModel {
  nameId?: string;
  descriptionId?: string;
  delay?: Int32IValue;
  duration?: Int32IValue;
  canBeUnbewitched?: BooleanIValue;
  priority?: StatusPriorityTypeIValue;
  modelUid?: IID;
  entityUid?: string;
  effectIds?: ObjectIdIEntityList;
}

export interface IStringEntity {
  value?: string | null;
  modelUid?: IID;
  entityUid?: string;
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
  entityUid?: string;
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
  entityUid?: string;
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
export enum LimitType {
  Owned = "Owned",
  Bought = "Bought",
}

export interface ModelProduct {
  modelID?: IID;
  _id?: string;
  /** @format int32 */
  currency?: number;
  isAvailableInShop?: boolean;
  isAvailableInDrop?: boolean;
  /** @format int32 */
  dropWeight?: number;
  limitType?: LimitType;
  /** @format int32 */
  limitPerAccount?: number;
}

/** @format  */
export enum MoveType {
  Walk = "Walk",
  Translate = "Translate",
  Teleport = "Teleport",
  Carry = "Carry",
  Throw = "Throw",
}

export interface ObjectIdIEntityList {
  allowDuplicates?: boolean;
  values?: string[] | null;
  entityUid?: string;
}

export interface ObjectIdIEntitySet {
  allowDuplicates?: boolean;
  values?: string[] | null;
  entityUid?: string;
}

export interface OtherProperty {
  category?: CharacteristicCategory;
  /** @format int32 */
  localId?: number;
  baseName?: string | null;
  conditions?: ICondition[] | null;
  statValueType?: StatValueType;
  id?: CharacteristicId;
  nameModelUid?: IID;
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
  nameModelUid?: IID;
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
  nameModelUid?: IID;
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

export interface SpellModel {
  entityUid?: string;
  modelUid?: IID;
  nameId?: string;
  descriptionId?: string;
  sourceCondition?: ICondition;
  targetFilter?: ICondition;
  costs?: Record<string, number | null>;
  stats?: string;
  effectIds?: ObjectIdIEntityList;
  rangeZoneMin?: IZone;
  rangeZoneMax?: IZone;
}

export interface SpellModelProperty {
  category?: CharacteristicCategory;
  /** @format int32 */
  localId?: number;
  baseName?: string | null;
  conditions?: ICondition[] | null;
  statValueType?: StatValueType;
  id?: CharacteristicId;
  nameModelUid?: IID;
}

export interface SpellProperty {
  category?: CharacteristicCategory;
  /** @format int32 */
  localId?: number;
  baseName?: string | null;
  conditions?: ICondition[] | null;
  statValueType?: StatValueType;
  id?: CharacteristicId;
  nameModelUid?: IID;
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
  nameModelUid?: IID;
}

export interface StatusModel {
  entityUid?: string;
  modelUid?: IID;
  nameId?: string;
  descriptionId?: string;
  delay?: Int32IValue;
  duration?: Int32IValue;
  canBeUnbewitched?: BooleanIValue;
  priority?: StatusPriorityTypeIValue;
  effectIds?: ObjectIdIEntityList;
}

export interface StatusModelProperty {
  category?: CharacteristicCategory;
  /** @format int32 */
  localId?: number;
  baseName?: string | null;
  conditions?: ICondition[] | null;
  statValueType?: StatValueType;
  id?: CharacteristicId;
  nameModelUid?: IID;
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

export interface StringEntity {
  entityUid?: string;
  modelUid?: IID;
  value?: string | null;
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
