using SFML.System;
using SFML.Graphics;
using Xenon.Client;
using Seed.Objects;
using Seed.Registry;
using static Seed.Objects.Projectile;
using Xenon.Common.Utilities;

namespace Seed {
	public class FarBeyond : Game {
		public static bool showHitboxes = false;

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
			};

			player = new Player(new Vector2f(0, 0));
			testProj = new Projectile(ProjectileType.player);
			testProj.position = new Vector2f(32, 0);

			base.Init();
		}

		protected override void Update() {
			player.Update(deltatime);
			testProj.Update(deltatime);

			testProj.collider.targetCollider = player.collider;
		}

		protected override void Render() {
			player.Render(window);
			testProj.Render(window);
		}

		protected override void Exit() {
			base.Exit();
			//Logger.Export();
		}
	}
}
