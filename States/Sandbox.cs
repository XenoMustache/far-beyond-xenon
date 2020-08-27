﻿using FarBeyond.Objects;
using FarBeyond.Objects.Entities;
using SFML.Graphics;
using SFML.System;
using Xenon.Common.State;

namespace FarBeyond.States {
	public class Sandbox : GameState {
		public float mapWidth = 256, mapHeight = 256;

		RectangleShape mapBounds;
		Player player;
		NPC testNPC;

		public Sandbox(RenderWindow window) : base(window) {
			window.KeyPressed += (s, e) => {
				if (e.Code == SFML.Window.Keyboard.Key.Space) {
					player.leftEmitter.Fire(ProjectileEmitter.ProjectileType.playerShot);
					player.rightEmitter.Fire(ProjectileEmitter.ProjectileType.playerShot);
				}
			};

			mapBounds = new RectangleShape(new Vector2f(mapWidth * 2, mapHeight * 2));
			testNPC = new NPC(new Vector2f(-32, 0), NPC.NPCType.Civ);
			player = new Player(new Vector2f(0, 0));

			mapBounds.Origin = mapBounds.Size / 2;
			mapBounds.FillColor = Color.Transparent;
			mapBounds.OutlineColor = Color.Red;
			mapBounds.OutlineThickness = 0.5f;

			testNPC.health = 100;
			player.health = 100;

			Objects.Add(player);
			Objects.Add(testNPC);
		}

		public override void Update(double deltaTime) {
			base.Update(deltaTime);

			if (!testNPC.disposed) {
				for (var k = 0; k < player.leftEmitter.projectiles.Count; k++) {
					var projectile = player.leftEmitter.projectiles[k];
					var targets = projectile.collider.targets;

					if (!testNPC.disposed) targets.Add(testNPC.collider);
				}

				for (var l = 0; l < player.rightEmitter.projectiles.Count; l++) {
					var projectile = player.rightEmitter.projectiles[l];
					var targets = projectile.collider.targets;

					if (!testNPC.disposed) targets.Add(testNPC.collider);
				}
			}
		}

		public override void Render(RenderWindow window) {
			base.Render(window);

			if (FarBeyond.showHitboxes)
				window.Draw(mapBounds);
		}
	}
}