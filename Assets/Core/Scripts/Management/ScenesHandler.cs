using Seven.Core.ScriptableObjectsDefinitions;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Seven.Core.Management
{
	internal sealed class ScenesHandler : MonoBehaviour
	{
		[SerializeField] private ScenesHandlerMediatorSO _scenesHandlerMediator;
		[Space]
		[SerializeField] private string _mainMenuSceneName;
		[SerializeField] private string _persistentSceneName;
		[SerializeField] private string _gameplaySceneName;

		private void OnEnable()
		{
			_scenesHandlerMediator.SceneSwitchRequested += HandleSwitchSceneRequested;
		}

		private void OnDisable()
		{
			_scenesHandlerMediator.SceneSwitchRequested -= HandleSwitchSceneRequested;
		}

		private void HandleSwitchSceneRequested(string sceneName)
		{
			StartCoroutine(SwitchScene(sceneName));
		}

		private IEnumerator SwitchScene(string sceneName)
		{
			Scene[] loadedScenes = new Scene[SceneManager.loadedSceneCount];

			for (int i = 0; i < loadedScenes.Length; i++)
			{
				loadedScenes[i] = SceneManager.GetSceneAt(i);
			}

			if (sceneName == _mainMenuSceneName)
			{
				for (int i = 0; i < loadedScenes.Length; i++)
				{
					if (loadedScenes[i].name != _persistentSceneName)
					{
						yield return UnloadScene(loadedScenes[i]);
					}
				}
			}
			else
			{
				for (int i = 0; i < loadedScenes.Length; i++)
				{
					if (loadedScenes[i].name != _persistentSceneName && loadedScenes[i].name != _gameplaySceneName)
					{
						yield return UnloadScene(loadedScenes[i]);
					}
				}

				if (SceneManager.GetSceneByName(_gameplaySceneName).isLoaded == false)
				{
					yield return LoadScene(_gameplaySceneName);
				}
			}

			yield return LoadScene(sceneName);

			SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
		}

		private IEnumerator UnloadScene(Scene scene)
		{
			var operation = SceneManager.UnloadSceneAsync(scene);

			while (operation.isDone == false)
			{
				yield return null;
			}
		}

		private IEnumerator LoadScene(string sceneName)
		{
			var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

			while (operation.isDone == false)
			{
				yield return null;
			}
		}
	}
}
