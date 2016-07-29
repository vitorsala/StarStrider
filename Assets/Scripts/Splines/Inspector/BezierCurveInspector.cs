using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor {
	
	private BezierCurve curve;
	private Transform handleTransform;
	private Quaternion handleRotation;

	private const int lineSteps = 10;

	private void OnSceneGUI(){
		
		curve = target as BezierCurve;
		handleTransform = curve.transform;
		handleRotation = (Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity);

		Handles.color = Color.gray;
		for (int i = 0; i < curve.points.Length - 1; i++) {
			Handles.DrawLine (ShowPoint(i), ShowPoint(i+1));
		}

		if (curve.points.Length == 3) {
			Handles.color = Color.white;
			Vector3 lineStart = curve.GetPoint (0);
			for (int i = 1; i <= lineSteps; i++) {
				Vector3 lineEnd = curve.GetPoint (i / (float)lineSteps);
				Handles.DrawLine (lineStart, lineEnd);
				lineStart = lineEnd;
			}
		}
		else if (curve.points.Length == 4) {
			Handles.DrawBezier (curve.points [0], curve.points [3], curve.points [1], curve.points [2], Color.white, null, 2f);
		}
        ShowDirections();

    }

	private void ShowDirections(){
        int stepsPerCurve = 10;
        Handles.color = Color.green;
        int steps = stepsPerCurve;

        for (int i = 0; i <= steps; i++) {
            float t = i / (float)steps;
            Vector3 p = curve.GetPoint(t);
            Handles.DrawLine(p, p + curve.GetVelocity(t).normalized);
            Handles.SphereCap(0, p, Quaternion.identity, 0.5f);
        }
    }

	private Vector3 ShowPoint(int index){
		
		Vector3 point = handleTransform.TransformPoint (curve.points [index]);
		EditorGUI.BeginChangeCheck ();
		point = Handles.DoPositionHandle (point, handleRotation);

		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject (curve, "Move Point");
			EditorUtility.SetDirty (curve);
			curve.points [index] = handleTransform.InverseTransformPoint (point);
		}
		return point;
	}
}
