using SFML.Graphics;
using SFML.System;

namespace FarBeyond.Objects.Entities {
	public class EntityLiving : Entity {
		public float health;

		public EntityLiving(Vector2f position) : base(position) {
			this.position = position;
		}

		public override void Update(double deltaTime) { }

		public override void Render(RenderWindow window) { }
	}
}
