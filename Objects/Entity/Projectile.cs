using FarBeyond.Registry;
using SFML.Graphics;
using SFML.System;
using Xenon.Common.Utilities;
using static System.Math;

namespace FarBeyond.Objects.Entities {
	public class Projectile : Entity {
		public float speed;

		float angle;
		EntityLiving hit;

		public enum ProjectileType {
			security,
			player,
			pirate
		}

		Sprite sprite;
		IntRect spriteRect;

		public Projectile(Vector2f position, ProjectileType type, float angle) : base(position) {
			this.angle = angle;
			this.position = position;
			spriteRect = new IntRect(new Vector2i(0, 0), new Vector2i((int)AssetRegistry.bulletsTexture.Size.X / 5, (int)AssetRegistry.bulletsTexture.Size.Y / 3));

			switch (type) {
				case ProjectileType.security: spriteRect.Top = 0; break;
				case ProjectileType.player: spriteRect.Top = 16; break;
				case ProjectileType.pirate: spriteRect.Top = 32; break;
			}

			sprite = new Sprite(AssetRegistry.bulletsTexture, spriteRect);

			sprite.Origin = new Vector2f(spriteRect.Width / 2, spriteRect.Height / 2);
			sprite.Position = position;
			sprite.Rotation = angle;

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

		public void OnHit() {

		}

		protected override void OnDispose() {
			sprite.Dispose();
			base.OnDispose();
		}
	}
}
