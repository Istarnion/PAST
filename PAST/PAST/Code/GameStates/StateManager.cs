using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST.Code.GameStates
{
	/// <summary>
	/// This class will have responsible for 
	/// acting as a stack of game states.
	/// </summary>
	class StateManager
	{

		/// <summary>
		/// The stack for storing states.
		/// </summary>
		private Stack<State> states;

		/// <summary>
		/// The constructor simply sets up the stack.
		/// </summary>
		public StateManager()
		{
			states = new Stack<State>();
		}

		/// <summary>
		/// Pushes a new state to the top of the stack,
		/// but first calling Pause() to the previous top, if it existed.
		/// </summary>
		/// <param name="state"></param>
		public void Push(State state)
		{
			if (states.Count > 0) states.Peek().Pause();    // Calls Pause() on the previous top.

			states.Push(state);
		}

		/// <summary>
		/// Pops the top of the stack, and calling Resume() on
		/// the element below, if there is something there.
		/// </summary>
		/// <returns>The popped state</returns>
		public State Pop()
		{
			if (states.Count > 0)
			{
				State state = states.Pop();

				if (states.Count > 0) states.Peek().Resume();

				return state;
			}
			else return null;
		}

		/// <summary>
		/// Peeks the stack.
		/// </summary>
		/// <returns>The top element on the stack</returns>
		public State Peek()
		{
			return states.Peek();
		}

		/// <summary>
		/// Calls the Update() method of the top element on the stack
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
			if (states.Count > 0)
			{
				states.Peek().Update(time);
			}
		}

		/// <summary>
		/// Calls the Draw() method of the top element on the stack
		/// </summary>
		/// <param name="time"></param>
		/// <param name="batch"></param>
		public void Draw(GameTime time, SpriteBatch batch)
		{
			if (states.Count > 0)
			{
				states.Peek().Draw(time, batch);
			}
		}
	}
}
