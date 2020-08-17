using SFML.Graphics;
using SFML.System;
using Xenon.Common;
using Xenon.Common.Utilities;

namespace FarBeyond.Objects {
	public class CollisionBox : GameObject {
		public bool display;
		public CollisionBox targetCollider;
		public Vector2f position, size;
		public RectangleShape colliderRect;

		protected CollisionBox collided;

		bool hasEntered;
		GameObject parent;

		public CollisionBox(GameObject parent, Vector2f position, Vector2f size, Color color) {
			this.parent = parent;
			this.position = position;
			this.size = size;

			colliderRect = new RectangleShape(size);

			colliderRect.Origin = size / 2;
			colliderRect.FillColor = Color.Transparent;
			colliderRect.OutlineColor = color;
			colliderRect.OutlineThickness = 0.5f;
		}

		public virtual void OnColliderEnter() {
			Logger.Print("Enter");
			collided = targetCollider;
		}

		public virtual void OnColliderExit() {
			collided = null;
		}

		public GameObject GetParent() { return parent; }

		public override void Update(double deltaTime) {
			colliderRect.Position = position;

			if (targetCollider != null && colliderRect.GetGlobalBounds().Intersects(targetCollider.colliderRect.GetGlobalBounds()) && !hasEntered) {
				hasEntered = true;
				OnColliderEnter();
			} else if (targetCollider != null && !colliderRect.GetGlobalBounds().Intersects(targetCollider.colliderRect.GetGlobalBounds()) && hasEntered) {
				hasEntered = false;
				OnColliderExit();
			}

			display = FarBeyond.showHitboxes;
		}

		public override void Render(RenderWindow window) {
			if (display) window.Draw(colliderRect);
		}
	}
}
