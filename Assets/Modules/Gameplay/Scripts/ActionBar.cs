using Seven.Gameplay.ScriptableObjectsDefinitions;
using Seven.Gameplay.Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seven.Gameplay
{
	internal sealed class ActionBar : MonoBehaviour
	{
		[SerializeField] private Transform[] _slots;
		[Space]
		[SerializeField] private ActionBarAndGameLogicMediatorSO _actionBarAndGameLogicMediator;
		[SerializeField] private ShapeClickerMediatorSO _shapeClickerMediator;

		public event Action<Shape> ShapeAdded;

		public IEnumerable<Shape> Shapes { get; private set; } = new List<Shape>();

		public int BarCapacity { get; private set; }

		private void OnEnable()
		{
			for (int i = 0; i < _slots.Length; i++)
			{
				var child = _slots[i];

				if (child != null && child.gameObject.GetComponent<Shape>() is Shape shape)
				{
					(Shapes as List<Shape>).Add(shape);
				}
			}

			BarCapacity = _slots.Length;

			_actionBarAndGameLogicMediator.RegisterActionBar(this);
			_shapeClickerMediator.ShapeClicked += HandleShapeClicked;
			_actionBarAndGameLogicMediator.FoundThree += HandleFoundThree;
		}

		private void OnDisable()
		{
			if (Shapes != null)
			{
				foreach (var shape in Shapes)
				{
					Destroy(shape.gameObject);
				}

				(Shapes as List<Shape>).Clear();
			}

			_actionBarAndGameLogicMediator.UnregisterActionBar();
			_shapeClickerMediator.ShapeClicked -= HandleShapeClicked;
			_actionBarAndGameLogicMediator.FoundThree -= HandleFoundThree;
		}

		private void HandleShapeClicked(Shape shape)
		{
			for (int i = 0; i < _slots.Length; i++)
			{
				if (_slots[i].childCount == 0)
				{
					shape.gameObject.GetComponent<Rigidbody2D>().simulated = false;

					shape.gameObject.transform.parent = _slots[i];
					shape.gameObject.transform.localPosition = Vector3.zero;
					shape.gameObject.transform.localRotation = Quaternion.identity;

					(Shapes as List<Shape>).Add(shape);

					ShapeAdded?.Invoke(shape);

					return;
				}
			}
		}

		private void HandleFoundThree(Shape s1, Shape s2, Shape s3)
		{
			for (int i = 0; i < _slots.Length; i++)
			{
				if (_slots[i].childCount == 0)
				{
					continue;
				}

				var child = _slots[i].GetChild(0);

				if (child != null && child.GetComponent<Shape>() is Shape shape && (shape == s1 || shape == s2 || shape == s3))
				{
					Destroy(shape.gameObject);
					(Shapes as List<Shape>).Remove(shape);
				}
			}

			StartCoroutine(TidyUpAfterFoundThree());
		}

		private IEnumerator TidyUpAfterFoundThree()
		{
			yield return new WaitForEndOfFrame();

			for (int i = 0; i < _slots.Length - 1; i++)
			{
				if (_slots[i].childCount == 0)
				{
					Transform tr = null;

					for (int j = i + 1; j < _slots.Length; j++)
					{
						if (_slots[j].childCount != 0)
						{
							tr = _slots[j].GetChild(0);
							break;
						}
					}

					if (tr == null)
					{
						break;
					}

					tr.parent = _slots[i];
					tr.localPosition = Vector3.zero;
				}
			}
		}
	}
}
