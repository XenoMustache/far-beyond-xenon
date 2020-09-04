using FarBeyond.Effects;
using FarBeyond.Objects;
using FarBeyond.Objects.Entities;
using SFML.Graphics;
using SFML.System;
using Xenon.Common.State;

namespace FarBeyond.States {
	public class Sandbox : GameState {
		public Vector2f mapDimensions;

		Clock backgroundClock;
		Starfield background;
		RectangleShape mapBounds;
		Player player;
		NPC testNPC, testNPC2;

		public Sandbox(RenderWindow window) : base(window) {
			mapDimensions = new Vector2f(256, 256);

			backgroundClock = new Clock();

			background = new Starfield(2, 0, 1, 0.3f);

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

			player = new Player(new Vector2f(0, 0)) {
				health = 100,
			};

			testNPC2 = new NPC(new Vector2f(32, 0), NPC.NPCType.Civ) {
				health = 100,
				bounds = mapDimensions,
				state = NPC.AIState.Wander,
				isHostile = true,
				playerPosition = player.position
			};

			Objects.Add(testNPC);
			Objects.Add(testNPC2);
			Objects.Add(player);
		}

		public override void Update(double deltaTime) {
			player.targets.Add(testNPC.collider);
			player.targets.Add(testNPC2.collider);

			testNPC2.playerPosition = player.position;

			background.Update(backgroundClock.ElapsedTime.AsSeconds());
			background.position = player.position;
			background.parallax = player.position;

			base.Update(deltaTime);
		}

		public override void Render(RenderWindow window) {
			window.Clear(Color.Blue);
			window.Draw(background);
			base.Render(window);

			if (FarBeyond.showHitboxes) window.Draw(mapBounds);
		}
	}
}
