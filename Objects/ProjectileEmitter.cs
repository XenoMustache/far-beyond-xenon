using FarBeyond.Objects.Entities;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using Xenon.Common;
using Xenon.Common.Utilities;

namespace FarBeyond.Objects {
	public class ProjectileEmitter : GameObject {
		public float angle;
		public Vector2f inputPosition, position, offset;

		public List<Projectile> projectiles;

		bool display;
		RectangleShape displayLine, displayRect;

		public enum ProjectileType {
			playerShot
		}

		public ProjectileEmitter(Vector2f position, Color color) {
			inputPosition = position;

			projectiles = new List<Projectile>();

			displayLine = new RectangleShape();
			displayLine.Size = new Vector2f(1, 32);
			displayLine.Origin = new Vector2f(0.5f, 32);
			displayLine.Position = position;
			displayLine.FillColor = color;
			displayLine.Rotation = angle;

			displayRect = new RectangleShape();
			displayRect.Size = new Vector2f(4, 4);
			displayRect.Origin = new Vector2f(displayRect.Size.X / 2, displayRect.Size.Y / 2);
			displayRect.Position = position;
			displayRect.FillColor = Color.Transparent;
			displayRect.OutlineColor = color;
			displayRect.OutlineThickness = 0.5f;
		}

		public override void Update(double deltaTime) {
			position.X = offset.X * (float)Math.Cos(MiscUtils.DegToRad(angle)) + inputPosition.X;
			position.Y = offset.X * (float)Math.Sin(MiscUtils.DegToRad(angle)) + inputPosition.Y;

			var rectPos = new Vector2f();
			rectPos.X = offset.Y * (float)Math.Sin(MiscUtils.DegToRad(-angle)) + position.X;
			rectPos.Y = offset.Y * (float)Math.Cos(MiscUtils.DegToRad(-angle)) + position.Y;

			display = FarBeyond.showHitboxes;

			displayLine.Position = position;
			displayLine.Rotation = angle;

			displayRect.Position = rectPos;

			for (var i = 0; i < projectiles.Count; i++) {
				if (!projectiles[i].disposed) projectiles[i].Update(deltaTime);
			}
		}

		public override void Render(RenderWindow window) {
			if (display) {
				window.Draw(displayLine);
				window.Draw(displayRect);
			}

			for (var i = 0; i < projectiles.Count; i++) {
				if (!projectiles[i].disposed) projectiles[i].Render(window);
			}
		}

		// TODO: Replace with registry
		public void Fire(ProjectileType type) {
			switch (type) {
				case ProjectileType.playerShot:
					var pos = new Vector2f();

					pos.X = offset.Y * (float)Math.Sin(MiscUtils.DegToRad(-angle)) + position.X;
					pos.Y = offset.Y * (float)Math.Cos(MiscUtils.DegToRad(-angle)) + position.Y;

					var projectile = new Projectile(pos, Projectile.ProjectileType.player, angle);
					projectile.speed = 200;
					projectile.lifeTime = 2000;

					projectiles.Add(projectile);
					break;
			}
		}
	}
}
