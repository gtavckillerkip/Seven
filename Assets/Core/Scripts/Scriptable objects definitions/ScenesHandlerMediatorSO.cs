using System;
using UnityEngine;

namespace Seven.Core.ScriptableObjectsDefinitions
{
	[CreateAssetMenu(menuName = "Scriptable objects/Core/ScenesHandlerMediator")]
	public sealed class ScenesHandlerMediatorSO : ScriptableObject
	{
		internal event Action<string> SceneSwitchRequested;

		public void SwitchScene(string sceneName)
		{
			SceneSwitchRequested?.Invoke(sceneName);
		}
	}
}
