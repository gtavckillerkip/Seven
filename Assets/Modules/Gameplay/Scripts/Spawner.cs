using Seven.Gameplay.ScriptableObjectsDefinitions;
using Seven.Gameplay.Shapes;
using System;
using System.Collections;
using UnityEngine;

namespace Seven.Gameplay
{
	internal sealed class Spawner : MonoBehaviour
	{
		[SerializeField] private GameObject[] _shapesPrefabs;
		[SerializeField] private Texture2D[] _shapeFramesColoringTextures;
		[SerializeField] private Sprite[] _shapeForegrounds;
		[Space]
		[SerializeField] private Transform _spawnToParent;
		[Space]
		[SerializeField] private float _spawnDeltaTime;
		[Tooltip("The amount of spawned shapes will be three times bigger than this number.")]
		[SerializeField] private int _spawningTriplesCount;
		[Space]
		[SerializeField] private SpawnerMediatorSO _spawnerMediator;

		private WaitForSeconds _spawnDeltaTimeWait;

		public event Action SpawningStarted;
		public event Action SpawningFinished;
		public event Action<Shape> ShapeSpawned;
		public event Action AllShapesRemovedFromBox;

		private void OnEnable()
		{
			_spawnerMediator.RegisterSpawner(this);
			_spawnDeltaTimeWait = new(_spawnDeltaTime);
		}

		private void OnDisable()
		{
			_spawnerMediator.UnregisterSpawner();
		}

		public void SpawnShapes()
		{
			SpawnShapes(_spawningTriplesCount * 3);
		}

		public void SpawnShapes(int amount)
		{
			for (int i = 0; i < _spawnToParent.childCount; i++)
			{
				Destroy(_spawnToParent.GetChild(i).gameObject);
			}
			AllShapesRemovedFromBox?.Invoke();

			StartCoroutine(Spawn(amount));
		}

		private IEnumerator Spawn(int amount)
		{
			SpawningStarted?.Invoke();

			for (int i = 0; i < amount; i++)
			{
				var randomIndex = UnityEngine.Random.Range(0, _shapesPrefabs.Length);

				var o = Instantiate(_shapesPrefabs[randomIndex], _spawnToParent);
				o.transform.position = transform.position;

				var shape = o.GetComponent<Shape>();

				randomIndex = UnityEngine.Random.Range(0, _shapeFramesColoringTextures.Length);

				shape.FrameSpriteRenderer.color = _shapeFramesColoringTextures[randomIndex].GetPixel(0, 0);

				randomIndex = UnityEngine.Random.Range(0, _shapeForegrounds.Length);

				shape.ForegroundSpriteRenderer.sprite = _shapeForegrounds[randomIndex];

				ShapeSpawned?.Invoke(shape);

				yield return _spawnDeltaTimeWait;
			}

			SpawningFinished?.Invoke();
		}
	}
}
