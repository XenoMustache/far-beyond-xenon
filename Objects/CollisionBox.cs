using FarBeyond.Objects.Entities;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using Xenon.Common;

namespace FarBeyond.Objects {
	public class CollisionBox : GameObject {
		public bool display;
		public Vector2f position, size;
		public RectangleShape colliderRect;
		public List<CollisionBox> targets;

		protected Entity parent;

		bool hasEntered;

		public CollisionBox(Entity parent, Vector2f position, Vector2f size, Color color) {
			this.parent = parent;
			this.position = position;
			this.size = size;

			targets = new List<CollisionBox>();

			colliderRect = new RectangleShape(size) {
				Origin = size / 2,
				FillColor = Color.Transparent,
				OutlineColor = color,
				OutlineThickness = 0.5F
			};
		}

		public virtual void OnColliderEnter(CollisionBox collided) {
			//Logger.Print($"Collided with object at X:{Math.Floor(collided.position.X)}, Y:{Math.Floor(collided.position.Y)}");
		}

		public virtual void OnColliderExit(CollisionBox collided) { }

		public Entity GetParent() { return parent; }

		public override void Update(double deltaTime) {
			colliderRect.Position = position;

			for (var i = 0; i < targets.Count; i++) {
				var box = targets[i];

				if (!box.parent.disposed && !disposed) {
					if (colliderRect.GetGlobalBounds().Intersects(box.colliderRect.GetGlobalBounds()) && !hasEntered) {
						hasEntered = true;
						OnColliderEnter(box);
					} else if (!colliderRect.GetGlobalBounds().Intersects(box.colliderRect.GetGlobalBounds()) && hasEntered) {
						hasEntered = false;
						OnColliderExit(box);
					}
				}
			}

			targets.Clear();
			display = FarBeyond.showHitboxes;
		}

		public override void Render(RenderWindow window) {
			if (display) window.Draw(colliderRect);
		}

		protected override void OnDispose() {
			colliderRect.Dispose();

			base.OnDispose();
		}
	}
}
