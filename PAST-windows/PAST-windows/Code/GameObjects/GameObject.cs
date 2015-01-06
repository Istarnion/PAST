using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code.GameObjects
{
	class GameObject
	{

		public static int TILE_SIZE = 64;

		private int xPos { get; set; }

		private int yPos { get; set; }

		protected float rotation = 0;

		private bool solid;

		private Sprite sprite;

		private bool[,] solidityMap;

		public GameObject(Sprite sprite, bool solid, int xPos, int yPos)
		{
			this.sprite = sprite;
			this.solid = solid;
			this.xPos = xPos;
			this.yPos = yPos;

			// Setup the soliditymap
			if (sprite != null)
			{
				Texture2D tex = sprite.GetTextureAtlas();
				Rectangle r = sprite.GetRegion();
				Color[] spriteData = new Color[tex.Width * tex.Height];
				tex.GetData<Color>(spriteData);

				solidityMap = new bool[r.Width, r.Height];
				for (int i = 0; i < r.Height; i++)
				{
					for (int j = 0; j < r.Width; j++)
					{
						solidityMap[j, i] = spriteData[i * r.Height + j].A == 0;	// This is WRONG. Fix!
					}
				}
			}
			else
			{
				solid = false;
			}
		}

		public GameObject(Sprite sprite, bool solid, int xPos, int yPos, bool[,] solidityMap)
		{
			this.sprite = sprite;
			this.solid = solid;
			this.xPos = xPos;
			this.yPos = yPos;
			this.solidityMap = solidityMap;
		}

		/// <summary>
		/// This method returns the result of a pixel perfect collision check
		/// It earlies out with the seperating axis theorem, so it is still quite efficient.
		/// </summary>
		/// <param name="x">The x coordinate of the point to check against</param>
		/// <param name="y">The y coordinate of the point to check against</param>
		/// <returns>True if it is a collision, false if not</returns>
		public bool PxPerfCollision(int x, int y)
		{
			if (!solid) return false;

			// Early out with seperating axis theorem
			if (x < xPos || x > xPos + TILE_SIZE || y < xPos || y > yPos + TILE_SIZE) return false;

			// Now we know the point is inside us, so we need to check with the solidity map
			int relX = x - xPos;
			int relY = y - yPos;
			if (solidityMap[relX, relY]) return true;
			return false;
		}

		public void Update(GameTime time)
		{ }

		/// <summary>
		/// Default drawing method. If the gameobject subclass doesn't need fancy drawing,
		/// it is enough to invoke this method.
		/// It draws the object to be square, at the default tile size, no rotation, and no tint.
		/// </summary>
		/// <param name="time">			The current timestamp </param>
		/// <param name="batch">		The spritebatch that we can talk to </param>
		/// <param name="camOffset">	The Camera offset we must consider when rendering </param>
		public void Draw(GameTime time, SpriteBatch batch, Vector2 camOffset)
		{
			sprite.Draw(batch, (int)(xPos + camOffset.X), (int)(yPos + camOffset.Y), TILE_SIZE, TILE_SIZE, rotation);
		}

		public GameObject Copy()
		{
			return new GameObject(sprite, solid, 0, 0, solidityMap);
		}

	}
}
