using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code.Graphics
{
	/// <summary>
	/// Static class for handling resources.
	/// </summary>
	class Sprites
	{
		private Dictionary<String, Sprite> sprites = new Dictionary<string,Sprite>();

		/// <summary>
		/// This constructor takes an object of type Past to make sure that Past is the one and only
		/// place this can be constructed from. This works, since nothing has a object of type Past.
		/// In C++ we would have made declared Past our friend.
		/// </summary>
		/// <param name="key"> The Past 'key' cheaty trick </param>
		/// <param name="content"> The games content manager, so Sprites can load Texture2Ds </param>
		public Sprites(Past key, ContentManager content)
		{
			AddSprite("cursor",			new Sprite(content.Load<Texture2D>("sprites/cursor"),		new Rectangle(0, 0, 8, 8)));
			AddSprite("playerTurret",	new Sprite(content.Load<Texture2D>("sprites/player"),		new Rectangle(0, 0, 63, 64)));
			AddSprite("playerBase_1",	new Sprite(content.Load<Texture2D>("sprites/player"),		new Rectangle(64, 0, 64, 64)));
			AddSprite("playerBase_2",	new Sprite(content.Load<Texture2D>("sprites/player"),		new Rectangle(128, 0, 64, 64)));
			AddSprite("redLaser",		new Sprite(content.Load<Texture2D>("sprites/lasers"),		new Rectangle(0, 0, 8, 8)));
			AddSprite("blueLaser",		new Sprite(content.Load<Texture2D>("sprites/lasers"),		new Rectangle(8, 0, 8, 8)));
			AddSprite("greenLaser",		new Sprite(content.Load<Texture2D>("sprites/lasers"),		new Rectangle(16, 0, 8, 8)));
			AddSprite("debugBG",		new Sprite(content.Load<Texture2D>("sprites/debugBG"),		new Rectangle(0, 0, 1000, 1000)));
			AddSprite("obstacle",		new Sprite(content.Load<Texture2D>("sprites/objects"),		new Rectangle(0, 0, 64, 64)));
			AddSprite("rLaser1",		new Sprite(content.Load<Texture2D>("sprites/redLaserHit"),	new Rectangle(0, 0, 20, 20)));
			AddSprite("rLaser2",		new Sprite(content.Load<Texture2D>("sprites/redLaserHit"),	new Rectangle(20, 0, 20, 20)));
			AddSprite("rLaser3",		new Sprite(content.Load<Texture2D>("sprites/redLaserHit"),	new Rectangle(20, 0, 20, 20)));
		}

		private void AddSprite(string key, Sprite s)
		{
			if (key != null && s != null) sprites.Add(key, s);
		}

		public Sprite GetSprite(string key)
		{
			Sprite sprite;
			if (sprites.TryGetValue(key, out sprite)) return sprite;
			else return null;
		}
	}

}
