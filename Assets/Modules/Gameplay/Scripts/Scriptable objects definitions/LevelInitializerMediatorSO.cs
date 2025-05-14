using System;
using UnityEngine;

namespace Seven.Gameplay.ScriptableObjectsDefinitions
{
	[CreateAssetMenu(menuName = "Scriptable objects/Gameplay/LevelInitializerMediator")]
	internal sealed class LevelInitializerMediatorSO : ScriptableObject
	{
		private LevelInitializer _levelInitializer;

		public event Action LevelInitStarted;
		public event Action LevelInitFinished;

		public LevelInitializer LevelInitializer => _levelInitializer;

		public void RegisterLevelInitializer(LevelInitializer levelInitializer)
		{
			UnregisterLevelInitializer();

			_levelInitializer = levelInitializer;

			_levelInitializer.InitStarted += HandleLevelInitStarted;
			_levelInitializer.InitFinished += HandleLevelInitFinished;
		}

		public void UnregisterLevelInitializer()
		{
			if (_levelInitializer != null)
			{
				_levelInitializer.InitStarted -= LevelInitStarted;
				_levelInitializer.InitFinished -= LevelInitFinished;
			}

			_levelInitializer = null;
		}

		private void HandleLevelInitStarted() => LevelInitStarted?.Invoke();

		private void HandleLevelInitFinished() => LevelInitFinished?.Invoke();
	}
}
