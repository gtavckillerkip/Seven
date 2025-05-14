using Seven.Gameplay.ScriptableObjectsDefinitions;
using UnityEngine;

namespace Seven.Gameplay.UI
{
	internal sealed class UIController : MonoBehaviour
	{
		[SerializeField] private GameplayUI _gameplayUI;
		[SerializeField] private GameEndUI _gameEndUI;
		[SerializeField] private MenuUI _menuUI;
		[Space]
		[SerializeField] private GameLogicHandler _gameLogicHandler;
		[SerializeField] private LevelInitializerMediatorSO _levelInitializerMediator;

		private void OnEnable()
		{
			_gameplayUI.gameObject.SetActive(true);
			_gameEndUI.gameObject.SetActive(false);

			_gameLogicHandler.GameLost += HandleGameLost;
			_gameLogicHandler.GameWon += HandleGameWon;

			_gameplayUI.MenuButtonClicked += HandleMenuButtonClicked;
			_menuUI.ResumeButtonClicked += HandleResumeButtonClicked;

			_levelInitializerMediator.LevelInitStarted += HandleLevelInitStarted;
			_levelInitializerMediator.LevelInitFinished += HandleLevelInitFinished;
		}

		private void OnDisable()
		{
			_gameEndUI.gameObject.SetActive(false);
			_gameEndUI.gameObject.SetActive(false);

			_gameLogicHandler.GameLost -= HandleGameLost;
			_gameLogicHandler.GameWon -= HandleGameWon;

			_gameplayUI.MenuButtonClicked -= HandleMenuButtonClicked;
			_menuUI.ResumeButtonClicked -= HandleResumeButtonClicked;

			_levelInitializerMediator.LevelInitStarted -= HandleLevelInitStarted;
			_levelInitializerMediator.LevelInitFinished -= HandleLevelInitFinished;
		}

		private void HandleGameWon()
		{
			_gameplayUI.gameObject.SetActive(false);
			_gameEndUI.gameObject.SetActive(true);
			_gameEndUI.Setup(GameEndUI.GameEndUIState.Won);
		}

		private void HandleGameLost()
		{
			_gameplayUI.gameObject.SetActive(false);
			_gameEndUI.gameObject.SetActive(true);
			_gameEndUI.Setup(GameEndUI.GameEndUIState.Lost);
		}

		private void HandleLevelInitStarted()
		{
			_gameplayUI.gameObject.SetActive(false);
			_gameEndUI.gameObject.SetActive(false);
		}

		private void HandleLevelInitFinished()
		{
			_gameplayUI.gameObject.SetActive(true);
		}

		private void HandleMenuButtonClicked()
		{
			_gameplayUI.gameObject.SetActive(false);
			_menuUI.gameObject.SetActive(true);
		}

		private void HandleResumeButtonClicked()
		{
			_menuUI.gameObject.SetActive(false);
			_gameplayUI.gameObject.SetActive(true);
		}
	}
}
