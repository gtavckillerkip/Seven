using Seven.Gameplay.Shapes;
using System;
using UnityEngine;

namespace Seven.Gameplay.ScriptableObjectsDefinitions
{
	[CreateAssetMenu(menuName = "Scriptable objects/Gameplay/SpawnerMediator")]
	internal sealed class SpawnerMediatorSO : ScriptableObject
	{
		private Spawner _spawner;

		public event System.Action SpawningStarted;
		public event System.Action SpawningFinished;
		public event Action<Shape> ShapeSpawned;
		public event System.Action AllShapesRemovedFromBox;

		public Spawner Spawner => _spawner;

		public void RegisterSpawner(Spawner spawner)
		{
			if (spawner == null)
			{
				return;
			}

			UnregisterSpawner();

			_spawner = spawner;

			_spawner.SpawningStarted += HandleSpawningStarted;
			_spawner.SpawningFinished += HandleSpawningFinished;
			_spawner.ShapeSpawned += HandleShapeSpawned;
			_spawner.AllShapesRemovedFromBox += HandleAllShapesRemovedFromBox;
		}

		public void UnregisterSpawner()
		{
			if (_spawner != null)
			{
				_spawner.SpawningStarted -= HandleSpawningStarted;
				_spawner.SpawningFinished -= HandleSpawningFinished;
				_spawner.ShapeSpawned -= HandleShapeSpawned;
				_spawner.AllShapesRemovedFromBox -= HandleAllShapesRemovedFromBox;
			}

			_spawner = null;
		}

		private void HandleSpawningStarted() => SpawningStarted?.Invoke();

		private void HandleSpawningFinished() => SpawningFinished?.Invoke();

		private void HandleShapeSpawned(Shape shape) => ShapeSpawned?.Invoke(shape);

		private void HandleAllShapesRemovedFromBox() => AllShapesRemovedFromBox?.Invoke();
	}
}
