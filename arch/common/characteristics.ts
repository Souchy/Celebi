
export enum StateType {
    flying,
    underground,
    wet,
    burning, // hot
    grounded,
    shocked,

    carrying,
    carried,

    invisible,
    ghosted, // phasing, // ghosted, // is 30% opacity, unlocks line of sight, but blocks movement
    
    pacifist,

    unmovable, // heavy + rooted + gravity
    heavy, // cant carry
    rooted, // cant dash/push/pull
    gravity, // can't teleport
}
export const stateTypes = Object.values(StateType).filter(item => isNaN(Number(item))).map(item => item as StateType)

export enum ResourceType {
    life,
    ap,
    mp,
    special,
}
export const resourceTypes = Object.values(ResourceType).filter(item => isNaN(Number(item))).map(item => item as ResourceType)

export enum ElementType {
    fire,
    water,
    air,
    earth,
}
export const elementTypes = Object.values(ElementType).filter(item => isNaN(Number(item))).map(item => item as ElementType)


export enum CharacteristicType {
    life,
    life_max,
    life_regen, // -1 = full every turn, 0 = no regen, 1 = 1/turn, 0.25 = 1 per 4 turns // string to parse for different regens

    ap,
    ap_max,
    ap_regen,

    mp,
    mp_max,
    mp_regen,

    special,
    special_max,
    special_regen, 

    speed,
    range,
    summons,
    maxSummons,

    heal_affinity,
    heal_flat,
    heal_resistance,

    glyph_affinity,
    glyph_flat,
    glyph_resistance,

    trap_affinity,
    trap_flat,
    trap_resistance,
    
    poison_affinity,
    poison_damaage,
    poison_resistance,
    
    global_affinity,
    fire_affinity,
    water_affinity,
    earth_affinity,
    air_affinity,

    global_flat,
    fire_flat,
    water_flat,
    earth_flat,
    air_flat,
    
    global_resistance,
    fire_resistance,
    water_resistance,
    earth_resistance,
    air_resistance
}
export const characteristicTypes = Object.values(CharacteristicType).filter(item => isNaN(Number(item))).map(item => item as CharacteristicType)
