using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Seven.Gameplay.InputSystem
{
	internal sealed class GameplayInputController : MonoBehaviour
	{
		private GameplayInputActions _inputActions;

		public event Action<Vector3> Touched;

		private void Awake()
		{
			_inputActions = new();

			_inputActions.Base.Touch.performed += context =>
			{
				var touchWorldPoint = Camera.main.ScreenToWorldPoint(Touchscreen.current.position.ReadValue());
				Touched?.Invoke(touchWorldPoint);
			};
		}

		private void OnEnable()
		{
			_inputActions.Base.Enable();
		}

		private void OnDisable()
		{
			_inputActions.Base.Disable();
		}
	}
}
