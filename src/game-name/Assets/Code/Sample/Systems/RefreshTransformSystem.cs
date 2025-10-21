using Entitas;

namespace Code.Sample.Systems
{
	public class RefreshTransformSystem : IExecuteSystem
	{
		private readonly IGroup<GameEntity> _transforms;

		public RefreshTransformSystem()
		{
			_transforms = Contexts.sharedInstance.game.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Transform,
					GameMatcher.WorldPosition,
					GameMatcher.WorldRotation));
		}
		
		public void Execute()
		{
			foreach (GameEntity entity in _transforms)
			{
				entity.Transform.position = entity.WorldPosition;
				entity.Transform.rotation = entity.WorldRotation;
			}
		}
	}
}