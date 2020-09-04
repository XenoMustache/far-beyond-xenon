using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xenon.Common.Objects;
using Xenon.Common.Utilities;

namespace FarBeyond.Objects {
	public class SolarSystem : GameObject {
		public AstralBody star;

		[JsonIgnore]
		bool serialized;

		[JsonProperty]
		List<Planet> planets;

		public SolarSystem(uint size) {
			star = new AstralBody(Color.Yellow, new Vector2f(0, 0));
			planets = new List<Planet>();

			for (var i = 0; i < size; i++) planets.Add(new Planet(Color.Blue));
		}

		public override void Update(double deltaTime) {
			star.Update(deltaTime);

			foreach (var planet in planets) {
				planet.Update(deltaTime);
				planet.SetOrbit(star.position, 80, 200);
				planet.showOrbit = true;
			}

			if (!serialized) {
				StartSerialization();
				serialized = true;
			}
		}

		public override void Render(RenderWindow window) {
			star.Render(window);

			foreach (var planet in planets) {
				planet.Render(window);
			}
		}

		async void StartSerialization() {
			Task task = new Task(Serialize);
			task.Start();
			await task;
		}

		void Serialize() {
			string output = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings {
				NullValueHandling = NullValueHandling.Ignore,
				//DefaultValueHandling = DefaultValueHandling.Ignore
			});
			
			Logger.Print(output, false);
		}
	}
}
