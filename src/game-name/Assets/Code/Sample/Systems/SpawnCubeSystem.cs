using Code.Common.Entity;
using Code.Common.Extensions;
using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Code.Sample.Systems
{
	public class SpawnCubeSystem : IInitializeSystem
	{
		public void Initialize()
		{
			GameObject cubePrefab = Resources.Load<GameObject>("SampleCube/RegularCube");
			GameObject cube = Object.Instantiate(cubePrefab);

			GameEntity cubeEntity = CreateEntity.Empty()
				.AddId(Identifiers.NextId())
				
				.AddName("Regular Cube")
				.With(x => x.isCube = true)
				
				.AddTransform(cube.transform)
				.AddWorldPosition(Vector3.zero)
				.AddWorldRotation(Quaternion.identity)
				
				.With(x => x.isJustCreated = true);
			
			cube.Link(cubeEntity);
		}
	}
}