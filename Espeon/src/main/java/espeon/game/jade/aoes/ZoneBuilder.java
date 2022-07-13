package espeon.game.jade.aoes;

import espeon.game.jade.Position;
import espeon.game.red.Aoe;

public class ZoneBuilder {
    
    public static Aoe build(Zone zone) {
        Aoe aoe = switch(zone.type) {
            case point -> new Aoe(1, 1, zone.targetTypeFilter);
            case line -> buildLine(zone);
            case diagonal -> buildDiagonal(zone);
            case cross -> buildLine(zone);
            case xcross -> buildXCross(zone);
            case circle -> buildCircle(zone);
            case circleRing -> buildCircle(zone);
            case square -> buildSquare(zone);
            case squareRing -> buildSquareRing(zone);
            case cone -> buildCone(zone);
            case arc -> buildArc(zone);
            case star -> throw new UnsupportedOperationException("Unimplemented case: " + zone.type);
            default -> throw new IllegalArgumentException("Unexpected value: " + zone.type);
        };
        
        setLocalOrigin(aoe, zone);
        aoe.rotate(zone.rotation);

        return aoe;
    }

    private static void setLocalOrigin(Aoe aoe, Zone zone) {
        int centerx = Math.floorDiv(aoe.getWidth(), 2);
        int centery = Math.floorDiv(aoe.getHeight(), 2);
        int right = aoe.getWidth() - 1;
        int top = aoe.getHeight() - 1;
        switch(zone.localOrigin) {
            case center -> aoe.origin = new Position(centerx, centery);
            case bottom -> aoe.origin = new Position(centerx, 0);
            case bottom_left -> aoe.origin = new Position(0, 0);
            case bottom_right -> aoe.origin = new Position(right, 0);
            case left -> aoe.origin = new Position(0, centery);
            case right -> aoe.origin = new Position(right, centery);
            case top -> aoe.origin = new Position(centerx, top);
            case top_left -> aoe.origin = new Position(0, top);
            case top_right -> aoe.origin = new Position(right, top);
        }
    }

    public static Aoe buildLine(Zone zone) {
        Aoe aoe = new Aoe(0, zone.length);
        for(int i = 0; i < zone.length; i++) {
            aoe.set(0, i, zone.targetTypeFilter);
        }
        return aoe;
    }
    
    public static Aoe buildCross(Zone zone) {
        Aoe aoe = new Aoe(0, zone.length);
        for(int i = 0; i < zone.length; i++) {
            aoe.set(0, i, zone.targetTypeFilter);
            aoe.set(i, 0, zone.targetTypeFilter);
        }
        return aoe;
    }
    
    public static Aoe buildDiagonal(Zone zone) {
        Aoe aoe = new Aoe(zone.length, zone.length);
		for (int i = 0; i < zone.length; i++) {
            aoe.set(i, i , zone.targetTypeFilter);
		}
        return aoe;
    }
    
    public static Aoe buildXCross(Zone zone) {
        Aoe aoe = new Aoe(zone.length, zone.length);
		for (int i = 0; i < zone.length; i++) {
            aoe.set(i, i , zone.targetTypeFilter);
            aoe.set(zone.length - i, zone.length - i , zone.targetTypeFilter);
		}
        return aoe;
    }

    public static Aoe buildCircle(Zone zone) {
        // exemple radius de 2 : 0, 1,  2,  3, 4
        // exemple radius de 3 : 0, 1, 2,  3,  4, 5, 6
        Aoe aoe = new Aoe(zone.length * 2 + 1, zone.length * 2 + 1);
        int radius = zone.length;
		for (int i = -radius; i < radius; i++) {
			for (int j = -radius; j < radius; j++) {
				if(Math.abs(i) + Math.abs(j) <= radius) {
					aoe.set(i + radius, j + radius, zone.targetTypeFilter);
				}
			}
		}
        return aoe;
    }

    public static Aoe buildCircleRing(Zone zone) {
        Aoe aoe = new Aoe(zone.length * 2 + 1, zone.length * 2 + 1);
        int radius = zone.length;
		for (int i = -radius; i < radius; i++) {
			for (int j = -radius; j < radius; j++) {
				if(Math.abs(i) + Math.abs(j) == radius) {
					aoe.set(i + radius, j + radius , zone.targetTypeFilter);
				}
			}
		}
        return aoe;
    }
    
    public static Aoe buildCone(Zone zone) {
        Aoe aoe = new Aoe(zone.length * 2 + 1, zone.length * 2 + 1);
        int radius = zone.length;
		for (int i = -radius; i < 1; i++) {
			for (int j = -radius; j < radius; j++) {
				if(Math.abs(i) + Math.abs(j) <= radius) {
					aoe.set(i + radius, j + radius, zone.targetTypeFilter);
				}
			}
		}
        return aoe;
    }

    public static Aoe buildArc(Zone zone) {
        Aoe aoe = new Aoe(zone.length * 2 + 1, zone.length * 2 + 1);
        int radius = zone.length;
		for (int i = -radius; i < 1; i++) {
			for (int j = -radius; j < radius; j++) {
				if(Math.abs(i) + Math.abs(j) == radius) {
					aoe.set(i + radius, j + radius, zone.targetTypeFilter);
				}
			}
		}
        return aoe;
    }

    public static Aoe buildSquare(Zone zone) {
        Aoe aoe = new Aoe(zone.length * 2 + 1, zone.length * 2 + 1);
        aoe.fill(zone.targetTypeFilter);
        return aoe;
    }

    public static Aoe buildSquareRing(Zone zone) {
        int width = zone.length + 1;
        Aoe aoe = new Aoe(width, width);
        for(int i = 0; i < width; i++) {
            aoe.set(0, i, zone.targetTypeFilter);
            aoe.set(width - 1, i, zone.targetTypeFilter);
            aoe.set(i, 0, zone.targetTypeFilter);
            aoe.set(i, width - 1, zone.targetTypeFilter);
        }
        return aoe;
    }




}
