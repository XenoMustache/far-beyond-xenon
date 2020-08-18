using SFML.Graphics;
using SFML.System;
using Xenon.Common;
using Xenon.Common.Utilities;

namespace FarBeyond.Objects.Entities {
	public class Entity : GameObject {
		public Vector2f position;
		public CollisionBox collider;

		public Entity(Vector2f position) {
			this.position = position;
			Logger.Print($"Entity created at X:{position.X} Y:{position.Y}");
		}

		public override void Render(RenderWindow window) { }

		public override void Update(double deltaTime) { }
	}
}
