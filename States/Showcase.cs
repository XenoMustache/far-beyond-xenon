using FarBeyond.Effects;
using SFML.Graphics;
using SFML.System;
using Xenon.Client.Objects;
using Xenon.Common.State;

namespace FarBeyond.States {
	public class Showcase : GameState {
		Camera camera;
		Clock backgroundClock;
		Starfield background;

		public Showcase(RenderWindow window) : base(window) {
			window.SetMouseCursorVisible(false);

			camera = new Camera();

			backgroundClock = new Clock();
			background = new Starfield(2, 0.4f, 1, 0.5f);

			camera.camZoom = 0.5f;
			camera.target = new Vector2f(0, 0);
		}

		public override void Render(RenderWindow window) {
			base.Render(window);

			window.Draw(background);
			camera.Render(window);
		}

		public override void Update(double deltaTime) {
			base.Update(deltaTime);

			background.parallax += new Vector2f(0.5f, 0.5f);
			background.Update(backgroundClock.ElapsedTime.AsSeconds());
			camera.Update(deltaTime);
		}
	}
}
