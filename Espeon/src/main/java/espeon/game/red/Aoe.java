package espeon.game.red;

import java.util.ArrayList;
import java.util.function.Consumer;

import espeon.game.jade.Position;
import espeon.game.jade.Target.TargetType;
import espeon.game.jade.Target.TargetTypeFilter;
import espeon.game.types.Direction;
import espeon.util.Table;

public class Aoe extends Table<Integer> {

	public Position origin = new Position();

    public Aoe() {
        this(1, 1);
    }

    public Aoe(int w, int h) {
        this(w, h, TargetType.all);
    }

	public Aoe(int[][] vals) {
		super(vals[0].length, vals.length, TargetType.nothing);
		for (int i = 0; i < vals.length; i++) { // row
			for (int j = 0; j < vals[i].length; j++) { // col
				set(i, j, vals[i][j]); // new TargetTypeFilter(vals[i][j]));
			}
		}
	}

    public Aoe(int w, int h, int defaul) {
        super(w, h, defaul);
    }

	public Aoe rotate(Direction r) {
		Aoe copy = this.copy();
		// TODO
		return copy;
	}

	public Aoe copy() {
		Aoe copy = new Aoe();
		// copy.list = new ArrayList<>(this.list);
		copy.list.addAll(this.list);
		copy.origin = origin.copy();
		return copy;
	}

	public void setAoe(Aoe a) {
		int u = (width - a.width) / 2;
		int v = (height - a.height) / 2;
		setAoe(a, u, v);
	}
	public void setAoe(Aoe a, int u, int v) {
		for (int i = 0; i < a.width; i++) { // col
			for (int j = 0; j < a.height; j++) { // row
				set(i + u, j + v, a.get(j, i));
			}
		}
	}
	public void addAoe(Aoe a, int u, int v) {
		for (int i = 0; i < a.width; i++) { // col
			for (int j = 0; j < a.height; j++) { // row
				addAt(i + u, j + v, a.get(j, i));
			}
		}
	}
	public void orAoe(Aoe a, int u, int v) {
		for (int i = 0; i < a.width; i++) { // col
			for (int j = 0; j < a.height; j++) { // row
				orAt(i + u, j + v, a.get(j, i));
			}
		}
	}
	public void removeAoe(Aoe a, int u, int v) {
		for (int i = 0; i < a.width; i++) { // col
			for (int j = 0; j < a.height; j++) { // row
				removeAt(i + u, j + v, a.get(j, i));
			}
		}
	}

	// FIXME swap x and y because set/get use rows and columns instead
	public void addAt(int x, int y, int filter) {
		set(y, x, get(x, y) + filter);
	}
	public void orAt(int x, int y, int filter) {
		set(y, x, get(x, y) | filter);
	}
	// FIXME swap x and y because set/get use rows and columns instead
	public void removeAt(int x, int y, int filter) {
		set(y, x, get(x, y) & ~filter);
	}

	public void print() {
		String str = "[";
		int row = 0;
		int col = 0;
		for (var val : list) {
			if(col == 0) {
				if(row == 0)
					str += "[";
				else
					str += " [";
			}
			col++;
            String space = ",\t";
			if (col == width) {
				col = 0;
				row++;
				if(row == height)
					space = "]]\n";
				else
            	    space = "]\n";
			}
			// str += "}";
            // System.out.print("" + val.value + space);
			str += val + space;
		}
		System.out.print(str);
	}

	// public void foreach(Consumer<TargetTypeFilter> f) {
	// 	for (var val : list) {
	// 		f.accept(val);
	// 	}
	// }
	public void foreachId(Consumer<Integer> actionOnId) {
		for(int i = 0; i < size(); i++) {
			actionOnId.accept(i);
		}
	}

	public int[][] toArray() {
		int[][] arr = new int[height][width];
		for(int i = 0; i < height; i++) {
			for(int j = 0; j < width; j++) {
				arr[i][j] = get(i, j);
			}
		}
		return arr;
	}


    public static class Single extends Aoe {
        public Single() {
            super();
        }
    }
	public static Aoe newLine(int len, TargetTypeFilter defaul) {
		Aoe aoe = new Aoe(1, len, defaul.value);
		return aoe;
	}
	public static Aoe newLinePerpendicular(int len, TargetTypeFilter defaul) {
		Aoe aoe = new Aoe(len, 1, defaul.value);
		return aoe;
	}
}
