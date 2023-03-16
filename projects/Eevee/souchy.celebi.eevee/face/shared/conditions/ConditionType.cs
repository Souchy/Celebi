namespace souchy.celebi.eevee.face.shared.conditions
{
    public enum ConditionType
    {
        CreatureModel, // modelid = val
        Distance, // dist = val
        Height, // height = val
        Stat, // statId = val1 && statValue = val2 (can be int or bool)
        Status, // statusId = val1 && statId = val2 && statValue = val3
        Spell,  // spellId = val1 && statId = val2 && statValue = val3
        Team, // teamEnumId = val
        Moment, // momentEnumId = val
        LineOfSight, // haveLos = val
    }
}