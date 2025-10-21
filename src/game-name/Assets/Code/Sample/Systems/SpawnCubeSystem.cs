using Code.Common.Entity;
using Code.Common.Extensions;
using Entitas;
using UnityEngine;

namespace Code.Sample.Systems
{
	public class SpawnCubeSystem : IInitializeSystem
	{
		public void Initialize()
		{
			GameObject cubePrefab = Resources.Load<GameObject>("SampleCube/RegularCube");
			GameObject cube = Object.Instantiate(cubePrefab);

			CreateEntity.Empty()
				.AddName("Regular Cube")
				.With(x => x.isCube = true)
				
				.AddTransform(cube.transform)
				.AddWorldPosition(Vector3.zero)
				.AddWorldRotation(Quaternion.identity)
				
				.With(x => x.isJustCreated = true);
		}
	}
}