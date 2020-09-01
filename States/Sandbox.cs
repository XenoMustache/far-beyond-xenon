using FarBeyond.Objects;
using FarBeyond.Objects.Entities;
using SFML.Graphics;
using SFML.System;
using Xenon.Common.State;
using Xenon.Common.Utilities;

namespace FarBeyond.States {
	public class Sandbox : GameState {
		public Vector2f mapDimensions;

		RectangleShape mapBounds;
		Player player;
		NPC testNPC, testNPC2;

		public Sandbox(RenderWindow window) : base(window) {
			mapDimensions = new Vector2f(256, 256);

			window.KeyPressed += (s, e) => {
				if (e.Code == SFML.Window.Keyboard.Key.Space) {
					player.leftEmitter.Fire(ProjectileEmitter.ProjectileType.playerShot);
					player.rightEmitter.Fire(ProjectileEmitter.ProjectileType.playerShot);
				}
			};

			mapBounds = new RectangleShape(mapDimensions * 2) {
				FillColor = Color.Transparent,
				OutlineColor = Color.Red,
				OutlineThickness = 0.5F
			};

			mapBounds.Origin = mapBounds.Size / 2;

			testNPC = new NPC(new Vector2f(-32, 0), NPC.NPCType.Civ) {
				health = 100,
				bounds = mapDimensions,
				state = NPC.AIState.Wander
			};

			testNPC2 = new NPC(new Vector2f(32, 0), NPC.NPCType.Civ) {
				health = 100,
				bounds = mapDimensions,
				state = NPC.AIState.Wander,
				isHostile = true
			};

			player = new Player(new Vector2f(0, 0)) {
				health = 100,
			};

			Objects.Add(testNPC);
			Objects.Add(testNPC2);
			Objects.Add(player);
		}

		public override void Update(double deltaTime) {
			// TODO: Clean this up
			// TODO: Fix the former list issue
			for (var k = 0; k < player.leftEmitter.projectiles.Count; k++) {
				var projectile = player.leftEmitter.projectiles[k];
				var targets = projectile.collider.targets;

				if (!testNPC.disposed) targets.Add(testNPC.collider);
				if (!testNPC2.disposed) targets.Add(testNPC2.collider);
			}

			for (var l = 0; l < player.rightEmitter.projectiles.Count; l++) {
				var projectile = player.rightEmitter.projectiles[l];
				var targets = projectile.collider.targets;

				if (!testNPC.disposed) targets.Add(testNPC.collider);
				if (!testNPC2.disposed) targets.Add(testNPC2.collider);
			}

			base.Update(deltaTime);
		}

		public override void Render(RenderWindow window) {
			base.Render(window);

			if (FarBeyond.showHitboxes)
				window.Draw(mapBounds);
		}
	}
}
