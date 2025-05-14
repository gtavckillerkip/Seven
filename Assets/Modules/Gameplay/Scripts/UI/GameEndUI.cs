using Seven.Core.ScriptableObjectsDefinitions;
using Seven.Gameplay.ScriptableObjectsDefinitions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Seven.Gameplay.UI
{
	internal sealed class GameEndUI : MonoBehaviour
	{
		public enum GameEndUIState
		{
			Lost,
			Won
		}

		[SerializeField] private Button _restartButton;
		[SerializeField] private Button _nextButton;
		[SerializeField] private Button _mainMenuButton;
		[Space]
		[SerializeField] private Image _gameLostImage;
		[SerializeField] private Image _gameWonImage;
		[Space]
		[SerializeField] private ScenesHandlerMediatorSO _scenesHandlerMediator;
		[SerializeField] private string _mainMenuSceneName;
		[Space]
		[SerializeField] private LevelInitializerMediatorSO _levelInitializerMediator;
		[SerializeField] private LevelHandlerMediatorSO _levelHandlerMediator;

		private void OnEnable()
		{
			_gameLostImage.gameObject.SetActive(false);
			_gameWonImage.gameObject.SetActive(false);

			_restartButton.onClick.AddListener(HandleRestartButtonClicked);
			_nextButton.onClick.AddListener(HandleNextButtonClicked);
			_mainMenuButton.onClick.AddListener(HandleMainMenuButtonClicked);
		}

		private void OnDisable()
		{
			_restartButton.onClick.RemoveAllListeners();
			_nextButton.onClick.RemoveAllListeners();
			_mainMenuButton.onClick.RemoveAllListeners();
		}

		public void Setup(GameEndUIState state)
		{
			switch (state)
			{
				case GameEndUIState.Lost:
					_gameLostImage.gameObject.SetActive(true);
					_nextButton.gameObject.SetActive(false);
					break;

				case GameEndUIState.Won:
					_gameWonImage.gameObject.SetActive(true);
					_nextButton.gameObject.SetActive(_levelHandlerMediator.LevelHandler.NextLevelSceneName.Length != 0);
					break;
			}
		}

		private void HandleRestartButtonClicked()
		{
			_levelInitializerMediator.LevelInitializer.Initialize();
		}

		private void HandleNextButtonClicked()
		{
			_scenesHandlerMediator.SwitchScene(_levelHandlerMediator.LevelHandler.NextLevelSceneName);
		}

		private void HandleMainMenuButtonClicked()
		{
			_scenesHandlerMediator.SwitchScene(_mainMenuSceneName);
		}
	}
}
