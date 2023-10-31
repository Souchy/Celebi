# Celebi

Tactical game.

## Draft

Each player has: 
  - ~3~ ~6~ 4 creatures 
  - ~2~ 3 on the board

You can swap a creature in and out with another one like pokemon.

You can choose elemental affinities on each creatures.

You can choose what spells to learn on each creature.

Creatures have a base spell set and a family set from which to choose spells.

Common elemental spells are unlocked with high affinities.

You can choose items/relics which give boosts to your whole team.

## Turn structure

- Pre-turn : only instant spells can be cast
- Turn : only normal spells and movement can be cast
- Post-turn : instant spells can be cast

## Creatures

Creatures have:
- passives
- base stats
- chosen affinities
- chosen spells
- base passives
  - Some passives can trigger when itself or another creature gets swapped in or swapped out.
- resource bars 1,2,3 associated to resourceEnums

## Game Modes
- Constructed 1v1 : 
  - each player makes their team in advance
  - then queue up with a selected team build
- Draft 3v3: 
  - 3 players queue up, then each chooses their character and build. 
  - the captain chooses the 4th creature. 
  - when a creature is swapped, the player who swapped out takes control of the new creature swapped in.
  - there's a pick order for creatures, but you can then do customization asynchronously during other people's turn to pick.





