using UnityEngine;

public class Bezier {
	public static Vector3 GetPoint(float t, params Vector3[] points){

		// B(t) = (1 - t)² P0 + 2 (1 - t) t P1 + t² P2
		t = Mathf.Clamp01 (t);
		float oneMinusT = 1f - t;
		Vector3 result = Vector3.zero;


		if (points.Length == 3) {
			result = oneMinusT * oneMinusT * points [0] +
				2f * oneMinusT * t * points [1] +
				t * t * points [2];
		}
		else if (points.Length == 4) {
			result = oneMinusT * oneMinusT * oneMinusT * points[0] +
				oneMinusT * oneMinusT * points [1] +
				2f * oneMinusT * t * points [2] +
				t * t * points [3];
		}

		return result;
	}
	

	public static Vector3 GetDerivative(float t, params Vector3[] points){
		// B'(t) = 2 (1 - t)(P1 - P0) + 2 t (P2 - P1)

		t = Mathf.Clamp01 (t);
		float oneMinusT = 1f - t;
		Vector3 result = Vector3.zero;

		if (points.Length == 3) {
			result = 2f * oneMinusT * (points [1] - points [0]) +
				2f * t * (points [2] - points [1]);
		}
		else if (points.Length == 4) {
			result = 3 * oneMinusT * oneMinusT * (points[1] - points[0]) +
				6f * oneMinusT * (points [2] - points [1]) +
				3f * t * t * (points [3] - points [2]);
		}

		return result;
	}
}
