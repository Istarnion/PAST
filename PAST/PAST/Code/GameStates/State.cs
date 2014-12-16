using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST.Code.GameStates
{
	/// <summary>
	/// This interface is needed for use of the StateManager.
	/// </summary>
	abstract class State
	{
		/// <summary>
		/// Every state realization must have a reference to the
		/// StateManager object so they can push other states unto the stack, or pop themselves.
		/// </summary>
		protected StateManager manager;

		/// <summary>
		/// The constructor simply assigns the manager field
		/// </summary>
		/// <param name="manager"></param>
		public State(StateManager manager)
		{
			this.manager = manager;
		}

		/// <summary>
		/// This method is called every update cycle,
		/// to tell the state to update it's logic.
		/// It provides a GameTime object for timing.
		/// </summary>
		/// <param name="time"></param>
		public abstract void Update(GameTime time);

		/// <summary>
		/// This method is called every frame to tell the state to draw itself.
		/// It provides a GameTime object for timing, and
		/// a SpriteBatch object to do the actual rendering.
		/// </summary>
		/// <param name="time"></param>
		/// <param name="batch"></param>
		public abstract void Draw(GameTime time, SpriteBatch batch);

		/// <summary>
		/// This method is called only when the state
		/// has been beneath another on the stack, and shall now
		/// resume controll.
		/// </summary>
		public abstract void Resume();

		/// <summary>
		/// This method should be called if and only if another state is put atop of this
		/// state on the stack of the StateManager. The StateManager calls this method,
		/// and nothing else should.
		/// </summary>
		public abstract void Pause();
	}
}
