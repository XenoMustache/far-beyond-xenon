using FarBeyond.Registry;
using SFML.Graphics;
using SFML.System;

namespace FarBeyond.Objects.Entities {
	public class NPC : EntityLiving {
		float angle;
		int spriteIndex;
		IntRect spriteRect;
		Texture imageIndex;
		Sprite sprite;


		public enum NPCType {
			Civ,
			Pirate,
			Security
		}

		// TODO: replace enum with NPC registry
		public NPC(Vector2f position, NPCType type) : base(position) {
			this.position = position;

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

			spriteRect = new IntRect(new Vector2i(0 + (32 * spriteIndex), 0), new Vector2i(32, 32));
			sprite = new Sprite(imageIndex, spriteRect);

			sprite.Position = position;
			sprite.Rotation = angle;
			sprite.Origin = new Vector2f(16, 16);
		}

		public override void Render(RenderWindow window) {
			window.Draw(sprite);
		}

		public override void Update(double deltaTime) {
			sprite.Position = position;
		}
	}
}
