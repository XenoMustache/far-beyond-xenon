using SFML.Graphics;
using SFML.System;

namespace FarBeyond.Objects.Entities {
	public class EntityLiving : Entity {
		public float health, maxHealth;

		public EntityLiving(Vector2f position, float health) : base(position) {
			this.health = health;
			this.position = position;
		}

		public override void Update(double deltaTime) {
			if (health < 0) {
				Dispose();
			}
		}

		public override void Render(RenderWindow window) { }
	}
}
