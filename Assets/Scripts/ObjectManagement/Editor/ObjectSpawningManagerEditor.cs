using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Collections;
using Malee.Editor;
using System;

[CanEditMultipleObjects]
[CustomEditor(typeof(ObjectSpawningManager))]
public class ObjectSpawningManagerEditor : Editor
{
    private ReorderableList list1;

	void OnEnable() {
		list1 = new ReorderableList(serializedObject.FindProperty("objectsToSpawn"));
		list1.elementNameProperty = "spawnObject";
	}

	public override void OnInspectorGUI() {

		serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        serializedObject.FindProperty("startTimeOnGameStart").boolValue = EditorGUILayout.Toggle("Start on Game Start", serializedObject.FindProperty("startTimeOnGameStart").boolValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        serializedObject.FindProperty("spawnParent").objectReferenceValue = EditorGUILayout.ObjectField("Spawn Parent", serializedObject.FindProperty("spawnParent").objectReferenceValue, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        //draw the list using GUILayout, you can of course specify your own position and label
        list1.DoLayoutList();



		serializedObject.ApplyModifiedProperties();
	}
}
