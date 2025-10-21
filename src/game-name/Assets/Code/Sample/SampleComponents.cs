using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Sample
{
	[Game] public class Cube : IComponent { }
	[Game, Cleanup(CleanupMode.RemoveComponent)] public class JustCreated : IComponent { }
}