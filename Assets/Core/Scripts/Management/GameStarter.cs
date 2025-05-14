using Seven.Core.ScriptableObjectsDefinitions;
using UnityEngine;

namespace Seven.Core.Management
{
	internal sealed class GameStarter : MonoBehaviour
	{
		[SerializeField] private ScenesHandlerMediatorSO _scenesHandlerMediator;
		[Space]
		[SerializeField] private string _mainMenuSceneName;

		private void Start()
		{
			_scenesHandlerMediator.SwitchScene(_mainMenuSceneName);
		}
	}
}
