import { ActorType, Affinity, AffinityTypes, Contextual, ContextualTypes, Direction8Type, Direction9Type, EffT, ElementType, OtherProperty, OtherPropertyTypes, 
    Resistance, ResistanceTypes, Resource, ResourceProperty, ResourceTypes, Rotation4Type, SpellModelProperty, 
    SpellModelPropertyTypes, SpellProperty, SpellPropertyTypes, State, StateTypes, StatusProperty, StatusPropertyTypes, ZoneType } from "./services/api/data-contracts";


export class Constants {
    public static readonly serverUrl = "https://localhost:7295";
    public static readonly googleClientId = "850322629277-c9fu1umd1dlk7tjv325u6s33g32fb0ea.apps.googleusercontent.com";
}

export class Characteristics {
    
    public static readonly affinities: Affinity[] = Object.values(AffinityTypes)
    public static readonly resistances: Resistance[] = Object.values(ResistanceTypes);
    public static readonly resources: Resource[] = Object.values(ResourceTypes);
    public static readonly resourcesCreatureModel: Resource[] = Object.values(ResourceTypes).filter((r: Resource) => r.resProp == ResourceProperty.InitialMax || r.resProp == ResourceProperty.Regen);
    public static readonly resourcesSpellCosts: Resource[] = Object.values(ResourceTypes).filter((r: Resource) => r.resProp == ResourceProperty.Current);
    public static readonly states: State[] = Object.values(StateTypes);
    public static readonly others: OtherProperty[] = Object.values(OtherPropertyTypes);
    public static readonly contextuals: Contextual[] = Object.values(ContextualTypes);
    
    public static readonly spells: SpellProperty[] = Object.values(SpellPropertyTypes)
    public static readonly spellModels: SpellModelProperty[] = Object.values(SpellModelPropertyTypes)
    public static readonly statusModels: StatusProperty[] = Object.values(StatusPropertyTypes)

    public static readonly all: 
          (Affinity | Resistance | Resource | State | OtherProperty | Contextual | SpellProperty | SpellModelProperty | StatusProperty)[]
        = [ ...Characteristics.affinities, ...Characteristics.resistances, ...Characteristics.resources,
            ...Characteristics.states, ...Characteristics.others, ...Characteristics.contextuals,
            ...Characteristics.spells, ...Characteristics.spellModels , ...Characteristics.statusModels
          ]

    public static getCharac(characId: string) {
        return Characteristics.all.find(c => c.id == characId);
    }

}

export class Enums {
    public static readonly elements = Object.keys(ElementType).filter(k => isNaN(+k));
    public static readonly effectTypes = Object.keys(EffT).filter(k => isNaN(+k));
    public static readonly zoneTypes = Object.keys(ZoneType).filter(k => isNaN(+k));
    public static readonly direction8 = Object.keys(Direction8Type).filter(k => isNaN(+k));
    public static readonly direction9 = Object.keys(Direction9Type).filter(k => isNaN(+k));
    public static readonly rotation4 =  Object.keys(Rotation4Type).filter(k => isNaN(+k));
    public static readonly actors = Object.keys(ActorType).filter(k => isNaN(+k));
}

export class Effects {
    // public static readonly schemas: EffectSchemaTypes;
}
