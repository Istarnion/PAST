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
			Vector2 endpoint = origin + dir * (int)Math.Sqrt(Math.Pow(width, 2) + Math.Pow(height, 2));
			GameObject victim = null;

			foreach(Vector2 v in BresenhamLine((int)origin.X, (int)origin.Y, (int)endpoint.X, (int)endpoint.Y))
			{
				foreach(GameObject obj in gameObjects)
				{
					if(v.X < 0 || v.X >= width || v.Y < 0 || v.Y >= height)
					{
						endpoint = v;
						break;
					}
					if(obj.PxPerfCollision((int)v.X, (int)v.Y))
					{
						victim = obj;
						endpoint = v;
						break;
					}
				}
			}

			RayHitInfo info = new RayHitInfo(origin, endpoint, victim);
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
