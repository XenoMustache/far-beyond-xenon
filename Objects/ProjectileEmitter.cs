using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using Xenon.Common;

namespace FarBeyond.Objects {
	public class ProjectileEmitter : GameObject {
		public float angle;
		public Vector2f position;

		public List<Projectile> projectiles;

		bool display;
		RectangleShape displayLine, displayRect;

		public enum ProjectileType {
			playerShot
		}

		public ProjectileEmitter(Vector2f position, Color color) {
			this.position = position;

			projectiles = new List<Projectile>();

			displayLine = new RectangleShape();
			displayLine.Size = new Vector2f(1, 32);
			displayLine.Origin = new Vector2f(0.5f, 32);
			displayLine.Position = position;
			displayLine.FillColor = color;
			displayLine.Rotation = angle;

			displayRect = new RectangleShape();
			displayRect.Size = new Vector2f(8, 8);
			displayRect.Origin = new Vector2f(4, 4);
			displayRect.Position = position;
			displayRect.FillColor = Color.Transparent;
			displayRect.OutlineColor = color;
			displayRect.OutlineThickness = 0.5f;
		}

		public override void Update(double deltaTime) {
			display = FarBeyond.showHitboxes;

			displayLine.Position = position;
			displayLine.Rotation = angle;

			displayRect.Position = position;

			foreach(var projectile in projectiles) {
				projectile.Update(deltaTime);
			}
		}

		public override void Render(RenderWindow window) {
			if (display) {
				window.Draw(displayLine);
				window.Draw(displayRect);
			}

			foreach (var projectile in projectiles) {
				projectile.Render(window);
			}
		}

		public void Fire(ProjectileType type) {
			switch (type) {
				case ProjectileType.playerShot:
					var projectile = new Projectile(Projectile.ProjectileType.player, position, angle);
					projectile.speed = 1000;

					projectiles.Add(projectile);
					break;
			}
		}
	}
}
