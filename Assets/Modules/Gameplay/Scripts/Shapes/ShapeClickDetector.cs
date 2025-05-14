using UnityEngine;

namespace Seven.Gameplay.Shapes
{
	internal sealed class ShapeClickDetector : MonoBehaviour
	{
		public Shape Click()
		{
			return transform.parent.GetComponent<Shape>();
		}
	}
}
