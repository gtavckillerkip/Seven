using UnityEngine;

namespace Seven.Gameplay.ScriptableObjectsDefinitions
{
	[CreateAssetMenu(menuName = "Scriptable objects/Gameplay/LevelHandlerMediator")]
	internal sealed class LevelHandlerMediatorSO : ScriptableObject
	{
		public LevelHandler LevelHandler { get; private set; }

		public void RegisterLevelHandler(LevelHandler levelHandler)
		{
			LevelHandler = levelHandler;
		}

		public void UnregisterLevelHandler()
		{
			LevelHandler = null;
		}
	}
}
