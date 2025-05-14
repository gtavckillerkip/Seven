using Seven.Gameplay.PlayingFieldInteractions;
using Seven.Gameplay.Shapes;
using System;
using UnityEngine;

namespace Seven.Gameplay.ScriptableObjectsDefinitions
{
	[CreateAssetMenu(menuName = "Scriptable objects/Gameplay/ShapeClickerMediator")]
	internal sealed class ShapeClickerMediatorSO : ScriptableObject
	{
		[SerializeField] private ShapeClicker _shapeClicker;

		public event Action<Shape> ShapeClicked;

		public void RegisterShapeClicker(ShapeClicker shapeClicker)
		{
			if (shapeClicker == null)
			{
				return;
			}

			UnregisterSHapeClicker();

			_shapeClicker = shapeClicker;

			_shapeClicker.ShapeClicked += HandleShapeClicked;
		}

		public void UnregisterSHapeClicker()
		{
			if (_shapeClicker != null)
			{
				_shapeClicker.ShapeClicked -= HandleShapeClicked;
			}

			_shapeClicker = null;
		}

		private void HandleShapeClicked(Shape shape) => ShapeClicked?.Invoke(shape);
	}
}
