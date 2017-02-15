using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GlorPong
{
	public enum PlayerType
	{
		Human,
		Computer
	}

	public class Paddle : Sprite
	{
		private PlayerType playerType;

		public Paddle(Texture2D texture, Vector2 location, Rectangle screenSize, PlayerType playerType) : base(texture, location, screenSize)
		{
			this.playerType = playerType;
		}

		public override void Update(GameTime gameTime, GameObjects gameObjects)
		{
			if (playerType == PlayerType.Computer)
			{
				if (gameObjects.Ball.Location.Y + gameObjects.Ball.Height < Location.Y)
					Velocity = new Vector2(0, -10f);
				if (gameObjects.Ball.Location.Y > Location.Y)
					Velocity = new Vector2(0, 10f);
			}
			// Think about how I want this to actually work. This is a simple implementation that really just changes the direction.
			// The paddle starts out with a zero velocity, but once it starts moving it always moves until it hits the opposite boundary
			// in this implementation.
			if (playerType == PlayerType.Human)
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Left))
					Velocity = new Vector2(0, -10f);

				if (Keyboard.GetState().IsKeyDown(Keys.Right))
					Velocity = new Vector2(0, 10f);
			}

			base.Update(gameTime, gameObjects);
		}

		protected override void CheckBounds()
		{
			Location.Y = MathHelper.Clamp(Location.Y, 0, gameBoundaries.Height - texture.Height);
		}
	}
}
