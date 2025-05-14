using Seven.Gameplay.ScriptableObjectsDefinitions;
using UnityEngine;

namespace Seven.Gameplay
{
	internal sealed class LevelHandler : MonoBehaviour
	{
		[SerializeField] private LevelHandlerMediatorSO _levelHandlerMediator;
		[SerializeField] private LevelInitializer _levelInitializer;

		[field: SerializeField] public string NextLevelSceneName { get; private set; }

		private void OnEnable()
		{
			_levelHandlerMediator.RegisterLevelHandler(this);
			_levelInitializer.Initialize();
		}

		private void OnDisable()
		{
			_levelHandlerMediator.UnregisterLevelHandler();
		}
	}
}
