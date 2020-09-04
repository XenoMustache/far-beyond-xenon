using FarBeyond.Objects.Entities;
using Xenon.Common.Objects;

namespace FarBeyond.Objects.AI {
	public class NPCController : AIController {
		public AIState wander;

		public NPCController(GameObject parent) : base(parent) {
			wander = new NPCWander(new SFML.System.Vector2f(256, 256));

			currentState = wander;
		}
	}
}
