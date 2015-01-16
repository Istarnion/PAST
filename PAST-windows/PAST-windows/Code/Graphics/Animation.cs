using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code.Graphics
{
	class Animation
	{

		private Sprite[] frames;

		private float frameTime;

		private double lastFrame = 0;

		public bool pause { get; set; }

		private int currFrame = 0;

		public Animation(Sprite[] frames, float frameTime)
		{
			this.frames = frames;
			this.frameTime = frameTime;
		}

		public void Update(GameTime time)
		{
			if (!pause) lastFrame += time.ElapsedGameTime.TotalSeconds;
			if(lastFrame >= frameTime)
			{
				lastFrame = 0;
				currFrame++;
				if (currFrame >= frames.Length) currFrame = 0;
			}
		}

		public Sprite CurrentFrame()
		{
			return frames[currFrame];
		}
	}
}
