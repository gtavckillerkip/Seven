using Seven.Core.ScriptableObjectsDefinitions;
using Seven.Gameplay.ScriptableObjectsDefinitions;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Seven.Gameplay.UI
{
	internal sealed class GameplayUI : MonoBehaviour
	{
		[SerializeField] private Button _respawnButton;
		[SerializeField] private Button _menuButton;
		[Space]
		[SerializeField] private SpawnerMediatorSO _spawnerMediator;
		[SerializeField] private GameLogicHandler _logicHandler;

		public event Action MenuButtonClicked;

		private void OnEnable()
		{
			_respawnButton.gameObject.SetActive(false);

			_spawnerMediator.SpawningStarted += HandleSpawningStarted;
			_spawnerMediator.SpawningFinished += HandleSpawningFinished;
			_respawnButton.onClick.AddListener(HandleRespawnButtonClicked);
			_menuButton.onClick.AddListener(HandleMenuButtonClicked);
		}

		private void OnDisable()
		{
			_spawnerMediator.SpawningStarted -= HandleSpawningStarted;
			_spawnerMediator.SpawningFinished -= HandleSpawningFinished;
			_respawnButton.onClick.RemoveAllListeners();
			_menuButton.onClick.RemoveAllListeners();
		}

		private void HandleSpawningStarted()
		{
			_respawnButton.gameObject.SetActive(false);
		}

		private void HandleSpawningFinished()
		{
			_respawnButton.gameObject.SetActive(true);
		}

		private void HandleRespawnButtonClicked()
		{
			_spawnerMediator.Spawner.SpawnShapes(_logicHandler.CurrentShapesInBoxAmount);
		}

		private void HandleMenuButtonClicked()
		{
			MenuButtonClicked?.Invoke();
		}
	}
}
