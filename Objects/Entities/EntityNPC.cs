using FarBeyond.Objects.AI;
using SFML.System;

namespace FarBeyond.Objects.Entities {
	public class EntityNPC : Entity {
		public float defaultSpeed, defaultRotationSpeed;
		public bool isHostile;
		public Vector2f playerPosition;

		public NPCController controller;

		public EntityNPC(Vector2f position) : base(position) { controller = new NPCController(this); }

		public override void Update(double deltaTime) {
			controller.Update(deltaTime);
			base.Update(deltaTime);
		}
	}
}
