using Entitas;
using UnityEngine;

namespace Code.Sample.Systems
{
	public class CubeMoveAroundSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _cubes;

		public CubeMoveAroundSystem()
		{
			_cubes = Contexts.sharedInstance.game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Cube,
					GameMatcher.WorldPosition));
		}
		
		public void Execute()
		{
			foreach (GameEntity cube in _cubes.GetEntities())
			{
				Vector3 position = cube.WorldPosition;
				position.x = Mathf.Sin(Time.time) * 5f;
				cube.ReplaceWorldPosition(position);
			}
		}
	}
}