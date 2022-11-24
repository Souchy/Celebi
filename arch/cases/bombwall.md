
Spell {
    name: tornabomb
    Effects [
        {
            Summon Bomb
        }

    ]
}




Monster {
    name: tornabomb
    Bomb: {
        spellIds: [
            0
        ],
        passiveSpellIds: [
            189, 
            780
        ]
    }
}



Spell: {
    id: 189
    name: bombPassiveSpellApplyStatus
    Effects: [
        {
            ApplyStatus: {
                target: self,
                effects: [
                    {
                        subSpell: 781,
                        trigger: beforeDeath
                    },
                    {
                        subSpell: 782
                        trigger: afterMove
                    },
                    {
                        subSpell: 780
                        trigger: onMove
                    }
                ],
                dispellable: never
            }
        }
    ]
}

Spell {
    id: 782
    name: updateWalls
    Effects: [
        {

        },
        {
            ApplyStatusWallColleagues
            aoe: Cross7
        }
    ]
}

Status {
    name: wallColleagues
    effects: [
        {
            effectId: 54
            value: colleagueId
        }
    ]
    stats: [
        0
    ]
}

Effect {
    id: 54
    hint: WallColleague
    valueMin: colleagueId
    valueMax: 0
}










Spell {
    id: 781
    name: destroyWall
    Effects: [
        {
            aoe: Cross7
        }
    ]
}


Spell {
    id: 780
    name: createWall
    Effects: [
        {
            aoe: Cross7
        }
    ]
}

