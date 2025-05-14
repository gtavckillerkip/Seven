using System;
using UnityEngine;
using UnityEngine.UI;

namespace Seven.MainMenu.UI
{
	internal sealed class MainScreenUI : MonoBehaviour
	{
		[SerializeField] private Button _playButton;
		[SerializeField] private Button _quitButton;

		public event Action PlayButtonClicked;

		private void OnEnable()
		{
			_playButton.onClick.AddListener(HandlePlayButtonClicked);
			_quitButton.onClick.AddListener(HandleQuitButtonClicked);
		}

		private void OnDisable()
		{
			_playButton.onClick.RemoveAllListeners();
			_quitButton.onClick.RemoveAllListeners();
		}

		private void HandlePlayButtonClicked()
		{
			PlayButtonClicked?.Invoke();
		}

		private void HandleQuitButtonClicked()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
#else
			Application.Quit();
#endif
		}
	}
}
