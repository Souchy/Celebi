
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
Flèche de répulsion into piège de répulsion et piège de dérive

Flèche de répulsion repousse de 2, puis fait des dégâts

Le répu d1 est placé avant le dérive d1.

Le piège de répulsion s'applique à t4, puis t2 vu que t4 est avant dans l'ordre horaire.

Le piège de dérive s'applique à t2, puis t4 vu que t2 est au centre.


```
[  ][r1][t4][f1]

[  ][d1][  ]

[t1][t2][t3]

[  ][  ][  ]

[  ][s0][  ]
```

### Étape 0, le packet : <b><i>s0</i></b> lance flèche de répulsion sur <b><i>t2</i></b>

### Étape 1 : process l'action de flèche
```
Action Flèche {
    process(Pousse)
    process(Dégâts)
}
```
Résultat : 
```
Group Flèche {
    Group Pousse {
        Effect pousse t2
        Effect pousse t3
        Effect pousse t1
    }
    Group Dégât {
        Effect dégât t2
        Effect dégât t3
        Effect dégât t1
    }
}
```
### Étape 2 : applique les effets de flèches et process les actions de triggers
```
Group Flèche {
    Group Pousse {
        apply pousse t2 {
            cell.triggers.find(onMoveOnCell).foreach()
        }
        apply pousse t3
        apply pousse t1
    }
    Group Dégât {
        apply dégât t2
        apply dégât t3
        apply dégât t1
    }
}
```
cell . triggers {
                Action piège répu {
                    process(pousse, cells du répu en ordre horaire)
                    process(dégât, cells du répu en ordre horaire)
                }
                Action piège dérive {
                    process(dégât, cells du derive en ordre horaire)
                    process(pousse, cells du derive en ordre horaire)
                }
            }
### Étape 3 : applique les effets ajoutés par les actions de triggers
```
Group Flèche {
    Group Pousse {
        Effect apply pousse t2
        Group répu {
            Group Pousse {
                apply pousse t4
                Group funeste {
                    apply dégât t4
                }
                apply pousse t2
            }
            Groupe Dégât {
                apply dégât t4
                apply dégât t2
            }
        }
        Group dérive {
            Groupe Pousse {
                apply pousse t4
                apply pousse t2
            }
            Groupe Dégât {
                apply dégât t4
                apply dégât t2
            }
        }
        
        apply pousse t3
        apply pousse t1
    }
    Group Dégât {
        apply dégât t2
        apply dégât t3
        apply dégât t1
    }
}
```
### Version horizontale, compilée
```
Flattened/Compiled {
    flèche apply pousse t2 
        répu apply remove status
        répu apply pousse t4 
            funeste dégât t4
        répu apply dégât t4
        répu apply pousse t2 
        répu apply dégât t2
        dérive apply remove status
        dérive apply dégât t2
        dérive apply pousse t2
    flèche apply pousse t3
    flèche apply pousse t1

    flèche apply dégât t2
    flèche apply dégât t3
    flèche apply dégât t1
}
```
Départ
```
    [  ][r1][t4][f1]

    [  ][d1][  ]

    [t1][t2][t3]

    [  ][  ][  ]

    [  ][s0][  ]
```
Flèche t2
```
    [  ][r1][t4][f1]

    [  ][t2][  ]

    [t1][  ][t3]

    [  ][  ][  ]

    [  ][s0][  ]
```
Mouvement arrêté par répu, trigger répu
```
    [  ][r1][t4][f1]

    [  ][t2][  ]

    [t1][  ][t3]

    [  ][  ][  ]

    [  ][s0][  ]
```
Répu t4
```
    [  ][r1][  ][t4]

    [  ][t2][  ]

    [t1][  ][t3]

    [  ][  ][  ]

    [  ][s0][  ]
```
Funeste t4
```
    [  ][r1][  ][t4]

    [  ][t2][  ]

    [t1][  ][t3]

    [  ][  ][  ]

    [  ][s0][  ]
```
Répu t2
```
    [  ][r1][  ][t4]

    [  ][  ][  ]

    [t1][  ][t3]

    [  ][t2][  ]

    [  ][s0][  ]
```

Dérive t2
```
    [  ][  ][  ][t4]

    [  ][  ][  ]

    [t1][  ][t3]

    [  ][t2][  ]

    [  ][s0][  ]
```
Dérive t4
```
    [  ][  ][  ][  ][t4]

    [  ][  ][  ][  ]

    [  ][  ][  ]

    [t1][  ][t3]

    [  ][t2][  ]

    [  ][s0][  ]
```

Dérive t3
```
    [  ][  ][  ][  ][t4]
    
    [  ][  ][  ][  ]
    
    [  ][  ][  ]
    
    [t1][  ][  ]
    
    [  ][t2][  ][t3]
    
    [  ][s0][  ]
```

Dérive t1
```
    [  ][  ][  ][  ][t4]
    
    [  ][  ][  ][  ]
    
    [  ][  ][  ]
    
    [  ][  ][  ]

[t1][  ][t2][  ][t3]

    [  ][s0][  ]
```
Flèche t3
```
    [  ][  ][  ][  ][t4]
    
    [  ][  ][  ][  ]

[t1][  ][  ][  ][t3]

    [  ][  ][  ]
    
    [  ][t2][  ]
    
    [  ][s0][  ]
```

