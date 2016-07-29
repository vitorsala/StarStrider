using UnityEngine;
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
        //DrawDirection();
	}

    public override void OnInspectorGUI() {

        spline = target as BezierSpline;


        if (selectedIndex >= 0 && selectedIndex < spline.ControlPointCount) {

            EditorGUI.BeginChangeCheck();
            bool loop = EditorGUILayout.Toggle("Loop", spline.loop);
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(spline, "Toggle Loop");
                EditorUtility.SetDirty(spline);
                spline.loop = loop;
            }
            
            GUILayout.Label("Selected Group:");
            EditorGUI.BeginChangeCheck();
            BezierControlPointMode mode = (BezierControlPointMode)EditorGUILayout.EnumPopup("Mode", spline.GetControlPointMode(selectedIndex));

            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(spline, "Change Point Mode");
                EditorUtility.SetDirty(spline);
                spline.SetControlPointMode(selectedIndex, mode);
            }

            int inspectedIndex = selectedIndex;
            if (selectedIndex % 3 == 0) {
                inspectedIndex -= 1;
            }
            else if (selectedIndex % 3 == 1) {
                inspectedIndex -= 2;
            }

            if (spline.loop || inspectedIndex >= 0) {
                int i = (inspectedIndex >= 0 ? inspectedIndex : spline.ControlPointCount - 2);
                EditorGUI.BeginChangeCheck();
                Vector3 p1 = EditorGUILayout.Vector3Field("Position P1", spline.GetControlPoint(i));
                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(spline, "Move Point");
                    EditorUtility.SetDirty(spline);
                    spline.SetControlPoint(i, p1);
                }
            }

            EditorGUI.BeginChangeCheck();
            Vector3 pivot = EditorGUILayout.Vector3Field("Position Pivot", spline.GetControlPoint(inspectedIndex + 1));
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(spline, "Move Point");
                EditorUtility.SetDirty(spline);
                spline.SetControlPoint(inspectedIndex + 1, pivot);
            }

            if (spline.loop || inspectedIndex + 2 < spline.ControlPointCount) {
                int i = (inspectedIndex + 2 < spline.ControlPointCount ? inspectedIndex + 2 : 1);
                EditorGUI.BeginChangeCheck();
                Vector3 p2 = EditorGUILayout.Vector3Field("Position P2", spline.GetControlPoint(i));
                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(spline, "Move Point");
                    EditorUtility.SetDirty(spline);
                    spline.SetControlPoint(i, p2);
                }
            }
        }

        if (GUILayout.Button("Add Curve")) {
            Undo.RecordObject(spline, "Add Curve");
            EditorUtility.SetDirty(spline);
            spline.AddCurve();
        }
        if (GUILayout.Button("Remove Curve")) {
            Undo.RecordObject(spline, "Remove Curve");
            EditorUtility.SetDirty(spline);
            spline.RemoveCurve();
        }
    }

    private Vector3 ShowPoint(int index){
		Vector3 point = handleTransform.TransformPoint (spline.GetControlPoint(index));
		Handles.color = Color.white;
		float size = HandleUtility.GetHandleSize (point);
        Handles.color = modeColors [(int)spline.GetControlPointMode (index)];
        
        if(index % 3 == 0) {
            if (index == 0 || index == spline.ControlPointCount - 1) {
                size *= 3f;
            }
            else {
                size *= 2f;
            }
            
            if (Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.SphereCap)) {
                selectedIndex = index;
                Repaint();
            }
        }
        else {
            if (Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.DotCap)) {
                selectedIndex = index;
                Repaint();
            }
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

    private void DrawDirection() {
        int stepsPerCurve = 10;
        Handles.color = Color.green;
        int steps = stepsPerCurve * spline.CurveCount;

        for (int i = 0; i <= steps; i++) {
            float t = i / (float)steps;
            Vector3 p = spline.GetPoint(t);
            Handles.DrawLine(p, p + spline.GetDirection(t));
        }
    }
}
