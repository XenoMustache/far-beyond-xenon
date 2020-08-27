using FarBeyond.Registry;
using SFML.Graphics;
using SFML.System;

namespace FarBeyond.Objects.Entities {
	public class NPC : Entity {
		public Vector2f bounds;

		float angle;
		int spriteIndex;
		Texture imageIndex;
		IntRect spriteRect;
		Sprite sprite;
		Vector2f travelPoint;

		public AIState state;

		// TODO: replace enums with registry entries
		public enum AIState {
			Wander,
		}

		public enum NPCType {
			Civ,
			Pirate,
			Security
		}

		public NPC(Vector2f position, NPCType type) : base(position) {
			this.position = position;
			angle = 0;

			switch (type) {
				case NPCType.Civ:
					imageIndex = AssetRegistry.civShipsTexture;
					spriteIndex = 1;
					break;
				case NPCType.Security:
					imageIndex = AssetRegistry.civShipsTexture;
					spriteIndex = 3;
					break;
			}

			collider = new CollisionBox(this, position, new Vector2f(14, 14), Color.Red);

			spriteRect = new IntRect(new Vector2i(0 + (32 * spriteIndex), 0), new Vector2i(32, 32));
			sprite = new Sprite(imageIndex, spriteRect);

			sprite.Position = position;
			sprite.Rotation = angle;
			sprite.Origin = new Vector2f(16, 16);
		}

		public override void Render(RenderWindow window) {
			window.Draw(sprite);

			collider.Render(window);
		}

		public override void Update(double deltaTime) {
			sprite.Position = position;

			collider.position = position;
			collider.Update(deltaTime);

			StateMachine(state);

			if (health <= 0) Dispose();
		}

		protected override void OnDispose() {
			collider.Dispose();
			base.OnDispose();
		}

		// TODO: Replace with registry
		protected virtual void StateMachine(AIState state) {
			switch (state) {
				case AIState.Wander:
					break;
			}
		}
	}
}
