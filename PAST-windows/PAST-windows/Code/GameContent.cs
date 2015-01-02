using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code
{
	/// <summary>
	/// Static class for handling resources.
	/// </summary>
	class GameContent
	{
		private static Dictionary<String, Sprite> sprites;



		private static bool ready = false;

		private GameContent() { }

		/// <summary>
		/// Setup the Dictionary
		/// </summary>
		/// <param name="gd"></param>
		/// <param name="manager"></param>
		public static void Setup()
		{
			sprites = new Dictionary<string, Sprite>();
			ready = true;
		}

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

		public static bool Ready() { return ready; }
	}

}
