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
		/// TODO:
		///		- Find a better way to calculate the endpoint
		///		- Optimize collision checks with quadTree or something similar
		/// </summary>
		/// <param name="origin">	The origin of the ray. </param>
		/// <param name="dir">		The direction of the ray. </param>
		/// <returns> null for now. Must return a RayHitInfo object. </returns>
		public RayHitInfo Raycast(Vector2 origin, Vector2 dir)
		{
			dir.Normalize();
			Vector2 endpoint = origin;
			GameObject victim = null;

			int delta;
			int stepDir = 1;

			bool steep = Math.Abs(dir.Y) > Math.Abs(dir.X);
			if(steep)
			{
				delta = (int)(dir.X / dir.Y);
				if (dir.Y < 0) stepDir = -1;
			}
			else
			{
				delta = (int)(dir.Y / dir.X);
				if (dir.Y < 0) stepDir = -1;
			}

			int ox = (int)origin.X;
			int oy = (int)origin.Y;
			int x = ox;
			int y = oy;

			int length = 0;

			bool hitObject = false;

			while(true)
			{
				// First, find x and y, flipping if steep.
				if(steep)
				{
					y += stepDir;
					x += delta;
				}
				else
				{
					x += stepDir;
					y += delta;
				}

				// Then, check if we are still inside the bounds of the room
				if(x < 0 || x >= width || y < 0 || y >= height)
				{
					endpoint = new Vector2(x, y);
					break;
				}

				// If we are, chek against all game objects. This may be optimized with something like a quad tree if needed
				foreach (GameObject gObject in gameObjects)
				{
					if(gObject.PxPerfCollision(x, y))
					{
						victim = gObject;
						endpoint = new Vector2(x, y);
						hitObject = true;
						break;
					}
				}
				if (hitObject) break;

				length++;
			}

			RayHitInfo info = new RayHitInfo(origin, endpoint, victim, length);
			return info;
		}

		/// <summary>
		/// Implemented using this as reference:
		/// http://www.codeproject.com/Articles/15604/Ray-casting-in-a-D-tile-based-environment
		/// </summary>
		/// <param name="x0"> x-coord of the starting point </param>
		/// <param name="y0"> y-coord of the starting point </param>
		/// <param name="x1"> x-coord of the endpoint </param>
		/// <param name="y1"> y-coord of the endpoint</param>
		/// <returns> A List<Vector2> of all the (pixel) points between points 0 and 1 </Vector2></value></returns>
		private List<Vector2> BresenhamLine(int x0, int y0, int x1, int y1)
		{
			List<Vector2> result = new List<Vector2>((int)Math.Sqrt(Math.Pow(x0 - x1, 2) + Math.Pow(y0 - y1, 2)));

			bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
			if(steep)
			{
				Swap(ref x0, ref y0);
				Swap(ref x1, ref y1);
			}
			if(x0 > x1)
			{
				Swap(ref x0, ref x1);
				Swap(ref y0, ref y1);
			}

			int deltax = x1 - x0;
			int deltay = Math.Abs(y1 - y0);
			int error = 0;
			int ystep;
			int y = y0;

			if (y0 < y1) ystep = 1;
			else ystep = -1;

			for (int x = x0; x <= x1; x++ )
			{
				if (steep) result.Add(new Vector2(y, x));
				else result.Add(new Vector2(x, y));
				error += deltay;
				if ( 2 * error >= deltax )
				{
					y += ystep;
					error -= deltax;
				}
			}

			return result;
		}

		/// <summary>
		/// Swaps the values of a and b using xor swap algorithm.
		/// This is done for novel resons, we know there is little if any at all
		/// gain in preformance
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		private void Swap(ref int a, ref int b)
		{
			if(a != b)
			{
				a ^= b;
				b ^= a;
				a ^= b;
			}
		}
	}
}
