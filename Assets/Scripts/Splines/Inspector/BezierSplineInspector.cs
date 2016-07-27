﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierSpline))]
public class BezierSplineInspector : Editor {

	private static Color[] modeColors = {
		Color.white,
		Color.yellow,
		Color.cyan
	};

	private const float handleSize = 0.04f;
	private const float pickSize = 0.06f;
	private int selectedIndex = -1;

	private BezierSpline spline;
	private Transform handleTransform;
	private Quaternion handleRotation;

	private void OnSceneGUI(){
		
		spline = target as BezierSpline;
		handleTransform = spline.transform;
		handleRotation = (Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity);

		Handles.color = Color.gray;
		for (int i = 0; i < spline.ControlPointCount - 1; i++) {
			Handles.DrawLine (ShowPoint(i), ShowPoint(i+1));
		}

		Vector3 p0 = ShowPoint (0);
		for (int i = 1; i < spline.ControlPointCount; i += 3) {
			Vector3 p1 = ShowPoint (i);
			Vector3 p2 = ShowPoint (i + 1);
			Vector3 p3 = ShowPoint (i + 2);

			Handles.color = Color.gray;
			Handles.DrawLine (p0, p1);
			Handles.DrawLine (p2, p3);

			Handles.DrawBezier (p0, p3, p1, p2, Color.white, null, 2f);
			p0 = p3;
		}
	}

	private Vector3 ShowPoint(int index){
		Vector3 point = handleTransform.TransformPoint (spline.GetControlPoint(index));
		Handles.color = Color.white;
		float size = HandleUtility.GetHandleSize (point);
		Handles.color = modeColors [(int)spline.GetControlPointMode (index)];

		if(Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.DotCap)){
			selectedIndex = index;
			Repaint ();
		}
		if (selectedIndex == index) {
			EditorGUI.BeginChangeCheck ();
			point = Handles.DoPositionHandle (point, handleRotation);

			if (EditorGUI.EndChangeCheck ()) {
				Undo.RecordObject (spline, "Move Point");
				EditorUtility.SetDirty (spline);
				spline.SetControlPoint(index, handleTransform.InverseTransformPoint (point));
			}
		}
		return point;
	}

	public override void OnInspectorGUI(){
		spline = target as BezierSpline;
		if (selectedIndex >= 0 && selectedIndex < spline.ControlPointCount) {
			GUILayout.Label ("Selected Point");
			EditorGUI.BeginChangeCheck ();
			Vector3 point = EditorGUILayout.Vector3Field("Position", spline.GetControlPoint(selectedIndex));
			if (EditorGUI.EndChangeCheck ()) {
				Undo.RecordObject (spline, "Move Point");
				spline.SetControlPoint (selectedIndex, point);
			}

			EditorGUI.BeginChangeCheck ();
			BezierSpline.BezierControlPointMode mode = (BezierSpline.BezierControlPointMode)EditorGUILayout.EnumPopup ("Mode", spline.GetControlPointMode (selectedIndex));
			if (EditorGUI.EndChangeCheck ()) {
				Undo.RecordObject (spline, "Change Point Mode");
				spline.SetControlPointMode (selectedIndex, mode);
				EditorUtility.SetDirty (spline);
			}
		}
		if (GUILayout.Button ("Add Curve")) {
			Undo.RecordObject (spline, "Add Curve");
			spline.AddCurve ();
			EditorUtility.SetDirty (spline);
		}
		if (GUILayout.Button ("Remove Curve")) {
			Undo.RecordObject (spline, "Remove Curve");
			spline.RemoveCurve ();
			EditorUtility.SetDirty (spline);
		}
	}
}
