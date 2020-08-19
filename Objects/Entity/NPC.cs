﻿using FarBeyond.Registry;
using SFML.Graphics;
using SFML.System;

namespace FarBeyond.Objects.Entities {
	public class NPC : EntityLiving {
		float angle;
		int spriteIndex;
		Texture imageIndex;
		IntRect spriteRect;
		Sprite sprite;

		public enum NPCType {
			Civ,
			Pirate,
			Security
		}

		// TODO: replace enum with NPC registry
		public NPC(Vector2f position, float health, NPCType type) : base(position, health) {
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
		}
	}
}
