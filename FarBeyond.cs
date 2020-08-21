using SFML.System;
using Xenon.Client;
using FarBeyond.Objects;
using FarBeyond.Registry;
using FarBeyond.Objects.Entities;
using static FarBeyond.Objects.Entities.Projectile;

namespace FarBeyond {
	public class FarBeyond : Game {
		public static bool showHitboxes = false;

		NPC testNPC;
		Player player;
		Projectile testProj;

		public FarBeyond(string name, Vector2u screenSize) : base(name, screenSize) { }

		protected override void PreInit() {
			AssetRegistry.Init();
			settings.AntialiasingLevel = 8;
		}

		protected override void Init() {
			window.KeyPressed += (s, e) => {
				if (e.Code == SFML.Window.Keyboard.Key.F2) {
					if (!showHitboxes) showHitboxes = true; else showHitboxes = false;
				}

				if (e.Code == SFML.Window.Keyboard.Key.Space) {
					player.leftEmitter.Fire(ProjectileEmitter.ProjectileType.playerShot);
					player.rightEmitter.Fire(ProjectileEmitter.ProjectileType.playerShot);
				}
			};

			window.SetKeyRepeatEnabled(false);

			testNPC = new NPC(new Vector2f(-32, 0), NPC.NPCType.Civ);
			player = new Player(new Vector2f(0, 0));
			testProj = new Projectile(new Vector2f(32, 0), ProjectileType.player, 0, 10);

			testProj.decay = false;

			testNPC.health = 100;
			player.health = 100;

			base.Init();
		}

		protected override void Update() {
			if (!testNPC.disposed) testNPC.Update(deltatime);
			if (!testProj.disposed) testProj.Update(deltatime);

			if (!player.disposed) player.Update(deltatime);

			if (testProj.collider.targets.Contains(player.collider))
				testProj.collider.targets.Remove(player.collider);

			testProj.collider.targets.Add(player.collider);

			if (!testNPC.disposed) {
				for (var i = 0; i < player.leftEmitter.projectiles.Count; i++) {
					var projectile = player.leftEmitter.projectiles[i];
					var targets = projectile.collider.targets;

					if (targets.Contains(testNPC.collider)) targets.Remove(testNPC.collider);
				}

				for (var j = 0; j < player.rightEmitter.projectiles.Count; j++) {
					var projectile = player.rightEmitter.projectiles[j];
					var targets = projectile.collider.targets;

					if (targets.Contains(testNPC.collider)) targets.Remove(testNPC.collider);
				}

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

		protected override void Render() {
			if (!testNPC.disposed) testNPC.Render(window);
			if (!testProj.disposed) testProj.Render(window);

			if (!player.disposed) player.Render(window);
		}

		protected override void Exit() {
			base.Exit();
			//Logger.Export();
		}
	}
}
