﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects
{
    /// <summary>
    /// We have multiple modes and even allow custom games.
    /// </summary>
    public class FightSettings
    {
        public FightSettings() { }
        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }
        public FightPreparationType preparationType { get; set; }
        public int secondsPerTurn { get; set; } = 30;
        public int numberOfTeams { get; set; } = 2;
        public int numberOfCreaturesPerTeam { get; set; } = 5;
        public int numberOfcreaturesOnBoardPerTeam { get; set; } = 3;
    }

    public enum FightPreparationType
    {
        constructed = 1,
        draft = 2,
    }
}