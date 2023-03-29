export enum CharacteristicCategory {
    Resource = 1,
    Affinity = 2,
    Resistance = 3,
    State = 4,
    Contextual = 5,
    Other = 6,
    Spell = 7,
    SpellModel = 8,
    Status = 9
}

export enum ResourceEnum {
    Life,
    Mana,
    Movement,
    Summon,
    Rage
}

export enum ResourceProperty {
    Current,
    Max,
    Missing,
    Regen,
    Percent,
    MissingPercent
}

export interface CharacteristicTypePropertiesAttribute extends Attribute {
    valueType: StatValueType;
    element: ElementType;
}

export interface ElementTypePropertiesAttribute extends Attribute {
    element: ElementType;
}