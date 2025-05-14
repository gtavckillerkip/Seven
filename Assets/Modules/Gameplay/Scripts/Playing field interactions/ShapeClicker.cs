using Seven.Gameplay.InputSystem;
using Seven.Gameplay.ScriptableObjectsDefinitions;
using Seven.Gameplay.Shapes;
using System;
using UnityEngine;

namespace Seven.Gameplay.PlayingFieldInteractions
{
	internal sealed class ShapeClicker : MonoBehaviour
	{
		[SerializeField] private GameplayInputController _inputController;
		[SerializeField] private ShapeClickerMediatorSO _shapeClickerMediator;

		public event Action<Shape> ShapeClicked;

		private void OnEnable()
		{
			_shapeClickerMediator.RegisterShapeClicker(this);
			_inputController.Touched += HandleTouchedScreen;
		}

		private void OnDisable()
		{
			_shapeClickerMediator.UnregisterSHapeClicker();
			_inputController.Touched -= HandleTouchedScreen;
		}

		private void HandleTouchedScreen(Vector3 touchWorldPoint)
		{
			var hit = Physics2D.Raycast(touchWorldPoint, Vector2.zero);

			if (hit.collider != null && hit.collider.GetComponent<ShapeClickDetector>() is ShapeClickDetector shapeClickDetector)
			{
				var shape = shapeClickDetector.Click();

				ShapeClicked?.Invoke(shape);
			}
		}
	}
}
