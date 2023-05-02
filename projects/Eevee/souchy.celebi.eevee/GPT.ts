


class Condition {

}
class ConditionDistanceCasterTarget extends Condition {
    value: number
}


class Characteristic {
    conditions: Condition[]
    public constructor(conditions: Condition[]) { }
}


class Characteristics {
    meleeDmg = new Characteristic( [ new ConditionDistanceCasterTarget() ] )

    //new Characteristic();

    conds: Condition[] = [ new ConditionDistanceCasterTarget() ]
}


class Damage {
    public apply(caster, target) {
        let stats = caster.getStats();
    }
}

class Creature {
    public getStats() {

    }
}