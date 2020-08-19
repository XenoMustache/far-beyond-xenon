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
			testProj = new Projectile(new Vector2f(32, 0), ProjectileType.player, 0);

			base.Init();
		}

		protected override void Update() {
			testNPC.Update(deltatime);
			if (!testProj.disposed) testProj.Update(deltatime);
			player.Update(deltatime);

			if (testProj.collider.targets.Contains(player.collider))
				testProj.collider.targets.Remove(player.collider);
			testProj.collider.targets.Add(player.collider);
		}

		protected override void Render() {
			testNPC.Render(window);
			if (!testProj.disposed) testProj.Render(window);
			player.Render(window);
		}

		protected override void Exit() {
			base.Exit();
			//Logger.Export();
		}
	}
}
