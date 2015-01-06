using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PAST_windows.Code.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code.Rooms
{
	class Room
	{

		private int width, height;

		private List<GameObject> gameObjects;

		public Room(int width, int height)
		{
			this.width = width;
			this.height = height;
			gameObjects = new List<GameObject>(50);
		}

		public void Add(GameObject obj)
		{
			gameObjects.Add(obj);
		}

		public void Update(GameTime time)
		{
			foreach(GameObject obj in gameObjects)
			{
				obj.Update(time);
			}
		}

		public void Draw(GameTime time, SpriteBatch batch, Vector2 camOffset)
		{
			foreach (GameObject obj in gameObjects)
			{
				obj.Draw(time, batch, camOffset);
			}
		}

		/// <summary>
		/// TODO. Implement this.
		/// </summary>
		/// <param name="origin">	The origin of the ray. </param>
		/// <param name="dir">		The direction of the ray. </param>
		/// <returns> null for now. Must return a RayHitInfo object. </returns>
		public RayHitInfo Raycast(Vector2 origin, Vector2 dir)
		{
			return null;
		}
	}
}
