using Seven.Gameplay.Shapes;
using System;
using UnityEngine;

namespace Seven.Gameplay.ScriptableObjectsDefinitions
{
	[CreateAssetMenu(menuName = "Scriptable objects/Gameplay/ActionBarAndGameLogicMediator")]
	internal sealed class ActionBarAndGameLogicMediatorSO : ScriptableObject
	{
		private ActionBar _actionBar;
		private GameLogicHandler _gameLogicHandler;

		public event Action<Shape> ShapeAdded;

		public event Action<Shape, Shape, Shape> FoundThree;

		public event Action ActionBarRegistered;

		public ActionBar ActionBar => _actionBar;

		public void RegisterActionBar(ActionBar actionBar)
		{
			if (actionBar == null)
			{
				return;
			}

			UnregisterActionBar();

			_actionBar = actionBar;

			ActionBarRegistered?.Invoke();

			_actionBar.ShapeAdded += HandleShapeAdded;
		}

		public void UnregisterActionBar()
		{
			if (_actionBar != null)
			{
				_actionBar.ShapeAdded -= HandleShapeAdded;
			}

			_actionBar = null;
		}

		public void RegisterGameLogicHandler(GameLogicHandler gameLogicHandler)
		{
			if (gameLogicHandler == null)
			{
				return;
			}

			UnregisterGameLogicHandler();

			_gameLogicHandler = gameLogicHandler;

			_gameLogicHandler.FoundThree += HandleFoundThree;
		}

		public void UnregisterGameLogicHandler()
		{
			if (_gameLogicHandler != null)
			{
				_gameLogicHandler.FoundThree -= HandleFoundThree;
			}

			_gameLogicHandler = null;
		}

		private void HandleShapeAdded(Shape shape) => ShapeAdded?.Invoke(shape);

		private void HandleFoundThree(Shape s1, Shape s2, Shape s3) => FoundThree?.Invoke(s1, s2, s3);
	}
}
