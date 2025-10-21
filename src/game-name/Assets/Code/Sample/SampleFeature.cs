using Code.Sample.Systems;

namespace Code.Sample
{
	public sealed class SampleFeature : Feature
	{
		public SampleFeature()
		{
			Add(new SpawnCubeSystem());
			Add(new CubeMoveAroundSystem());
			Add(new CubeRotateSystem());
			Add(new RefreshTransformSystem());
			Add(new GameWatchedCleanupSystems(Contexts.sharedInstance));
			Add(new GameCleanupSystems(Contexts.sharedInstance));
		}
	}
}