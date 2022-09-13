package cthulhu;

import java.util.List;

public class PluginManager {
	
	public List<Plugin> plugins;


	
	public void load(String jarPath) {
		// the jar's manifest or config should specify the "main" / plugin class iside the jar
		Plugin p = new Plugin(); // Class.forname().instanciate()...
		p.manager = this;
	}



	public Object message(PluginMessage msg) {
		return null;
	}

	public void listen(Plugin p) {

	}

}
