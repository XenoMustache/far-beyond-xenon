using FarBeyond.Objects.Entities;
using SFML.Graphics;
using SFML.System;

namespace FarBeyond.Objects {
	public class ProjectileCollisionBox : CollisionBox {
		public ProjectileCollisionBox(Entity parent, Vector2f position, Vector2f size, Color color) : base(parent, position, size, color) { }

		public override void OnColliderEnter(CollisionBox collided) {
			var obj = collided.GetParent();

			base.OnColliderEnter(collided);
		}
	}
}
