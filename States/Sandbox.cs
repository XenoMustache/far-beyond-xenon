﻿using FarBeyond.Effects;
using FarBeyond.Objects;
using FarBeyond.Objects.Entities;
using SFML.Graphics;
using SFML.System;
using Xenon.Common.State;

namespace FarBeyond.States {
	public class Sandbox : GameState {
		public Vector2f mapDimensions;

		EffectStarfield background;
		RectangleShape mapBounds;
		Player player;
		NPC testNPC2;

		public Sandbox(RenderWindow window) : base(window) {
			mapDimensions = new Vector2f(256, 256);
			background = new EffectStarfield(2, 0, 1, 0.3f);

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

			player = new Player(new Vector2f(0, 0)) {
				health = 100,
			};

			testNPC2 = new NPC(new Vector2f(32, 0), NPC.NPCType.Civ) {
				health = 100,
				defaultRotationSpeed = 0.75f,
				defaultSpeed = 25,
				isHostile = true,
				playerPosition = player.position
			};

			Objects.Add(testNPC2);
			Objects.Add(player);
		}

		public override void Update(double deltaTime) {
			background.Update(deltaTime);
			background.position = player.position;
			background.parallax = player.position;

			player.targets.Add(testNPC2.collider);

			testNPC2.playerPosition = player.position;

			base.Update(deltaTime);
		}

		public override void Render(RenderWindow window) {
			window.Draw(background);
			window.Draw(explode);
			base.Render(window);

			if (FarBeyond.showHitboxes) window.Draw(mapBounds);
		}
	}
}
