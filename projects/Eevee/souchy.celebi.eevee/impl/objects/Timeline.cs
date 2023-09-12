﻿using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects.zones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects
{
    public class Timeline : ITimeline
    {
        public ObjectId entityUid { get; set; }
        public ObjectId fightUid { get; set; }

        public int currentRound { get; set; }
        public ObjectId currentTurn { get; set; }
        public List<TimelineSlot> slots { get; init; } = new List<TimelineSlot>();

        public static ITimeline Create(ObjectId fightid)
        {
            var timeline = new Timeline()
            {
                fightUid = fightid,
                entityUid = Eevee.RegisterIIDTemporary()
            };
            timeline.GetFight().timeline = timeline;
            return timeline;
        }

        public void nextRound()
        {
            currentRound++;
        }
        public void nextTurn()
        {
            int i = indexOf(currentTurn);
            i++;
            if (i >= size())
                i = 0;
            var next = getCreatureAt(i);
            this.currentTurn = next.entityUid;
        }
        public int indexOf(ObjectId creatureId)
        {
            int i = 0;
            foreach (var slot in slots)
            {
                if (slot.creatureId.Equals(creatureId))
                    return i;
                i++;
                foreach(var summ in slot.summons)
                {
                    if(summ.entityUid == creatureId) 
                        return i;
                    i++;
                }
            }
            return -1;
        }

        public int size() => slots.Sum(s => s.count);

        public ICreature getCreatureAt(int i)
        {
            int count = 0;
            foreach(var slot in this.slots)
            {
                if (i > count + slot.count)
                {
                    count += slot.count;
                }
                int localTurn = i - count;
                return slot.get(localTurn);
            }
            throw new Exception($"Error trying to Timeline.getCurrentCreature(). CurrentTurn: {currentTurn}. Count: {count}. Size: {size()}.");
        }

        public ICreature getCurrentCreature()
        {
            return this.GetFight().creatures.Get(currentTurn);
        }
        public IPlayer getCurrentPlayer()
        {
            return getCurrentCreature().GetCurrentOwner();
        }

        public bool isCreatureOnBoard(ObjectId crea)
        {
            return indexOf(crea) != -1;
        }

        public void Dispose()
        {
            slots.Clear();
        }

        public void addSlot(ObjectId crea)
        {
            var slot = new TimelineSlot()
            {
                creatureId = crea,
                fightUid = this.fightUid,
            };
            this.slots.Add(slot);
        }
    }

    /// <summary>
    /// For summoner's and their summons
    /// </summary>
    public class TimelineSlot
    {
        public ObjectId fightUid { get; set; }
        public ObjectId creatureId { get; set; }
        public IEnumerable<ICreature> summons { get => Eevee.fights.Get(fightUid).creatures.Values.Where(c => c.summoner == creatureId); }
        public int count { get => 1 + summons.Count(); }
        public IEnumerable<ICreature> getAll()
        {
            var list = new List<ICreature>(summons);
            list.Insert(0, get(0));
            return list;
        }
        public ICreature get(int localTurn)
        {
            if (localTurn == 0) return Eevee.fights.Get(fightUid).creatures.Get(creatureId);
            return summons.ElementAt(localTurn - 1);
        }
        public ICreature getCreature() => get(0);
    }

}
