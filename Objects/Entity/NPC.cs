using FarBeyond.Registry;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using SFML.Graphics;
using SFML.System;
using System;
using Xenon.Common.Utilities;

namespace FarBeyond.Objects.Entities {
	public class NPC : Entity {
		public AIState state;

		public Vector2f bounds;

		float defaultRotationSpeed = 0.75f, acceleratedRotation = 1.5f, rotationDrag = 0.1f, defaultSpeed = 25;
		float angle, rotationSpeed, speed;
		int spriteIndex, rotate;
		bool hasPoint, facingPoint;
		Texture imageIndex;
		IntRect spriteRect;
		Sprite sprite;
		CircleShape point;
		Vector2f wanderPoint;
		RandomizerNumber<float> randX, randY;

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

			point = new CircleShape(1);
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
			window.Draw(point);

			collider.Render(window);
		}

		public override void Update(double deltaTime) {
			sprite.Position = position;

			collider.position = position;
			collider.Update(deltaTime);

			switch (state) {
				case AIState.Wander:
					if (!hasPoint) {
						point.Origin = new Vector2f(point.Radius, point.Radius);
						point.FillColor = Color.Transparent;
						point.OutlineColor = Color.Red;
						point.OutlineThickness = 0.5f;

						randX = new RandomizerNumber<float>(new FieldOptionsFloat() { Max = bounds.X, Min = -bounds.X });
						randY = new RandomizerNumber<float>(new FieldOptionsFloat() { Max = bounds.Y, Min = -bounds.Y });

						wanderPoint = new Vector2f((float)randX.Generate(), (float)randY.Generate());
						hasPoint = true;
						facingPoint = false;

						point.Position = wanderPoint;

						var dist = wanderPoint.GetDistance(position);
						var dir = wanderPoint.GetDirection(position);

						Logger.Print(dist.ToString());
						Logger.Print(dir.ToString());
					} else {
						var dist = position.GetDistance(wanderPoint);
						var dir = wanderPoint.GetDirection(position);

						if (!facingPoint) {
							rotate = MiscUtils.FindTurnSideDeg(angle.RadToDeg(), dir.RadToDeg());
							speed = 0;
						} else {
							speed = defaultSpeed;
						}

						if (rotate == 0) facingPoint = true;

						//Logger.Print(angle.RadToDeg().ToString());
						//Logger.Print(dist.ToString());

						if (dist < 10) hasPoint = false;
						Logger.Print(dist.ToString());

						rotationSpeed = defaultRotationSpeed;

						angle += rotate * rotationSpeed.DegToRad();
						sprite.Rotation += rotate * rotationSpeed;
					}
					break;
			}

			position.X += (float)Math.Sin(angle) * speed * (float)deltaTime;
			position.Y += (float)-Math.Cos(angle) * speed * (float)deltaTime;

			if (health <= 0) Dispose();
		}

		protected override void OnDispose() {
			collider.Dispose();
			base.OnDispose();
		}
	}
}
