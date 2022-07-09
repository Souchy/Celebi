package espeon.util;

import java.util.ArrayList;
import java.util.List;
import java.util.function.Consumer;
import java.util.function.Predicate;

public class Table<T> {

    public static class TableAnchor {
        public static TableAnchor top = new TableAnchor((int) Math.pow(2, 0));
        public static TableAnchor bottom = new TableAnchor((int) Math.pow(2, 1));
        public static TableAnchor left = new TableAnchor((int) Math.pow(2, 2));
        public static TableAnchor right = new TableAnchor((int) Math.pow(2, 3));
        public static TableAnchor topleft = top.or(left);
        public static TableAnchor bottomright = bottom.or(right);
        public final int val;
        private TableAnchor(int val) {
            this.val = val;
        }
        public TableAnchor or(TableAnchor anchor) {
            return new TableAnchor(this.val | anchor.val);
        }
        public boolean contains(TableAnchor anchor) {
            return (this.val & anchor.val) == anchor.val;
        }
        public boolean isTop() {
            return contains(top);
        }
        public boolean isBottom() {
            return contains(bottom);
        }
        public boolean isLeft() {
            return contains(left);
        }
        public boolean isRight() {
            return contains(right);
        }
    }

    protected final List<T> list = new ArrayList<>();
    protected final T defaul;
    protected int width;
    protected int height;

    public Table(int w, int h, T defaul) {
        this.width = w;
        this.height = h;
        this.defaul = defaul;
        // clear();
        while(size() < w * h) {
            list.add(defaul);
            // System.out.println("Table add " + defaul);
        }
        // resize(TableAnchor.top.or(TableAnchor.left), w, h);
    }

    
    public T get(int row, int col) {
        if (row < 0 || row >= height) return null;
		if (col < 0 || col >= width) return null;
        return list.get(row * width + col);
    }
    public void set(int row, int col, T v) {
		if (row < 0 || row >= height) return;
		if (col < 0 || col >= width) return;
		list.set(row * width + col, v);
    }
    public void set(int id, T v) {
        list.set(id, v);
    }
    public void add(int row, int col, T v) {
        if (row < 0 || row >= height) return;
		if (col < 0 || col >= width) return;
		list.add(row * width + col, v);
    }
    
    public int getRow(int cellid) {
        return Math.floorDiv(cellid, width);
    }
    public int getCol(int cellid) {
        return cellid % width;
    }
    public T get(int cellid) {
        return this.list.get(cellid);
    }

    public int getWidth() {
        return width;
    }
    public int getHeight() {
        return height;
    }

    public int size() {
        // return width * height; 
        return list.size();
    }

    public void fill(T v) {
        for(int i = 0; i < size(); i++) {
            list.set(i, v);
        }
    }

    public void clear() {
        // this.list.clear();
        fill(defaul);
    }

    public void addColumn() {
        addColumn(width - 1);
    }
    public void addColumn(int colIndex) {
		width++;
		for (int row = 0; row < height; row++) {
            list.add(colIndex + width * row, defaul);
		}
    }
    public void removeColumn(int colIndex) {
        for (int row = height - 1; row >= 0; row--) {
            list.remove(colIndex + width * row);
		}
		width--;
    }
    public void addRow() {
        addRow(height - 1);
    }
    public void addRow(int rowIndex) {
        height++;
        for (int col = 0; col < width; col++) {
            list.add(rowIndex * width + col, defaul);
        }
    }
    public void removeRow(int rowIndex) {
		height--;
		for (int col = 0; col < width; col++) {
            list.remove(rowIndex * width + 0);
		}
    }
    public void move(int x, int y) {
        while (x > 0) {
            addColumn(0);
			removeColumn(width - 1);
			x--;
		}
		while (x < 0) {
            removeColumn(0);
			addColumn(width);
			x++;
		}
		while (y > 0) {
            addRow(0);
			removeRow(height - 1);
			y--;
		}
		while (y < 0) {
			removeRow(0);
			addRow(height);
			y++;
		}
    }

    public void resize(TableAnchor anchor, int w, int h) {
        while(h > this.height) {
            if(anchor.isTop()) {
                addRow(0);
            } 
            if(anchor.isBottom()) {
                addRow(this.height - 1);
            }
        }
        while(w > this.width) {
            if(anchor.isLeft()) {
                addColumn(0);
            } 
            if(anchor.isRight()) {
                addColumn(this.width - 1);
            }
        }
        while(w < this.width) {
            if(anchor.isLeft()) {
                removeColumn(0);
            } 
            if(anchor.isRight()) {
                removeColumn(this.width - 1);
            }
        }
        while(h < this.height) {
            if(anchor.isTop()) {
                removeRow(0);
            } 
            if(anchor.isBottom()) {
                removeRow(this.height - 1);
            }
        }
    }

    public void foreach(Consumer<T> consumer) {
        for(var t : list)
            consumer.accept(t);
    }
    public T findOne(Predicate<T> predicate) {
        for(var t : list) {
            if(predicate.test(t)) {
                return t;
            }
        }
        return null;
    }

    public String[] toStringArray() {
        String[] arr = new String[list.size()];
        for(int i = 0; i < size(); i++) {
            arr[i] = String.valueOf(list.get(i));
        }
        return arr;
    }
    
}
