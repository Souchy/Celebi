package espeon;

import static org.junit.jupiter.api.Assertions.assertArrayEquals;
import static org.junit.jupiter.api.Assertions.assertEquals;

import java.util.Arrays;

import org.junit.jupiter.api.Test;

import espeon.game.jade.Target.TargetType;
import espeon.game.jade.Target.TargetTypeFilter;
import espeon.game.red.Aoe;
import espeon.util.Table;
import espeon.util.Table.TableAnchor;

class AoeTest {

    private static int d = new TargetTypeFilter().value;
    
    @Test 
    void aoeSize() {
        Aoe aoe = new Aoe(5, 5);
        aoe.print();
        assertEquals(aoe.size(), 25);
    }
    
    @Test 
    void aoeInitArray() {
        int[][] arr = new int[][] {
            { 1, 1, 2, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 3, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
        };
        Aoe aoe = new Aoe(arr);
        aoe.print();
        assertEquals(aoe.size(), 25);
    }

    @Test 
    void aoeGet() {
        Aoe aoe = new Aoe(5, 5, new TargetTypeFilter(TargetType.corpse));
        assertEquals(aoe.get(0, 0).value, TargetType.corpse.value);
        assertEquals(aoe.get(4, 4).value, TargetType.corpse.value);
    }

    @Test
    void aoeSet() {
        Aoe aoe = new Aoe(5, 5, new TargetTypeFilter(TargetType.corpse));
        aoe.set(4, 4, TargetType.ally.toFilter());
        assertEquals(aoe.get(0, 0).value, TargetType.corpse.value);
        assertEquals(aoe.get(4, 4).value, TargetType.ally.value);
    }

    @Test
    void aoeAddColumn() {
        Aoe aoe = new Aoe(5, 5, new TargetTypeFilter(TargetType.corpse));
        aoe.print();
        // System.out.println(Arrays.deepToString(aoe.toArray()));
        System.out.println("add column");
        aoe.addColumn();
        aoe.print();
        // System.out.println(Arrays.deepToString(aoe.toArray())); //.replace("],", "],\n"));
        assertEquals(aoe.size(), 30);
    }
    
    @Test
    void aoeAddColumn2() {
        Aoe aoe = new Aoe(1, 5, new TargetTypeFilter(TargetType.corpse));
        assertEquals(aoe.size(), 5);
        aoe.print();
        // System.out.println("add column");
        aoe.addColumn();
        aoe.print();
        assertEquals(aoe.size(), 10);
    }
    
    @Test
    void aoeResize() {
        Aoe aoe;
        aoe = new Aoe();
        aoe.print();
        assertEquals(aoe.size(), 1);
        aoe.resize(TableAnchor.bottomright, 2, 5);
        aoe.print();
        assertEquals(aoe.size(), 10);
        aoe.resize(TableAnchor.bottomright, 2, 4);
        aoe.print();
        assertEquals(aoe.size(), 8);
        aoe.resize(TableAnchor.bottomright, 1, 3);
        aoe.print();
        assertEquals(aoe.size(), 3);
        aoe.resize(TableAnchor.bottomright, 5, 5);
        aoe.print();
        assertEquals(aoe.size(), 25);
    }
    
    @Test
    void aoeAddColumnIndex() {
        int[][] arr = new int[][] {
            { 1, 1, 2, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 3, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
        };
        Aoe aoe = new Aoe(arr);
        aoe.addColumn(3);
        int[][] arr2 = new int[][] {
            { 1, 1, 2, d, 1, 1 },
            { 1, 1, 1, d, 1, 1 },
            { 3, 1, 1, d, 1, 1 },
            { 1, 1, 1, d, 1, 1 },
            { 1, 1, 1, d, 1, 1 },
        };
        assertArrayEquals(aoe.toArray(), arr2);
    }

    @Test
    void aoeRemoveColumnIndex() {
        int[][] arr = new int[][] {
            { 1, 1, 2, 0, 1, 1 },
            { 1, 1, 1, 0, 1, 1 },
            { 3, 1, 1, 0, 1, 1 },
            { 1, 1, 1, 0, 1, 1 },
            { 1, 1, 1, 0, 1, 1 },
        };
        Aoe aoe = new Aoe(arr);
        aoe.removeColumn(3);
        int[][] arr2 = new int[][] {
            { 1, 1, 2, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 3, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
        };
        assertArrayEquals(aoe.toArray(), arr2);
    }

    
    @Test
    void aoeAddRow() {
        Aoe aoe = new Aoe(5, 5, new TargetTypeFilter(TargetType.corpse));
        aoe.print();
        System.out.println("add row");
        aoe.addRow(0);
        aoe.print();
        assertEquals(aoe.size(), 30);
    }

    @Test
    void aoeAddRowIndex() {
        int[][] arr = new int[][] {
            { 1, 1, 2, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 3, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
        };
        Aoe aoe = new Aoe(arr);
        aoe.print();
        aoe.addRow(4);
        int[][] arr2 = new int[][] {
            { 1, 1, 2, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 3, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { d, d, d, d, d },
            { 1, 1, 1, 1, 1 },
        };
        aoe.print();
        assertArrayEquals(aoe.toArray(), arr2);
    }

    @Test
    void aoeRemoveRowIndex() {
        int[][] arr = new int[][] {
            { 1, 1, 2, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 3, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1 },
        };
        Aoe aoe = new Aoe(arr);
        aoe.removeRow(4);
        int[][] arr2 = new int[][] {
            { 1, 1, 2, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 3, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
        };
        assertArrayEquals(aoe.toArray(), arr2);
    }

    @Test
    void aoeToArray() {
        int[][] arr = new int[][] {
            { 1, 1, 2, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 3, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
        };
        Aoe aoe = new Aoe(arr);
        var arr2 = aoe.toArray();
        assertArrayEquals(arr, arr2);
    }
    @Test
    void aoeToArray2() {
        int[][] arr = new int[][] {
            { 1, 1, 2, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 3, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
        };
        Aoe aoe = new Aoe(arr);
        var arr2 = aoe.toArray();
        assertArrayEquals(arr, arr2);
    }

}
