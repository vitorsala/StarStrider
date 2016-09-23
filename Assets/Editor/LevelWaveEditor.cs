using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

[CustomEditor(typeof(LevelData))]
public class LevelWaveEditor : Editor {
	private ReorderableList list;
    private int selectedIndex = -1;

    private Transform handleTransform;
    private Quaternion handleRotation;

    private void OnSceneGUI() {
        if(selectedIndex >= 0 && selectedIndex < list.count) {
            LevelData ld = target as LevelData;

            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(selectedIndex);
            string selectedEnemyType = element.FindPropertyRelative("waveType").enumDisplayNames[element.FindPropertyRelative("waveType").enumValueIndex];
            if(selectedEnemyType == "Kamikaze") {

                handleRotation = (Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity);
                Vector3 point = handleTransform.TransformPoint(element.FindPropertyRelative("origin").vector3Value);

                EditorGUI.BeginChangeCheck();
                Vector3 editorPoint = Handles.DoPositionHandle(point, handleRotation);

                if(EditorGUI.EndChangeCheck()) {
                    element.FindPropertyRelative("origin").vector3Value = handleTransform.InverseTransformPoint(editorPoint);
                }
            }
        }
    }

	private void OnEnable(){
        handleTransform = (target as LevelData).transform;

        list = new ReorderableList (serializedObject, serializedObject.FindProperty ("waves"), true, true, true, true);

		list.elementHeight = EditorGUIUtility.singleLineHeight * 5 + 10;

		list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {

			SerializedProperty elem = list.serializedProperty.GetArrayElementAtIndex(index);

			float height = EditorGUIUtility.singleLineHeight;
			float yOffset = height + 2;

			EditorGUI.LabelField(new Rect(rect.x, rect.y, 30, height), index+"");
			EditorGUI.PropertyField(new Rect(rect.x, rect.y + height, 30, height), elem.FindPropertyRelative("active"), GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + 20, rect.y, 100, height), "Wave Type");
			EditorGUI.PropertyField(new Rect(rect.x + 20, rect.y + yOffset, 100, height), elem.FindPropertyRelative("waveType"), GUIContent.none);


            string selectedEnemyType = elem.FindPropertyRelative("waveType").enumDisplayNames[elem.FindPropertyRelative("waveType").enumValueIndex];
            if(selectedEnemyType == "Wasp" || selectedEnemyType == "Kamikaze") {

                EditorGUI.PropertyField(new Rect(rect.x, rect.y + 3 * height, 30, height), elem.FindPropertyRelative("ignoreDelay"), GUIContent.none);

                EditorGUI.LabelField(new Rect(rect.x + 125, rect.y, 50, height), "Quantity");
                EditorGUI.PropertyField(new Rect(rect.x + 125, rect.y + yOffset, 50, height), elem.FindPropertyRelative("numberOfEnemies"), GUIContent.none);

                EditorGUI.LabelField(new Rect(rect.x + 180, rect.y, 50, height), "Vel.");
                EditorGUI.PropertyField(new Rect(rect.x + 180, rect.y + yOffset, 50, height), elem.FindPropertyRelative("enemySpeed"), GUIContent.none);

                EditorGUI.LabelField(new Rect(rect.x + 235, rect.y, 100, height), "Spawn.Time (s)");
                EditorGUI.PropertyField(new Rect(rect.x + 235, rect.y + yOffset, 100, height), elem.FindPropertyRelative("timeToStartSpawner"), GUIContent.none);

                EditorGUI.LabelField(new Rect(rect.x + 340, rect.y, 100, height), "Time Between (s)");
                EditorGUI.PropertyField(new Rect(rect.x + 340, rect.y + yOffset, 100, height), elem.FindPropertyRelative("timeBetweenEntities"), GUIContent.none);


                EditorGUI.LabelField(new Rect(rect.x + 20, rect.y + 2 * yOffset, 100, height), "Shooting Pattern");
                EditorGUI.PropertyField(new Rect(rect.x + 125, rect.y + 2 * yOffset, 100, height), elem.FindPropertyRelative("shootType"), GUIContent.none);

                string selectedShootType = elem.FindPropertyRelative("shootType").enumDisplayNames[elem.FindPropertyRelative("shootType").enumValueIndex];

                if(selectedShootType == "Arc" || selectedShootType == "Line") {
                    EditorGUI.LabelField(new Rect(rect.x + 230, rect.y + 2 * yOffset, 70, height), "Num. Shoot");
                    EditorGUI.PropertyField(new Rect(rect.x + 305, rect.y + 2 * yOffset, 50, height), elem.FindPropertyRelative("numberOfShoot"), GUIContent.none);
                }

                EditorGUI.LabelField(new Rect(rect.x + 360, rect.y + 2 * yOffset, 70, height), "Shoot Speed");
                EditorGUI.PropertyField(new Rect(rect.x + 435, rect.y + 2 * yOffset, 50, height), elem.FindPropertyRelative("projectileSpeed"), GUIContent.none);

                if(selectedEnemyType == "Wasp") {
                    EditorGUI.LabelField(new Rect(rect.x + 20, rect.y + 3 * yOffset, 100, height), "Path Prefab");
                    EditorGUI.PropertyField(new Rect(rect.x + 125, rect.y + 3 * yOffset, rect.width - 125, height), elem.FindPropertyRelative("enemyPath"), GUIContent.none);
                }
                else if(selectedEnemyType == "Kamikaze") {
                    EditorGUI.LabelField(new Rect(rect.x + 20, rect.y + 3 * yOffset, 100, height), "Origin Point");
                    EditorGUI.PropertyField(new Rect(rect.x + 125, rect.y + 3 * yOffset, rect.width - 125, height), elem.FindPropertyRelative("origin"), GUIContent.none);
                }

                EditorGUI.LabelField(new Rect(rect.x + 20, rect.y + 4 * yOffset, 100, height), "Enemy Prefab");
                EditorGUI.PropertyField(new Rect(rect.x + 125, rect.y + 4 * yOffset, rect.width - 125, height), elem.FindPropertyRelative("enemyToSpawn"), GUIContent.none);
            }


            
		};

		list.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, rect.height), "Wave Editor do Kawai");
		};

		list.onRemoveCallback = (ReorderableList l) => {
			if(EditorUtility.DisplayDialog("PERIGO!!!","Você realmente irá deletar a wave?  :(","YIS","NUU")){
				ReorderableList.defaultBehaviours.DoRemoveButton(l);
			}
		};

        list.onSelectCallback = (ReorderableList l) => {
            selectedIndex = l.index;
        };

		list.onAddCallback = (ReorderableList l) => {
			int index = l.serializedProperty.arraySize;
			l.serializedProperty.arraySize++;
			l.index = index;
			SerializedProperty element = l.serializedProperty.GetArrayElementAtIndex(index);
            element.FindPropertyRelative("origin").vector3Value = new Vector3(0.1f,0.1f);
			element.FindPropertyRelative("active").boolValue = true;
		};

	}

	public override void OnInspectorGUI(){
		serializedObject.Update ();
		list.DoLayoutList ();
		serializedObject.ApplyModifiedProperties ();
	}

}

