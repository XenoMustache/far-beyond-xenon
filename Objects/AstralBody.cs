using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;
using Xenon.Common;

namespace Seed.Objects {
	public class AstralBody : GameObject {
		[JsonIgnore]
		public CircleShape body;
		public Vector2f position;

		[JsonProperty]
		protected float size;
		[JsonProperty]
		protected Color color;

		public AstralBody(Color color, Vector2f position, float size = 50) {
			this.position = position;
			
			this.color = color;
			this.size = size;

			body = new CircleShape(this.size);
			body.Origin = new Vector2f(body.Radius, body.Radius);
			body.FillColor = this.color;
			body.Position = position;

			body.SetPointCount(50);
		}

		public override void Update(double deltaTime) {
			body.Position = position;
		}

		public override void Render(RenderWindow window) {
			window.Draw(body);
		}

		protected override void OnDispose() {
			body.Dispose();
			base.OnDispose();
		}
	}
}
