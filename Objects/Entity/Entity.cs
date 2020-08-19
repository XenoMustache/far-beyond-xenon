﻿using SFML.Graphics;
using SFML.System;
using Xenon.Common;

namespace FarBeyond.Objects.Entities {
	public class Entity : GameObject {
		public Vector2f position;
		public CollisionBox collider;

		public Entity(Vector2f position) { this.position = position; }

		public override void Render(RenderWindow window) { }

		public override void Update(double deltaTime) { }
	}
}