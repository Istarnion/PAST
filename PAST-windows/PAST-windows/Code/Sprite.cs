using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code
{
	class Sprite
	{

		private Texture2D textureAtlas;

		private Rectangle region;

		public Sprite(Texture2D tex, Rectangle region)
		{
			this.textureAtlas = tex;
			this.region = region;
		}

		public void Draw(SpriteBatch batch, int x, int y, int w, int h, float rotation)
		{
			int hw = w / 2;
			int hh = h / 2;
			batch.Draw(
				textureAtlas,
				new Rectangle(x, y, w, h), region,
				Color.White,
				rotation, new Vector2(hw, hh),
				SpriteEffects.None, 0);
		}
	}
}
