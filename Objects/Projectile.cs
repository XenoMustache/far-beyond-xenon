﻿using FarBeyond.Objects.Entities;
using SFML.Graphics;
using SFML.System;
using Xenon.Common.Utilities;
using static System.Math;

namespace FarBeyond.Objects {
	public class Projectile : Entity {
		public float lifeTime = 1000, damage;
		public bool decay = true;
		Clock disposeTimer;

		public enum ProjectileType {
			security,
			player,
			pirate
		}

		IntRect spriteRect;

		public Projectile(Vector2f position, ProjectileType type, float angle, float damage) : base(position) {
			this.damage = damage;
			this.angle = angle;
			this.position = position;

			spriteRect = new IntRect(new Vector2i(0, 0), new Vector2i((int)GameRegistry.bulletsTexture.Size.X / 5, (int)GameRegistry.bulletsTexture.Size.Y / 3));

			disposeTimer = new Clock();

			switch (type) {
				case ProjectileType.security:
					spriteRect.Top = 0;
					spriteRect.Left = 16 * 4;
					break;
				case ProjectileType.player:
					spriteRect.Top = 16;
					spriteRect.Left = 16 * 4;
					break;
				case ProjectileType.pirate:
					spriteRect.Top = 32;
					spriteRect.Left = 16 * 4;
					break;
			}

			sprite = new Sprite(GameRegistry.bulletsTexture, spriteRect) {
				Origin = new Vector2f(spriteRect.Width / 2, spriteRect.Height / 2),
				Position = this.position,
				Rotation = this.angle
			};

			collider = new ProjectileCollisionBox(this, position, new Vector2f(12, 12), Color.White) { damage = this.damage };
		}

		public override void Update(double deltaTime) {
			var life = disposeTimer.ElapsedTime.AsMilliseconds();

			var a = angle.DegToRad();

			position.X += (float)Sin(a) * speed * (float)deltaTime;
			position.Y += (float)-Cos(a) * speed * (float)deltaTime;

			sprite.Position = position;
			sprite.Rotation = angle;

			if (!collider.disposed) {
				collider.position = position;
				collider.Update(deltaTime);
			}

			if (decay && life >= lifeTime) {
				Dispose();
			}
		}

		public override void Render(RenderWindow window) {
			window.Draw(sprite);
			if (!collider.disposed) collider.Render(window);
		}

		protected override void OnDispose() {
			disposeTimer.Dispose();
			sprite.Dispose();
			collider.Dispose();

			base.OnDispose();
		}
	}
}
