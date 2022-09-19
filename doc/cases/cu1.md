
ContextStats
- numberOfTargetsAffected
- AP used/lost/gained/increased/reduced
- MP used/lost/gained/increased/reduced
- Damage done/received
- Healing done/received
	
---

	Spell {
		Book {
			ResourceEffect1 {
				mod = ap
				value = 2
				
				DamageEffect1 {
					origin = parent.target
					aoe = circle 2
					power = 150
				}
			}
			HealEffect1 {
				target  = caster
				value = previousEffectContext.caster.apReduced * 100
			}
		}
	}


	SpellBookContext {
		contextStats;
		ResourceEffect1Context {
			this.contextStats[apReduced] += 2 * number of targets
			this.contextStats[targets] += number of targets
			DamageEffect1Context {
				this.contextStats[damageDone] += 150 * number of targets around each target
			}
		}
		HealEffect1Context {

		}

	}

