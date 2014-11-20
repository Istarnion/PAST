package teambeard.games.past.desktop;

import teambeard.games.past.PAST;

import com.badlogic.gdx.backends.lwjgl.LwjglApplication;
import com.badlogic.gdx.backends.lwjgl.LwjglApplicationConfiguration;

public class DesktopLauncher {
	public static void main (String[] arg) {
		LwjglApplicationConfiguration config = new LwjglApplicationConfiguration();
		config.title = "P.A.S.T.";
		config.width = 1024;
		config.height = 600;
		config.vSyncEnabled = true;
		config.foregroundFPS = 40;
		new LwjglApplication(new PAST(), config);
	}
}
