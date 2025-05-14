using Seven.Gameplay.ScriptableObjectsDefinitions;
using Seven.Gameplay.Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Seven.Gameplay
{
	internal sealed class GameLogicHandler : MonoBehaviour
	{
		[SerializeField] private SpawnerMediatorSO _spawnerMediator;
		[SerializeField] private ActionBarAndGameLogicMediatorSO _actionBarAndLogicMediator;

		private IEnumerable<Shape> _shapes;
		private int _barCapacity;

		public event Action<Shape, Shape, Shape> FoundThree;
		public event Action GameLost;
		public event Action GameWon;

		public int CurrentShapesInBoxAmount { get; private set; }

		private void Awake()
		{
			_actionBarAndLogicMediator.ActionBarRegistered += HandleActionBarRegistered;
		}

		private void OnEnable()
		{
			_actionBarAndLogicMediator.RegisterGameLogicHandler(this);

			_actionBarAndLogicMediator.ShapeAdded += HandleShapeAddedToActionBar;
			_spawnerMediator.ShapeSpawned += HandleShapeSpawned;
			_spawnerMediator.AllShapesRemovedFromBox += HandleAllShapesRemovedFromBox;
		}

		private void OnDisable()
		{
			_actionBarAndLogicMediator.UnregisterActionBar();

			_actionBarAndLogicMediator.ShapeAdded -= HandleShapeAddedToActionBar;
			_spawnerMediator.ShapeSpawned -= HandleShapeSpawned;
			_spawnerMediator.AllShapesRemovedFromBox -= HandleAllShapesRemovedFromBox;
		}

		private void OnDestroy()
		{
			_actionBarAndLogicMediator.ActionBarRegistered -= HandleActionBarRegistered;
		}

		private void HandleShapeAddedToActionBar(Shape shape)
		{
			var shapes = _shapes.Where(s =>
				s.FrameSpriteRenderer.sprite == shape.FrameSpriteRenderer.sprite &&
				s.FrameSpriteRenderer.color == shape.FrameSpriteRenderer.color &&
				s.ForegroundSpriteRenderer.sprite == shape.ForegroundSpriteRenderer.sprite
				);

			CurrentShapesInBoxAmount--;

			if (shapes != null && shapes.Count() == 3)
			{
				FoundThree?.Invoke(shapes.ElementAt(0), shapes.ElementAt(1), shape);
			}

			StartCoroutine(CheckForWinLose());
		}

		private IEnumerator CheckForWinLose()
		{
			yield return new WaitForEndOfFrame();

			int count = _shapes.Count();
			if (count == _barCapacity || count != _barCapacity && count != 0 && CurrentShapesInBoxAmount == 0)
			{
				GameLost?.Invoke();
				yield break;
			}
			
			if (CurrentShapesInBoxAmount == 0 && count == 0)
			{
				GameWon?.Invoke();
			}
		}

		private void HandleShapeSpawned(Shape shape)
		{
			CurrentShapesInBoxAmount++;
		}

		private void HandleAllShapesRemovedFromBox()
		{
			CurrentShapesInBoxAmount = 0;
		}

		private void HandleActionBarRegistered()
		{
			_shapes = _actionBarAndLogicMediator.ActionBar.Shapes;
			_barCapacity = _actionBarAndLogicMediator.ActionBar.BarCapacity;
		}
	}
}
