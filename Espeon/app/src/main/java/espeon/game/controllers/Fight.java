package espeon.game.controllers;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.atomic.AtomicInteger;

import espeon.util.IDGenerator;

public class Fight {
    
    private IDGenerator fightCounter = new IDGenerator(1);
    private IDGenerator entityCounter = new IDGenerator();

    public int id = fightCounter.get();
    public Board board;
    // public List<Creature> creatures;

    public AtomicInteger turn = new AtomicInteger();
    public AtomicInteger round = new AtomicInteger();
    private List<Integer> timeline = new ArrayList<>();


    public Fight() {
        board = new Board(this);
    }
    
    public int getCurrentPlayingCreature() {
        return timeline.get(turn.get());
    }

    public int newEntityId() {
        return entityCounter.get();
    }
    public synchronized void spawn(Integer summoner, int... summons) {
        int index = timeline.indexOf(summoner);
        for(int i = 0; i < summons.length; i++) {
            timeline.add(index + i + 1, summons[i]);
        }
    }
    public synchronized void despawn(Integer creature) {
        boolean found = timeline.remove(creature);
        // also despawn all its summons
    }
    public synchronized List<Integer> getTimelineCopy() {
        List<Integer> copy = new ArrayList<>(timeline);
        return copy;
    }

}
