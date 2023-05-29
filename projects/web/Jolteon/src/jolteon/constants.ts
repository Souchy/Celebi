import { TargetSamplingType } from "./services/api/data-contracts";
import { ActorType, Affinity, AffinityTypes, Contextual, ContextualTypes, Direction8Type, Direction9Type, EffT, ElementType, OtherProperty, OtherPropertyTypes, 
    Resistance, ResistanceTypes, Resource, ResourceProperty, ResourceTypes, Rotation4Type, SchemaDescription, SpellModelProperty, 
    SpellModelPropertyTypes, SpellProperty, SpellPropertyTypes, State, StateTypes,
    StatusContainerProperty, StatusContainerPropertyTypes, StatusInstanceProperty, StatusInstancePropertyTypes,
    ZoneType
} from "./services/api/data-contracts";


export class Constants {
    public static readonly serverUrl = "https://localhost:7295";
    public static readonly googleClientId = "850322629277-c9fu1umd1dlk7tjv325u6s33g32fb0ea.apps.googleusercontent.com";

    public static readonly MAX_INT = Math.pow(2, 31) - 1;
    public static readonly MIN_INT = -Math.pow(2, 31);
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
    public static readonly statusContainers: StatusContainerProperty[] = Object.values(StatusContainerPropertyTypes)
    public static readonly statusInstances: StatusInstanceProperty[] = Object.values(StatusInstancePropertyTypes)

    public static readonly all: 
          (Resource | Affinity | Resistance | State | OtherProperty | Contextual | SpellModelProperty | SpellProperty | StatusContainerProperty | StatusInstanceProperty)[]
        = [ ...Characteristics.resources, ...Characteristics.affinities, ...Characteristics.resistances, 
            ...Characteristics.states, ...Characteristics.others, ...Characteristics.contextuals,
            ...Characteristics.spellModels , ...Characteristics.spells, ...Characteristics.statusContainers, ...Characteristics.statusInstances
          ]

    public static readonly sectioned: 
        (Resource[] | Affinity[] | Resistance[] | State[] | OtherProperty[] | Contextual[] | SpellModelProperty[] | SpellProperty[] | StatusContainerProperty[] | StatusInstanceProperty[])[]
        = [ Characteristics.resources, Characteristics.affinities, Characteristics.resistances, 
            Characteristics.states, Characteristics.others, Characteristics.contextuals,
            Characteristics.spellModels , Characteristics.spells, Characteristics.statusContainers, Characteristics.statusInstances
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
    public static readonly actorTypes = Object.keys(ActorType).filter(k => isNaN(+k));
    public static readonly samplingTypes = Object.keys(TargetSamplingType).filter(k => isNaN(+k));
}

export class Effects {
    // public static readonly schemas: EffectSchemaTypes;
    public static schemas: SchemaDescription[] = [];
}
