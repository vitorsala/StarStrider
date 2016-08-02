using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Path))]
public class PathInspector : Editor {

	private static Color[] modeColors = {
		Color.white,
		Color.yellow,
		Color.cyan
	};

	private const float handleSize = 0.04f;
	private const float pickSize = 0.06f;
	private int selectedIndex = -1;

	private Path path;
	private Transform handleTransform;
	private Quaternion handleRotation;

	private void OnSceneGUI(){
		
		path = target as Path;
		handleTransform = path.transform;
		handleRotation = (Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity);

		Vector3 p0 = ShowPoint (0);
		for (int i = 1; i < path.ControlPointCount; i += 3) {
			if (path.GetSegmentType(i) == SegmentType.Curve) {
				Vector3 p1 = ShowPoint(i);
				Vector3 p2 = ShowPoint(i + 1);
				Vector3 p3 = ShowPoint(i + 2);

				Handles.color = Color.gray;
				Handles.DrawLine(p0, p1);
				Handles.DrawLine(p2, p3);

				Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
				p0 = p3;
			}
			else {
				Vector3 p3 = ShowPoint(i+2);

				Handles.color = Color.cyan;
				Handles.DrawLine(p0, p3);
				p0 = p3;
			}
		}
        //DrawDirection();
	}

    public override void OnInspectorGUI() {

		path = target as Path;


        if (selectedIndex >= 0 && selectedIndex < path.ControlPointCount) {

            EditorGUI.BeginChangeCheck();
            bool loop = EditorGUILayout.Toggle("Loop", path.loop);
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(path, "Toggle Loop");
                EditorUtility.SetDirty(path);
                path.loop = loop;
            }
            
            GUILayout.Label("Selected Group:");

			EditorGUI.BeginChangeCheck();
			SegmentType type = (SegmentType)EditorGUILayout.EnumPopup("Type", path.GetSegmentType(selectedIndex));

			if (EditorGUI.EndChangeCheck()) {
				Undo.RecordObject(path, "Change Segment Type");
				EditorUtility.SetDirty(path);
				path.SetSegmentType(selectedIndex, type);
			}

            EditorGUI.BeginChangeCheck();
            BezierControlPointMode mode = (BezierControlPointMode)EditorGUILayout.EnumPopup("Mode", path.GetControlPointMode(selectedIndex));

            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(path, "Change Point Mode");
                EditorUtility.SetDirty(path);
                path.SetControlPointMode(selectedIndex, mode);
            }

            int inspectedIndex = selectedIndex;
            if (selectedIndex % 3 == 0) {
                inspectedIndex -= 1;
            }
            else if (selectedIndex % 3 == 1) {
                inspectedIndex -= 2;
            }

            if (path.loop || inspectedIndex >= 0) {
                int i = (inspectedIndex >= 0 ? inspectedIndex : path.ControlPointCount - 2);
                EditorGUI.BeginChangeCheck();
                Vector3 p1 = EditorGUILayout.Vector3Field("Position P1", path.GetControlPoint(i));
                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(path, "Move Point");
                    EditorUtility.SetDirty(path);
                    path.SetControlPoint(i, p1);
                }
            }

            EditorGUI.BeginChangeCheck();
            Vector3 pivot = EditorGUILayout.Vector3Field("Position Pivot", path.GetControlPoint(inspectedIndex + 1));
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(path, "Move Point");
                EditorUtility.SetDirty(path);
                path.SetControlPoint(inspectedIndex + 1, pivot);
            }

            if (path.loop || inspectedIndex + 2 < path.ControlPointCount) {
                int i = (inspectedIndex + 2 < path.ControlPointCount ? inspectedIndex + 2 : 1);
                EditorGUI.BeginChangeCheck();
                Vector3 p2 = EditorGUILayout.Vector3Field("Position P2", path.GetControlPoint(i));
                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(path, "Move Point");
                    EditorUtility.SetDirty(path);
                    path.SetControlPoint(i, p2);
                }
            }
        }

        if (GUILayout.Button("Add Segment")) {
            Undo.RecordObject(path, "Add Segment");
            EditorUtility.SetDirty(path);
            path.AddSegment();
        }
        if (GUILayout.Button("Remove Segment")) {
            Undo.RecordObject(path, "Remove Segment");
            EditorUtility.SetDirty(path);
            path.RemoveSegment();
        }
    }

    private Vector3 ShowPoint(int index){
		Vector3 point = handleTransform.TransformPoint (path.GetControlPoint(index));
		Handles.color = Color.white;
		float size = HandleUtility.GetHandleSize (point);
        Handles.color = modeColors [(int)path.GetControlPointMode (index)];
        
        if(index % 3 == 0) {
            if (index == 0 || index == path.ControlPointCount - 1) {
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
				Undo.RecordObject (path, "Move Point");
				EditorUtility.SetDirty (path);
				path.SetControlPoint(index, handleTransform.InverseTransformPoint (point));
			}
		}

		return point;
	}

    private void DrawDirection() {
        int stepsPerCurve = 10;
        Handles.color = Color.green;
        int steps = stepsPerCurve * path.SegmentCount;

        for (int i = 0; i <= steps; i++) {
            float t = i / (float)steps;
            Vector3 p = path.GetPoint(t);
            Handles.DrawLine(p, p + path.GetDirection(t));
        }
    }
}
