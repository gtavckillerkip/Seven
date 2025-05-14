using Seven.Gameplay.ScriptableObjectsDefinitions;
using System;
using System.Collections;
using UnityEngine;

namespace Seven.Gameplay
{
	internal sealed class LevelInitializer : MonoBehaviour
	{
		[SerializeField] private ActionBar _actionBar;
		[SerializeField] private Spawner _spawner;
		[Space]
		[SerializeField] private LevelInitializerMediatorSO _levelInitializerMediator;

		public event Action InitStarted;
		public event Action InitFinished;

		private void OnEnable()
		{
			_levelInitializerMediator.RegisterLevelInitializer(this);
		}

		private void OnDisable()
		{
			_levelInitializerMediator.UnregisterLevelInitializer();
		}

		public void Initialize()
		{
			StartCoroutine(Init());
		}

		private IEnumerator Init()
		{
			InitStarted?.Invoke();

			_actionBar.gameObject.SetActive(false);

			yield return new WaitForEndOfFrame();

			_spawner.SpawnShapes();

			_actionBar.gameObject.SetActive(true);

			InitFinished?.Invoke();
		}
	}
}
