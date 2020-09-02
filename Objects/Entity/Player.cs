﻿using FarBeyond.Registry;
using SFML.Graphics;
using SFML.System;
using Xenon.Client;
using Xenon.Client.Prefabs;
using Xenon.Common.Utilities;
using static System.Math;

namespace FarBeyond.Objects.Entities {
	public class Player : Entity {
		public ProjectileEmitter leftEmitter, rightEmitter;

		float acceleration = 0.5f, topSpeed = 100, defaultSpeed = 25, drag = 0.25f, defaultRotationSpeed = 0.75f, acceleratedRotation = 1.5f,
			rotationDrag = 0.1f;
		float speed, angle, moveAngle, rotationSpeed;
		IntRect spriteRect;
		Sprite sprite;
		Camera cam;

		public Player(Vector2f position) : base(position) {
			spriteRect = new IntRect(new Vector2i(0, 0), new Vector2i(32, 32));

			cam = new Camera();

			leftEmitter = new ProjectileEmitter(position, Color.White) {
				damage = 20,
				offset = new Vector2f(-6.5f, -10)
			};

			rightEmitter = new ProjectileEmitter(position, Color.White) {
				damage = 20,
				offset = new Vector2f(6.5f, -10)
			};

			sprite = new Sprite(AssetRegistry.civShipsTexture, spriteRect);

			rotationSpeed = defaultRotationSpeed;

			cam.camZoom = 0.5f;
			sprite.Origin = new Vector2f(spriteRect.Width / 2, spriteRect.Height / 2);

			this.position = position;

			collider = new CollisionBox(this, position, new Vector2f(12, 12), Color.Cyan);
		}

		public override void Update(double deltaTime) {
			var move = Input.GetKey(SFML.Window.Keyboard.Key.W, true);
			var rotate = Input.GetKey(SFML.Window.Keyboard.Key.D) - Input.GetKey(SFML.Window.Keyboard.Key.A);

			if (move) {
				if (speed < defaultSpeed) speed = defaultSpeed;
				if (speed < topSpeed) speed += acceleration;
				if (rotationSpeed < acceleratedRotation) rotationSpeed += rotationDrag;
				moveAngle = angle;
			} else {
				if (speed > 0) speed -= drag;
				if (rotationSpeed > defaultRotationSpeed) rotationSpeed -= rotationDrag;
			}

			angle += rotate * rotationSpeed.DegToRad();
			sprite.Rotation += rotate * rotationSpeed;

			position.X += (float)Sin(moveAngle) * speed * (float)deltaTime;
			position.Y += (float)-Cos(moveAngle) * speed * (float)deltaTime;

			sprite.Position = position;

			cam.target = position;
			cam.Update(deltaTime);

			leftEmitter.angle += rotate * rotationSpeed;
			leftEmitter.inputPosition = position;
			//leftEmitter.offset.X = -6.5f;
			//leftEmitter.offset.Y = -10;
			leftEmitter.Update(deltaTime);

			rightEmitter.angle += rotate * rotationSpeed;
			rightEmitter.inputPosition = position;
			//rightEmitter.offset.X = 6.5f;
			//rightEmitter.offset.Y = -10;
			rightEmitter.Update(deltaTime);

			collider.position = position;
			collider.Update(deltaTime);
		}

		public override void Render(RenderWindow window) {
			cam.Render(window);
			leftEmitter.Render(window);
			rightEmitter.Render(window);

			window.Draw(sprite);

			collider.Render(window);
		}
	}
}
