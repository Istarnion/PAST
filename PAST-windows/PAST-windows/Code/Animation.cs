﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code
{
	class Animation
	{

		private Sprite[] frames;

		private float frameTime;

		private int lastFrame = 0;

		public bool pause { get; set; }

		private int currFrame = 0;

		public Animation(Sprite[] frames, int frameTime)
		{
			this.frames = frames;
			this.frameTime = frameTime;
		}

		public void Update(GameTime time)
		{
			if (lastFrame == 0) lastFrame = time.ElapsedGameTime.Milliseconds;
			if(!pause && time.ElapsedGameTime.Milliseconds - lastFrame >= frameTime)
			{
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
