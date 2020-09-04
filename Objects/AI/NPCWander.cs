using FarBeyond.Objects.Entities;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using SFML.System;
using Xenon.Common.Objects;
using Xenon.Common.Utilities;

namespace FarBeyond.Objects.AI {
	public class NPCWander : AIState {
		int rotate;
		bool hasPoint, facingPoint;
		Vector2f bounds, seekingPoint;

		public NPCWander(Vector2f bounds) { this.bounds = bounds; }

		public override void Exectute(double deltaTime) {
			if (!hasPoint) {
				var randX = new RandomizerNumber<float>(new FieldOptionsFloat() { Max = bounds.X, Min = -bounds.X });
				var randY = new RandomizerNumber<float>(new FieldOptionsFloat() { Max = bounds.Y, Min = -bounds.Y });

				seekingPoint = new Vector2f((float)randX.Generate(), (float)randY.Generate());
				hasPoint = true;
				facingPoint = false;
			} else {
				var p = (EntityNPC)controller.parent;
				var dist = p.position.GetDistance(seekingPoint);
				var dir = seekingPoint.GetDirection(p.position);

				if (!facingPoint) {
					rotate = MiscUtils.FindTurnSideDeg(p.angle.RadToDeg(), dir.RadToDeg());
					p.speed = 0;
				} else {
					p.speed = p.defaultSpeed;
				}

				if (rotate == 0) facingPoint = true;
				if (dist < 10) hasPoint = false;

				p.rotationSpeed = p.defaultRotationSpeed;
				p.angle += rotate * p.rotationSpeed.DegToRad();
				p.sprite.Rotation += rotate * p.rotationSpeed;

				var c = (NPCController)controller;

				//if (p.isHostile && p.position.GetDistance(p.playerPosition) < 128) c.currentState = c.attack;
			}
		}
	}
}
