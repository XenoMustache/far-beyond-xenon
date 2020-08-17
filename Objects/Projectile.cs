using FarBeyond.Registry;
using SFML.Graphics;
using SFML.System;
using Xenon.Common;
using Xenon.Common.Utilities;
using static System.Math;

namespace FarBeyond.Objects {
	public class Projectile : GameObject {
		public Vector2f position;
		public CollisionBox collider;

		public enum ProjectileType {
			security,
			player,
			pirate
		}

		float angle, speed;
		Sprite sprite;
		IntRect spriteRect;

		public Projectile(ProjectileType type) {
			spriteRect = new IntRect(new Vector2i(0, 0), new Vector2i((int)AssetRegistry.bullets.Size.X / 5, (int)AssetRegistry.bullets.Size.Y / 3));

			switch (type) {
				case ProjectileType.security: spriteRect.Top = 0; break;
				case ProjectileType.player: spriteRect.Top = 16; break;
				case ProjectileType.pirate: spriteRect.Top = 32; break;
			}

			sprite = new Sprite(AssetRegistry.bullets, spriteRect);

			sprite.Origin = new Vector2f(spriteRect.Width / 2, spriteRect.Height / 2);
			sprite.Position = position;

			collider = new CollisionBox(this, position, new Vector2f(12, 12), Color.White);
		}

		public override void Update(double deltaTime) {
			var a = MiscUtils.DegToRad(angle);

			position.X += (float)Sin(a) * speed * (float)deltaTime;
			position.Y += (float)-Cos(a) * speed * (float)deltaTime;

			sprite.Position = position;
			sprite.Rotation = angle;

			collider.position = position;
			collider.Update(deltaTime);
		}

		public override void Render(RenderWindow window) {
			window.Draw(sprite);
			collider.Render(window);
		}

		protected override void OnDispose() {
			sprite.Dispose();
			base.OnDispose();
		}
	}
}
