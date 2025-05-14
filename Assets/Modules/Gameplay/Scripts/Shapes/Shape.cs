using UnityEngine;

namespace Seven.Gameplay.Shapes
{
	internal sealed class Shape : MonoBehaviour
	{
		[SerializeField] private ShapeClickDetector _clickDetector;

		[field: SerializeField] public SpriteRenderer BackgroundSpriteRenderer { get; private set; }

		[field: SerializeField] public SpriteRenderer FrameSpriteRenderer { get; private set; }

		[field: SerializeField] public SpriteRenderer ForegroundSpriteRenderer { get; private set; }
	}
}
