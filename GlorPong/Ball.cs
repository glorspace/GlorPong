using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GlorPong
{
	public class Ball : Sprite
	{
#if ANDROID
		private const float BallSpeedX = 5f;
#else
		private const float BallSpeedX = 5f;
#endif
		private Paddle attachedToPaddle;

		public Ball(Texture2D texture, Vector2 location, Rectangle screenSize) : base(texture, location, screenSize)
		{
		}

		protected override void CheckBounds()
		{
			if (Location.Y >= (gameBoundaries.Height - texture.Height) || Location.Y <= 0)
			{
				var newVelocity = new Vector2(Velocity.X, -Velocity.Y);
				Velocity = newVelocity;
			}
		}

		public override void Update(GameTime gameTime, GameObjects gameObjects)
		{
			if ((Keyboard.GetState().IsKeyDown(Keys.Space) || gameObjects.TouchInput.Tapped) && attachedToPaddle != null)
			{
				var newVelocity = new Vector2(BallSpeedX, attachedToPaddle.Velocity.Y * .75f);
				Velocity = newVelocity;
				attachedToPaddle = null;
			}

			if (attachedToPaddle != null)
			{
				Velocity = Vector2.Zero;
				Location = new Vector2(attachedToPaddle.Location.X + attachedToPaddle.Width, attachedToPaddle.Location.Y);
			}
			else
			{
				if (BoundingBox.Intersects(gameObjects.PlayerPaddle.BoundingBox) || BoundingBox.Intersects(gameObjects.ComputerPaddle.BoundingBox))
				{
					Velocity = new Vector2(-Velocity.X, Velocity.Y);
				}
			}

			base.Update(gameTime, gameObjects);
		}

		public void AttachTo(Paddle paddle)
		{
			attachedToPaddle = paddle;
		}
	}
}