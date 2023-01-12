﻿using Umbreon.data;
using Godot;
using Newtonsoft.Json;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.io;
using souchy.celebi.eevee.face.models;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using System;
using System.Collections.Generic;
//using CreatureData = souchy.celebi.eevee.impl.objects.Creature;

namespace Umbreon.common
{

    public static class DiamondExtension
    {
        public static DiamondModels GetDiamonds(this Node node)
        {
            var nodePath = "/root/DiamondModels";
            var diamonds = node.GetNode<DiamondModels>(nodePath);
            return diamonds;
        }
    }

    public partial class DiamondModels : Node, IDiamondModels
    {
        public Dictionary<IID, ICreature> creatures { get; init; } = new Dictionary<IID, ICreature>();
        public Dictionary<IID, ISpellModel> spells { get; init; } = new Dictionary<IID, ISpellModel>();
        public Dictionary<IID, IEffect> effects { get; init; } = new Dictionary<IID, IEffect>();
        public Dictionary<IID, IMap> maps { get; init; } = new Dictionary<IID, IMap>();
        public Dictionary<IID, ICreatureModel> creatureModels { get; init; } = new Dictionary<IID, ICreatureModel>();
        public Dictionary<IID, ISpellModel> spellModels { get; init; } = new Dictionary<IID, ISpellModel>();
        public Dictionary<IID, IEffectModel> effectModels { get; init; } = new Dictionary<IID, IEffectModel>();
        public Dictionary<IID, ICreatureSkin> creatureSkins { get; init; } = new Dictionary<IID, ICreatureSkin>();
        public Dictionary<IID, ISpellSkin> spellSkins { get; init; } = new Dictionary<IID, ISpellSkin>();
        public Dictionary<IID, IEffectSkin> effectSkins { get; init; } = new Dictionary<IID, IEffectSkin>();


        public CreatureModelData[] creatureModelsData;
        public CreatureSkinData[] creatureSkinsData;
        public MapModelData[] mapModelsData;

        public DiamondModels()
        {
            //var fileCreatures = Godot.FileAccess.Open("res://data/creatures.json", Godot.FileAccess.ModeFlags.Read);
            //var jsonCreatures = Godot.JSON.ParseString(fileCreatures.GetAsText());
            // TODO: 
            //foreach(var creature in jsonCreatures.AsGodotArray()) 
            //parseCreature(creature);
            parseData();
        }

        public void parseData()
        {
            var creatureModelsText = Godot.FileAccess.Open("res://data/creatures.json", Godot.FileAccess.ModeFlags.Read).GetAsText();
            creatureModelsData = JsonConvert.DeserializeObject<CreatureModelData[]>(creatureModelsText);
            var creatureSkinsText = Godot.FileAccess.Open("res://data/skins.json", Godot.FileAccess.ModeFlags.Read).GetAsText();
            creatureSkinsData = JsonConvert.DeserializeObject<CreatureSkinData[]>(creatureSkinsText);
            var mapModelsDataText = Godot.FileAccess.Open("res://data/maps.json", Godot.FileAccess.ModeFlags.Read).GetAsText();
            mapModelsData = JsonConvert.DeserializeObject<MapModelData[]>(mapModelsDataText);
        }

    }
}