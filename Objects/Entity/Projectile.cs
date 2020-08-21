using FarBeyond.Registry;
using SFML.Graphics;
using SFML.System;
using Xenon.Common.Utilities;
using static System.Math;

namespace FarBeyond.Objects.Entities {
	public class Projectile : Entity {
		public float speed, lifeTime = 1000, damage = 10;
		public bool decay = true;
		Clock disposeTimer;

		float angle;

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

			disposeTimer = new Clock();

			switch (type) {
				case ProjectileType.security: spriteRect.Top = 0; break;
				case ProjectileType.player: spriteRect.Top = 16; break;
				case ProjectileType.pirate: spriteRect.Top = 32; break;
			}

			sprite = new Sprite(AssetRegistry.bulletsTexture, spriteRect);

			sprite.Origin = new Vector2f(spriteRect.Width / 2, spriteRect.Height / 2);
			sprite.Position = position;
			sprite.Rotation = angle;

			collider = new ProjectileCollisionBox(this, position, new Vector2f(12, 12), Color.White);

			var c = (ProjectileCollisionBox)collider;
			c.damage = damage;
		}

		public override void Update(double deltaTime) {
			var life = disposeTimer.ElapsedTime.AsMilliseconds();

			var a = MiscUtils.DegToRad(angle);

			position.X += (float)Sin(a) * speed * (float)deltaTime;
			position.Y += (float)-Cos(a) * speed * (float)deltaTime;

			sprite.Position = position;
			sprite.Rotation = angle;

			if (collider != null) {
				collider.position = position;
				collider.Update(deltaTime);
			}

			if (decay && life >= lifeTime) {
				Dispose();
			}
		}

		public override void Render(RenderWindow window) {
			window.Draw(sprite);
			if (collider != null) collider.Render(window);
		}

		protected override void OnDispose() {
			disposeTimer.Dispose();
			sprite.Dispose();

			base.OnDispose();
		}
	}
}
