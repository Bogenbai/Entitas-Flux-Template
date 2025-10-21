using UnityEngine;

namespace Code.Sample
{
	public class EcsRunner : MonoBehaviour
	{
		private SampleFeature _sampleFeature;

		private void Awake()
		{
			_sampleFeature = new SampleFeature();
		}

		private void Start()
		{
			_sampleFeature.Initialize();
		}
		
		private void Update()
		{
			_sampleFeature.Execute();
			_sampleFeature.Cleanup();
		}
		
		private void OnDestroy()
		{
			_sampleFeature.TearDown();
		}
	}
}