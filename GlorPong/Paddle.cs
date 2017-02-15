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
#if ANDROID
		private const float PaddleSpeed = 10f;
#else
		private const float PaddleSpeed = 10f;
#endif
		private PlayerType playerType;

		public Paddle(Texture2D texture, Vector2 location, Rectangle screenSize, PlayerType playerType) : base(texture, location, screenSize)
		{
			this.playerType = playerType;
		}

		public override void Update(GameTime gameTime, GameObjects gameObjects)
		{
			if (playerType == PlayerType.Computer)
			{
				var rand = new Random();
				var reactionThreshold = rand.Next(30, 130);

				if (gameObjects.Ball.Location.Y + gameObjects.Ball.Height < Location.Y + reactionThreshold)
					Velocity = new Vector2(0, -PaddleSpeed);
				if (gameObjects.Ball.Location.Y > Location.Y + Height + reactionThreshold)
					Velocity = new Vector2(0, PaddleSpeed);
			}
			// Think about how I want this to actually work. This is a simple implementation that really just changes the direction.
			// The paddle starts out with a zero velocity, but once it starts moving it always moves until it hits the opposite boundary
			// in this implementation.
			if (playerType == PlayerType.Human)
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Left) || gameObjects.TouchInput.Up)
					Velocity = new Vector2(0, -PaddleSpeed);

				if (Keyboard.GetState().IsKeyDown(Keys.Right) || gameObjects.TouchInput.Down)
					Velocity = new Vector2(0, PaddleSpeed);
			}

			base.Update(gameTime, gameObjects);
		}

		protected override void CheckBounds()
		{
			Location = new Vector2(Location.X, MathHelper.Clamp(Location.Y, 0, gameBoundaries.Height - texture.Height));
		}
	}
}
