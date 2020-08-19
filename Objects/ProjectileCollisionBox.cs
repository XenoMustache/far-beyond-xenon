using FarBeyond.Objects.Entities;
using SFML.Graphics;
using SFML.System;
using Xenon.Common.Utilities;

namespace FarBeyond.Objects {
	public class ProjectileCollisionBox : CollisionBox {
		public float damage;

		public ProjectileCollisionBox(Entity parent, Vector2f position, Vector2f size, float damage, Color color) : base(parent, position, size, color) {
			this.damage = damage;
		}

		public override void OnColliderEnter(CollisionBox collided) {
			var obj = collided.GetParent();
			obj.health -= damage;

			base.OnColliderEnter(collided);
			Logger.Print($"Object health now {obj.health}");
			parent.Dispose();
		}
	}
}
