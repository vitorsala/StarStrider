using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Line))]
public class LineInspector : Editor {

	private Line line;
	private Transform handleTransform;
	private Quaternion handleRotation;

	private int selectedIndex = -1;
	private const float handleSize = 0.04f;
	private const float pickSize = 0.06f;

	private void OnSceneGUI(){
		
		line = target as Line;
		handleTransform = line.transform;
		handleRotation = (Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity);

		Vector3 p0 = handleTransform.TransformPoint (line.p0);
		Vector3 p1 = handleTransform.TransformPoint (line.p1);

		Handles.color = Color.white;
		Handles.DrawLine (p0, p1);
		Handles.DoPositionHandle (p0, handleRotation);
		Handles.DoPositionHandle (p1, handleRotation);


		EditorGUI.BeginChangeCheck ();
		p0 = Handles.DoPositionHandle (p0, handleRotation);
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject (line, "Move Point");
			EditorUtility.SetDirty (line);
			line.p0 = handleTransform.InverseTransformPoint (p0);
		}
		EditorGUI.BeginChangeCheck ();
		p1 = Handles.DoPositionHandle (p1, handleRotation);
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject (line, "Move Point");
			EditorUtility.SetDirty (line);
			line.p1 = handleTransform.InverseTransformPoint (p1);
		}
	}

	private Vector3 ShowPoint(int index){
		Vector3 point = Vector3.zero;
		if (index == 0 || index == 1) {
			point = handleTransform.TransformPoint ((index == 0 ? line.p0 : line.p1));
			Handles.color = Color.white;
			float size = HandleUtility.GetHandleSize (point);
			Handles.color = Color.gray;
			if(Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.DotCap)){
				selectedIndex = index;
				Repaint ();
			}
			if (selectedIndex == index) {
				EditorGUI.BeginChangeCheck ();
				point = Handles.DoPositionHandle (point, handleRotation);

				if (EditorGUI.EndChangeCheck ()) {
					Undo.RecordObject (line, "Move Point");
					EditorUtility.SetDirty (line);
					if (index == 0) {
						line.p0 = handleTransform.InverseTransformPoint (point);
					}
					else {
						line.p1 = handleTransform.InverseTransformPoint (point);
					}
				}
			}
					
		}
		return point;

	}

}
