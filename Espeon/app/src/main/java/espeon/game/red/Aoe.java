package espeon.game.red;

import java.util.function.Consumer;

import espeon.game.jade.Position;
import espeon.game.jade.Target.TargetTypeFilter;
import espeon.util.Table;

public class Aoe extends Table<TargetTypeFilter> {
    
	public Position origin = new Position();

    public Aoe() {
        this(1, 1);
    }

    public Aoe(int w, int h) {
        this(w, h, new TargetTypeFilter());
    }

	public Aoe(int[][] vals) {
		super(vals[0].length, vals.length, new TargetTypeFilter());
		for (int i = 0; i < vals.length; i++) { // row
			for (int j = 0; j < vals[i].length; j++) { // col
				set(i, j, new TargetTypeFilter(vals[i][j]));
			}
		}
	}

    public Aoe(int w, int h, TargetTypeFilter defaul) {
        super(w, h, defaul);
    }

	public void setAoe(Aoe a) {
		int u = (width - a.width) / 2;
		int v = (height - a.height) / 2;
		setAoe(a, u, v);
	}
	public void setAoe(Aoe a, int u, int v) {
		for (int i = 0; i < a.width; i++) { // row
			for (int j = 0; j < a.height; j++) { // col
				set(i + u, j + v, a.get(i, j));
			}
		}
	}
	public void addAoe(Aoe a, int u, int v) {
		for (int i = 0; i < a.width; i++) { // row
			for (int j = 0; j < a.height; j++) { // col
				addAt(i + u, j + v, a.get(i, j));
			}
		}
	}
	public void removeAoe(Aoe a, int u, int v) {
		for (int i = 0; i < a.width; i++) { // row
			for (int j = 0; j < a.height; j++) { // col
				removeAt(i + u, j + v, a.get(i, j));
			}
		}
	}
	public void addAt(int x, int y, TargetTypeFilter f) {
		set(x, y, get(x, y).add(f));
	}
	public void removeAt(int x, int y, TargetTypeFilter f) {
		set(x, y, get(x, y).sub(f));
	}

	public void print() {
		String str = "";
		int i = 0;
		for (var val : list) {
			i++;
            String space = "\t";
			if (i == width) {
				i = 0;
                space = "\n";
			}
            // System.out.print("" + val.value + space);
			str += val.value + space;
		}
		System.out.print(str);
	}

	public void foreach(Consumer<TargetTypeFilter> f) {
		for (var val : list) {
			f.accept(val);
		}
	}

	public int[][] toArray() {
		int[][] arr = new int[height][width];
		for(int i = 0; i < height; i++) {
			for(int j = 0; j < width; j++) {
				arr[i][j] = get(i, j).value;
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
		Aoe aoe = new Aoe(1, len, defaul);
		return aoe;
	}
	public static Aoe newLinePerpendicular(int len, TargetTypeFilter defaul) {
		Aoe aoe = new Aoe(len, 1, defaul);
		return aoe;
	}
}
