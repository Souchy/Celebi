

export class TeamFilter {
    public allowAlly: boolean = true
    public allowEnemy: boolean = true
    public allowSelf: boolean = true
}
export class CreatureFilter {
    public modelsAllowed: [] = []
    public modelsDisallowed: [] = []
    public modelAllowedSummonedBy: [] = []
    public modelDisallowedSummonedBy: [] = []

    public allowSelfSummons: boolean = true
    public allowSummoners: boolean = true
    public allowSummons: boolean = true
}
export class CellFilter {
    public allowEmpty: boolean = true
    public allowOccupied: boolean = true
}


export class TargetFilter {
    public team: TeamFilter = new TeamFilter()
    public creature: CreatureFilter = new CreatureFilter()
    public cell: CellFilter = new CellFilter()

    public filter(entity): boolean {
        return true;
    }
}
