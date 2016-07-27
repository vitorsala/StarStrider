using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

[CustomEditor(typeof(LevelData))]
public class LevelWaveEditor : Editor {
	private ReorderableList list;

	private void OnEnable(){
		list = new ReorderableList (serializedObject, serializedObject.FindProperty ("waves"), true, true, true, true);

		list.elementHeight = EditorGUIUtility.singleLineHeight * 3 + 10;

		list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
			SerializedProperty elem = list.serializedProperty.GetArrayElementAtIndex(index);

			float height = EditorGUIUtility.singleLineHeight;
			float yoffset = height + 2;

			EditorGUI.LabelField(new Rect(rect.x, rect.y, 15, height), index+"");
			EditorGUI.PropertyField(new Rect(rect.x, rect.y + height, 15, height), elem.FindPropertyRelative("active"), GUIContent.none);

			EditorGUI.LabelField(new Rect(rect.x + 20, rect.y, 100, height), "Wave Type");
			EditorGUI.PropertyField(new Rect(rect.x + 20, rect.y + yoffset, 100, height), elem.FindPropertyRelative("type"), GUIContent.none);

			EditorGUI.LabelField(new Rect(rect.x + 125, rect.y, 50, height), "Count");
			EditorGUI.PropertyField(new Rect(rect.x + 125, rect.y + yoffset, 50, height), elem.FindPropertyRelative("count"), GUIContent.none);

			EditorGUI.LabelField(new Rect(rect.x + 180, rect.y, 50, height), "S.Time");
			EditorGUI.PropertyField(new Rect(rect.x + 180, rect.y + yoffset, 50, height), elem.FindPropertyRelative("count"), GUIContent.none);

			EditorGUI.LabelField(new Rect(rect.x + 235, rect.y, rect.width - 235, height), "Shooting Pattern");
			EditorGUI.PropertyField(new Rect(rect.x + 235, rect.y + yoffset, rect.width - 235, height), elem.FindPropertyRelative("prefab"), GUIContent.none);

			EditorGUI.PropertyField(new Rect(rect.x + 20, rect.y + 2 * yoffset, rect.width - 20, height), elem.FindPropertyRelative("prefab"), GUIContent.none);
		};

		list.drawElementBackgroundCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
		};

		list.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, rect.height), "Wave Editor do Kawai");
		};

		list.onRemoveCallback = (ReorderableList l) => {
			if(EditorUtility.DisplayDialog("PERIGO!!!","Você realmente irá deletar a wave?  :(","YIS","NUU")){
				ReorderableList.defaultBehaviours.DoRemoveButton(l);
			}
		};

		list.onAddCallback = (ReorderableList l) => {
			int index = l.serializedProperty.arraySize;
			l.serializedProperty.arraySize++;
			l.index = index;
			SerializedProperty element = l.serializedProperty.GetArrayElementAtIndex(index);
			element.FindPropertyRelative("active").boolValue = true;
		};

	}

	public override void OnInspectorGUI(){
		serializedObject.Update ();
		list.DoLayoutList ();
		serializedObject.ApplyModifiedProperties ();
	}

}

