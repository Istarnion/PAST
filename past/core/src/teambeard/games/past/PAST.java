package teambeard.games.past;

import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.Pixmap.Format;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.glutils.FrameBuffer;

public class PAST extends ApplicationAdapter {
	SpriteBatch batch;
	Texture img;
	Texture light;
	Texture black;
	Texture elements;
	
	int width, height, offsetx, offsety;
	
	FrameBuffer fbo;
	
	@Override
	public void create () {
		batch = new SpriteBatch();
		batch.enableBlending();
		
		img = new Texture("floor.jpg");
		light = new Texture("light.png");
		black = new Texture("black.png");
		elements = new Texture("terrain.png");
	}

	@Override
	public void render () {
		width = Gdx.graphics.getWidth();
		height = Gdx.graphics.getHeight();
		
		offsetx = Gdx.input.getX()-width/2;
		offsety = Gdx.input.getY()-width/2;
		
		Gdx.gl.glClearColor(0.05f, 0.05f, 0.05f, 1);
		Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);
		
		// Prepare lightmap
		fbo = new FrameBuffer(Format.RGBA8888, width, height, false);
		fbo.begin();
		batch.begin();
		batch.draw(black, 0, 0, width, height);
		batch.draw(light, offsetx, offsety, width, width);
		batch.end();
		fbo.end();
		
		// Render scene
		batch.begin();
		batch.setBlendFunction(GL20.GL_SRC_ALPHA, GL20.GL_ONE_MINUS_SRC_ALPHA);
		batch.draw(img, 0, 0, width, height);
		batch.draw(elements, 0, -100);
		// Blend in light map
		batch.setBlendFunction(GL20.GL_SRC_ALPHA, GL20.GL_DST_COLOR);
		batch.draw(fbo.getColorBufferTexture(), 0, 0);
		batch.end();
		
		fbo.dispose();
	}
}
