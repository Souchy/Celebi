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
  ActorType,
  BoardTargetType,
  CharacteristicCategory,
  Direction8Type,
  Direction9Type,
  MoveType,
  ResourceEnum,
  ResourceProperty,
  StatusPriorityType,
  TeamRelationType,
  TowerDirectionType,
  TriggerType,
  ZoneType,
} from "./data-contracts";
import { HttpClient, HttpResponse, RequestParams } from "./http-client";

export class EnumsController<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetCharacCharacteristicCategory
   * @request GET:/models/enums/charac/characteristicCategory
   * @response `200` `(CharacteristicCategory)[]` Success
   */
  public getCharacCharacteristicCategory(
    params: RequestParams = {},
  ): Promise<HttpResponse<CharacteristicCategory[], any>> {
    return this.request<CharacteristicCategory[], any>({
      path: `/models/enums/charac/characteristicCategory`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetCharacResourceType
   * @request GET:/models/enums/charac/resourceType
   * @response `200` `(ResourceProperty)[]` Success
   */
  public getCharacResourceType(params: RequestParams = {}): Promise<HttpResponse<ResourceProperty[], any>> {
    return this.request<ResourceProperty[], any>({
      path: `/models/enums/charac/resourceType`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetCharacResourceProperty
   * @request GET:/models/enums/charac/resourceProperty
   * @response `200` `(ResourceEnum)[]` Success
   */
  public getCharacResourceProperty(params: RequestParams = {}): Promise<HttpResponse<ResourceEnum[], any>> {
    return this.request<ResourceEnum[], any>({
      path: `/models/enums/charac/resourceProperty`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetActorType
   * @request GET:/models/enums/actorType
   * @response `200` `(ActorType)[]` Success
   */
  public getActorType(params: RequestParams = {}): Promise<HttpResponse<ActorType[], any>> {
    return this.request<ActorType[], any>({
      path: `/models/enums/actorType`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetBoardTargetType
   * @request GET:/models/enums/boardTargetType
   * @response `200` `(BoardTargetType)[]` Success
   */
  public getBoardTargetType(params: RequestParams = {}): Promise<HttpResponse<BoardTargetType[], any>> {
    return this.request<BoardTargetType[], any>({
      path: `/models/enums/boardTargetType`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetMoveType
   * @request GET:/models/enums/moveType
   * @response `200` `(MoveType)[]` Success
   */
  public getMoveType(params: RequestParams = {}): Promise<HttpResponse<MoveType[], any>> {
    return this.request<MoveType[], any>({
      path: `/models/enums/moveType`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetTeamRelationType
   * @request GET:/models/enums/teamRelationType
   * @response `200` `(TeamRelationType)[]` Success
   */
  public getTeamRelationType(params: RequestParams = {}): Promise<HttpResponse<TeamRelationType[], any>> {
    return this.request<TeamRelationType[], any>({
      path: `/models/enums/teamRelationType`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetZoneType
   * @request GET:/models/enums/zoneType
   * @response `200` `(ZoneType)[]` Success
   */
  public getZoneType(params: RequestParams = {}): Promise<HttpResponse<ZoneType[], any>> {
    return this.request<ZoneType[], any>({
      path: `/models/enums/zoneType`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetTriggerType
   * @request GET:/models/enums/triggerType
   * @response `200` `(TriggerType)[]` Success
   */
  public getTriggerType(params: RequestParams = {}): Promise<HttpResponse<TriggerType[], any>> {
    return this.request<TriggerType[], any>({
      path: `/models/enums/triggerType`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetTowerDirectionType
   * @request GET:/models/enums/towerDirectionType
   * @response `200` `(TowerDirectionType)[]` Success
   */
  public getTowerDirectionType(params: RequestParams = {}): Promise<HttpResponse<TowerDirectionType[], any>> {
    return this.request<TowerDirectionType[], any>({
      path: `/models/enums/towerDirectionType`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetStatusPriorityType
   * @request GET:/models/enums/statusPriorityType
   * @response `200` `(StatusPriorityType)[]` Success
   */
  public getStatusPriorityType(params: RequestParams = {}): Promise<HttpResponse<StatusPriorityType[], any>> {
    return this.request<StatusPriorityType[], any>({
      path: `/models/enums/statusPriorityType`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetDirection8Type
   * @request GET:/models/enums/direction8Type
   * @response `200` `(Direction8Type)[]` Success
   */
  public getDirection8Type(params: RequestParams = {}): Promise<HttpResponse<Direction8Type[], any>> {
    return this.request<Direction8Type[], any>({
      path: `/models/enums/direction8Type`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
  /**
   * No description
   *
   * @tags EnumsController
   * @name GetDirection9Type
   * @request GET:/models/enums/direction9Type
   * @response `200` `(Direction9Type)[]` Success
   */
  public getDirection9Type(params: RequestParams = {}): Promise<HttpResponse<Direction9Type[], any>> {
    return this.request<Direction9Type[], any>({
      path: `/models/enums/direction9Type`,
      method: "GET",
      format: "json",
      ...params,
    });
  }
}
