using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Common
{
    [Game] public class Id : IComponent { [PrimaryEntityIndex] public int Value; }
    [Game, Watched] public class Name : IComponent { public string Value; }
    
    [Game] public class TransformComponent : IComponent { public UnityEngine.Transform Value; }
    [Game] public class WorldRotation : IComponent { public UnityEngine.Quaternion Value; }
    [Game] public class WorldPosition : IComponent { public UnityEngine.Vector3 Value; }
}