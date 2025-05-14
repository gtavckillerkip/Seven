using Seven.Core.ScriptableObjectsDefinitions;
using Seven.Gameplay.ScriptableObjectsDefinitions;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Seven.Gameplay.UI
{
	internal sealed class MenuUI : MonoBehaviour
	{
		[SerializeField] private Button _resumeButton;
		[SerializeField] private Button _restartButton;
		[SerializeField] private Button _toMainMenuButton;
		[Space]
		[SerializeField] private string _mainMenuSceneName;
		[Space]
		[SerializeField] private LevelInitializerMediatorSO _levelInitializerMediator;
		[SerializeField] private ScenesHandlerMediatorSO _scenesHandlerMediator;

		public event Action ResumeButtonClicked;

		private void OnEnable()
		{
			_resumeButton.onClick.AddListener(HandleResumeButtonClicked);
			_restartButton.onClick.AddListener(HandleRestartButtonClicked);
			_toMainMenuButton.onClick.AddListener(HandleToMainMenuButtonClicked);
		}

		private void OnDisable()
		{
			_resumeButton.onClick.RemoveAllListeners();
			_restartButton.onClick.RemoveAllListeners();
			_toMainMenuButton.onClick.RemoveAllListeners();
		}

		private void HandleResumeButtonClicked()
		{
			ResumeButtonClicked?.Invoke();
		}

		private void HandleRestartButtonClicked()
		{
			gameObject.SetActive(false);
			_levelInitializerMediator.LevelInitializer.Initialize();
		}

		private void HandleToMainMenuButtonClicked()
		{
			_scenesHandlerMediator.SwitchScene(_mainMenuSceneName);
		}
	}
}
