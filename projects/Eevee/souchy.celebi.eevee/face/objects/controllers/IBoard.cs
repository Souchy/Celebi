﻿using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl;

namespace souchy.celebi.eevee.face.objects.controllers
{
    public interface IBoard : IFightEntity
    {
        /// <summary>
        /// List of creatures on the board <br></br>
        /// Creatures need to be ordered in timeline order
        /// </summary>
        public IEntityList<IID> creatureIds { get; init; }
        /// <summary>
        /// List of cells on the board
        /// </summary>
        public IEntityList<IID> cells { get; init; }


        // Board will be just a normal data object like the others.
        // Make something else for functions

        //public ICell getCell(IPosition pos);
        //public ICreature getCreature(IPosition pos);
        //public List<ICreature> getCreatures(IPosition pos);
        //public bool hasCreature(IPosition pos);

        public IEnumerable<ICreature> GetCreatures() => creatureIds.Values.Select(crea => GetFight().creatures.Get(crea));
        public IEnumerable<ICell> GetCells() => cells.Values.Select(cell => GetFight().cells.Get(cell));

        public IEnumerable<ICreature> GetCreaturesOnCell(IID targetCell)
        {
            var cell = this.GetFight().cells.Get(targetCell);
            return this.GetCreatures()
                    .Where(crea => crea.position == cell.position);
        }
        public ICreature? GetCreatureOnCell(IID targetCell)
        {
            return GetCreaturesOnCell(targetCell).FirstOrDefault();
        }
        public ICell? GetCreatureCell(ICreature crea)
        {
            return this.GetCells().FirstOrDefault(cell => cell.position == crea.position);
        }

    }
}