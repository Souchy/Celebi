package espeon.game.net;

import espeon.game.controllers.Fight;
import io.netty.util.AttributeKey;

public final class ChannelAttributes {
    
    public static final AttributeKey<Fight> fightKey = AttributeKey.newInstance("fight");

}
