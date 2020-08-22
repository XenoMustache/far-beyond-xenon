using SFML.System;
using Xenon.Client;
using FarBeyond.Registry;
using Xenon.Common.State;
using FarBeyond.States;

namespace FarBeyond {
	public class FarBeyond : Game {
		public static bool showHitboxes = false;
		public StateManager stateManager;

		public FarBeyond(string name, Vector2u screenSize) : base(name, screenSize) { }

		protected override void PreInit() {
			AssetRegistry.Init();
			settings.AntialiasingLevel = 8;
		}

		protected override void Init() {
			stateManager = new StateManager();

			window.KeyPressed += (s, e) => {
				if (e.Code == SFML.Window.Keyboard.Key.F2) {
					if (!showHitboxes) showHitboxes = true; else showHitboxes = false;
				}
			};

			window.SetKeyRepeatEnabled(false);

			stateManager.AddState(new Sandbox(window), 1);
			stateManager.Goto(1, false, true);

			base.Init();
		}

		protected override void Update() {
			stateManager.currentState.Update(deltatime);
		}

		protected override void Render() {
			stateManager.currentState.Render(window);
		}

		protected override void Exit() {
			base.Exit();
			//Logger.Export();
		}
	}
}
