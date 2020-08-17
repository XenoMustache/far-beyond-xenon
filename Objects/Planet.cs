using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;
using System;

namespace FarBeyond.Objects {
	public class Planet : AstralBody {
		public bool doOrbit, invertRotation, showOrbit;

		double timer;
		[JsonProperty]
		float orbitSpread, orbitSpeed, orbitOffset, inverted;

		[JsonProperty]
		Vector2f orbitPosition;

		[JsonIgnore]
		CircleShape orbitDisplay;

		public Planet(Color color, float size = 10) : base(color, new Vector2f(0,0), size) {
			this.color = color;
			this.size = size;

			body = new CircleShape(this.size);
			body.Origin = new Vector2f(body.Radius, body.Radius);
			body.FillColor = this.color;
			body.Position = position;

			orbitDisplay = new CircleShape();
			orbitDisplay.FillColor = Color.Transparent;
			orbitDisplay.OutlineColor = Color.White;
			orbitDisplay.OutlineThickness = 1f;
			orbitDisplay.SetPointCount(50);
		}

		public override void Update(double deltaTime) {
			if (doOrbit) {
				timer += deltaTime * orbitSpeed;

				if (!invertRotation) inverted = -1; else inverted = 1;

				float x = inverted * (float)Math.Cos(timer - orbitOffset) * orbitSpread;
				float y = (float)Math.Sin(timer - orbitOffset) * orbitSpread;
				Vector2f pos = new Vector2f(x, y);
				position = pos + orbitPosition;

				orbitDisplay.Position = orbitPosition;
				orbitDisplay.Radius = orbitSpread;
				orbitDisplay.Origin = new Vector2f(orbitDisplay.Radius, orbitDisplay.Radius);
			}

			base.Update(deltaTime);
		}

		public override void Render(RenderWindow window) {
			if (showOrbit) window.Draw(orbitDisplay);
			base.Render(window);
		}

		public void SetOrbit(Vector2f position, float speed, float distance, float offset = 0) {
			orbitPosition = position;
			orbitSpeed = speed / 100;
			orbitSpread = distance;
			orbitOffset = offset;
			doOrbit = true;
		}

		protected override void OnDispose() {
			orbitDisplay.Dispose();
			base.OnDispose();
		}
	}
}
