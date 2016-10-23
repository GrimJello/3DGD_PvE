using UnityEngine;
using System.Collections;

public class ClampAngle : MonoBehaviour {

	public static float ClampAngleRound(
		float currentValue,
		float minAngle,
		float maxAngle,
		float clampAroundAngle = 0
	) {
		float angle = currentValue - (clampAroundAngle + 180);

		while (angle < 0) {
			angle += 360;
		}

		angle = Mathf.Repeat(angle, 360);

		return Mathf.Clamp(
			angle - 180,
			minAngle,
			maxAngle
		) + 360 + clampAroundAngle;
	}
}