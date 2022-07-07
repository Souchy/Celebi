
# Effects
## Damage
## Heal
## Summon
## Status
## Move
### Translate
### Teleport
### Swap
### Throw
### Flee



# Status
## Triggers
### OnEffect
### OnTimeline


# Packets
## CastSpell
## Move

# Stats + Buffs





# CU01
Flèche de répulsion into piège de répulsion
Flèche de répulsion repousse de 1, puis fait des dégâts

[ ][p4][ ]

[p1][p2][p3]

[t1][t2][t3]

[ ][ ][ ]

[ ][S][ ]

S lance flèche de répulsion sur t2
```
Action Flèche {
    Context t2 est sur t2
    Pousse {
        Pousse (t2, not the cell) {
            Trigger Arrive sur p2 {
                Action p2 {
                    Enlève piège {
                        Enlève p1
                        Enlève p2
                        Enlève p3
                        Enlève p4
                    }
                    Pousse t2 {}
                    Dégât t2 {}
                }
            }
        }
        Pousse t3 {}
        Pousse t1 {}
    }
    Dégâts {
        Dégât (t2, not the cell),
        Dégât t3,
        Dégât t1
    }

}
```

