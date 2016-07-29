using UnityEngine;

public class Bezier {
	public static Vector3 GetPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3){

        // B(t) = (1 - t)³ P0 + 3(1 - t)² t P1 + 3(1 - t) t² P2 + t³ P3
        t = Mathf.Clamp01 (t);
		float oneMinusT = 1f - t;

		return oneMinusT * oneMinusT * oneMinusT * p0 +
                3f * oneMinusT * oneMinusT * t * p1 +
                3f * oneMinusT * t * t * p2 +
                t * t * t * p3;
	}
	

	public static Vector3 GetDerivative(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) {
        // B'(t) = 3(1 - t)² (P1 - P0) + 6(1 - t)t (P2 - P1) + 3t² (P3 - P2)

        t = Mathf.Clamp01 (t);
		float oneMinusT = 1f - t;

		return 3f * oneMinusT * oneMinusT * (p1 - p0) +
                6f * oneMinusT * t * (p2 - p1) +
                3f * t * t * (p3 - p2);
	}
}
