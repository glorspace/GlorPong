using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GlorPong
{
	public abstract class Sprite
	{
		protected readonly Texture2D texture;
		protected readonly Rectangle gameBoundaries;
		public Vector2 Location	{ get; set;	}

		public int Height
		{
			get { return texture.Height; }
		}

		public int Width
		{
			get { return texture.Width; }
		}

		public Rectangle BoundingBox
		{
			get
			{
				return new Rectangle((int)Location.X, (int)Location.Y, Width, Height);
			}
		}

		public Vector2 Velocity { get; protected set; }

		public Sprite(Texture2D texture, Vector2 location, Rectangle screenSize)
		{
			this.texture = texture;
			Location = location;
			gameBoundaries = screenSize;

			Velocity = Vector2.Zero;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, Location, Color.White);
		}
		public virtual void Update(GameTime gameTime, GameObjects gameObjects)
		{
			Location += (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

			CheckBounds();
		}

		protected abstract void CheckBounds();
	}
}