using UnityEngine;

namespace Seven.MainMenu.UI
{
	internal sealed class UIController : MonoBehaviour
	{
		[SerializeField] private MainScreenUI _mainScreenUI;
		[SerializeField] private LevelsScreenUI _levelsScreenUI;

		private void OnEnable()
		{
			_mainScreenUI.gameObject.SetActive(true);
			_levelsScreenUI.gameObject.SetActive(false);

			_mainScreenUI.PlayButtonClicked += HandlePlayButtonClicked;
			_levelsScreenUI.BackButtonClicked += HandleBackToMainScreenButtonClicked;
		}

		private void OnDisable()
		{
			_mainScreenUI.gameObject.SetActive(false);
			_levelsScreenUI.gameObject.SetActive(false);

			_mainScreenUI.PlayButtonClicked -= HandlePlayButtonClicked;
			_levelsScreenUI.BackButtonClicked -= HandleBackToMainScreenButtonClicked;
		}

		private void HandlePlayButtonClicked()
		{
			_mainScreenUI.gameObject.SetActive(false);
			_levelsScreenUI.gameObject.SetActive(true);
		}

		private void HandleBackToMainScreenButtonClicked()
		{
			_levelsScreenUI.gameObject.SetActive(false);
			_mainScreenUI.gameObject.SetActive(true);
		}
	}
}
