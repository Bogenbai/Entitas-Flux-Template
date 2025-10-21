using Entitas;
using UnityEngine;

namespace Code.Sample.Systems
{
	public class CubeRotateSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _cubes;

		public CubeRotateSystem()
		{
			_cubes = Contexts.sharedInstance.game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Cube,
					GameMatcher.WorldRotation));
		}
		
		public void Execute()
		{
			foreach (GameEntity cube in _cubes.GetEntities())
			{
				Quaternion rotation = cube.WorldRotation;
				rotation *= Quaternion.Euler(0f, 90f * Time.deltaTime, 0f);
				cube.ReplaceWorldRotation(rotation);
			}
		}
	}
}