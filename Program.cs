using System;
using Xenon.Common.Utilities;

namespace FarBeyond {
	class Program {
		public static readonly string VERSION = "Build 1";

		static void Main(string[] args) {
			Console.Title = "Far Beyond debug console";
			MiscUtils.HideConsole(false);
			new FarBeyond($"Far Beyond - Xenon - {VERSION}", new SFML.System.Vector2u(856, 482));
		}
	}
}
