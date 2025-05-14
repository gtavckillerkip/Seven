using System;
using Seven.Core.ScriptableObjectsDefinitions;
using UnityEngine;
using UnityEngine.UI;

namespace Seven.MainMenu.UI
{
	internal sealed class LevelsScreenUI : MonoBehaviour
	{
		[Serializable]
		private struct ButtonScene
		{
			public Button Button;
			public string SceneName;
		}

		[SerializeField] private Button _backButton;
		[Space]
		[SerializeField] private ButtonScene[] _buttonScenes;
		[Space]
		[SerializeField] private ScenesHandlerMediatorSO _scenesHandlerMediator;

		public event Action BackButtonClicked;

		private void OnEnable()
		{
			_backButton.onClick.AddListener(HandleBackButtonClicked);
			for (int i = 0; i < _buttonScenes.Length; i++)
			{
				var sceneName = _buttonScenes[i].SceneName;
				_buttonScenes[i].Button.onClick.AddListener(() => HandleLevelButtonClicked(sceneName));
			}
		}

		private void OnDisable()
		{
			_backButton.onClick.RemoveAllListeners();
			for (int i = 0; i < _buttonScenes.Length; i++)
			{
				_buttonScenes[i].Button.onClick.RemoveAllListeners();
			}
		}

		private void HandleBackButtonClicked()
		{
			BackButtonClicked?.Invoke();
		}

		private void HandleLevelButtonClicked(string sceneName)
		{
			_scenesHandlerMediator.SwitchScene(sceneName);
		}
	}
}
