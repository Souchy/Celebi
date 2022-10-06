enum EffectType {
    Damage, Heal,
    ApReduce, ApIncrease,
    MpReduce, MpIncrease,
    Push, Pull,
    Teleport, Swap,
    Random,

}

class Effect {
    id: string,
    children: Effect[]
    targetMask: string

}