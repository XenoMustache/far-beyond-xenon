using SFML.System;
using SFML.Graphics;
using Xenon.Client;
using FarBeyond.Objects;
using FarBeyond.Registry;
using static FarBeyond.Objects.Projectile;

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
			};

			testNPC = new NPC(NPC.NPCType.Civ, new Vector2f(-32, 0));
			player = new Player(new Vector2f(0, 0));
			testProj = new Projectile(ProjectileType.player, new Vector2f(32, 0), 0);

			base.Init();
		}

		protected override void Update() {
			testNPC.Update(deltatime);
			testProj.Update(deltatime);
			player.Update(deltatime);

			testProj.collider.targetCollider = player.collider;
		}

		protected override void Render() {
			testNPC.Render(window);
			testProj.Render(window);
			player.Render(window);
		}

		protected override void Exit() {
			base.Exit();
			//Logger.Export();
		}
	}
}
