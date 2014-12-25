﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code
{
	class GameObject
	{

		public static int TILE_SIZE = 64;

		private int xPos { get; set; }

		private int yPos { get; set; }

		private bool solid;

		private Texture2D sprite;

		private bool[,] solidityMap;

		public GameObject(Texture2D sprite, bool solid, int xPos, int yPos)
		{
			this.sprite = sprite;
			this.solid = solid;
			this.xPos = xPos;
			this.yPos = yPos;

			// Setup the soliditymap
			if (sprite != null)
			{
				Color[] spriteData = new Color[sprite.Width * sprite.Height];
				sprite.GetData<Color>(spriteData);

				solidityMap = new bool[sprite.Width, sprite.Height];
				for (int i = 0; i < sprite.Height; i++)
				{
					for (int j = 0; j < sprite.Width; j++)
					{
						solidityMap[j, i] = spriteData[i * sprite.Height + j].A == 0;
					}
				}
			}
			else
			{
				solid = false;
			}
		}

		public GameObject(Texture2D sprite, bool solid, int xPos, int yPos, bool[,] solidityMap)
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
		/// <param name="time"></param>
		/// <param name="batch"></param>
		/// <param name="camOffset"></param>
		public void Draw(GameTime time, SpriteBatch batch, Vector2 camOffset)
		{
			batch.Draw(sprite, new Rectangle((int)(xPos + camOffset.X), (int)(yPos + camOffset.Y), TILE_SIZE, TILE_SIZE), Color.White);
		}

		public GameObject Copy()
		{
			return new GameObject(sprite, solid, 0, 0, solidityMap);
		}

	}
}
