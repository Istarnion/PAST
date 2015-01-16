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
		private static Dictionary<String, Sprite> sprites = new Dictionary<string,Sprite>();

		private Sprites() { }

		public static void AddSprite(string key, Sprite s)
		{
			if (key != null && s != null) sprites.Add(key, s);
		}

		public static Sprite GetSprite(string key)
		{
			Sprite sprite;
			if (sprites.TryGetValue(key, out sprite)) return sprite;
			else return null;
		}
	}

}
