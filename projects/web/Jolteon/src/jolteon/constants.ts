import { Affinity, AffinityTypes, Contextual, ContextualTypes, OtherProperty, OtherPropertyTypes, 
    Resistance, ResistanceTypes, Resource, ResourceProperty, ResourceTypes, SpellModelProperty, 
    SpellModelPropertyTypes, SpellProperty, SpellPropertyTypes, State, StateTypes, StatusModelProperty, StatusModelPropertyTypes } from "./services/api/data-contracts";


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
    public static readonly statusModels: StatusModelProperty[] = Object.values(StatusModelPropertyTypes)

    public static readonly all: 
          (Affinity | Resistance | Resource | State | OtherProperty | Contextual | SpellProperty | SpellModelProperty | StatusModelProperty)[]
        = [ ...Characteristics.affinities, ...Characteristics.resistances, ...Characteristics.resources,
            ...Characteristics.states, ...Characteristics.others, ...Characteristics.contextuals,
            ...Characteristics.spells, ...Characteristics.spellModels, ...Characteristics.statusModels
          ]

    public static getCharac(characId: string) {
        return Characteristics.all.find(c => c.id == characId);
    }


}
