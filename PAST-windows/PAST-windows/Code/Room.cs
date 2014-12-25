using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code
{
	class Room
	{

		private List<GameObject> gameObjects;

		public Room()
		{
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

			}
		}

		public void Draw(GameTime time, SpriteBatch batch, Vector2 camOffset)
		{
			foreach (GameObject obj in gameObjects)
			{

			}
		}
	}
}
