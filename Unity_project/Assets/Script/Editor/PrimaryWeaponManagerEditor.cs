using UnityEngine;
using UnityEditor;
using System.Collections;

//Custom Editor for setting primary waepon manager variables
//[CustomEditor(typeof(PrimaryWeaponManager))]
[CanEditMultipleObjects]
public class PrimaryWeaponManagerEditor : Editor {

    SerializedProperty _bulletPrefabProp;
    SerializedProperty _shotPointProp;

    SerializedProperty _bulletsAmountProp ;
    SerializedProperty _shotDelayProp ;
    SerializedProperty _spreadAmountProp;
    SerializedProperty _offsetAmountProp;
    SerializedProperty _accuracyProp;

	// Use this for initialization
	void OnEnable ()
    {
        _bulletPrefabProp = serializedObject.FindProperty("_bulletPrefab");
        _shotPointProp = serializedObject.FindProperty("_shotPoint");

        _bulletsAmountProp = serializedObject.FindProperty("_bulletsAmount");
        _shotDelayProp = serializedObject.FindProperty("_shotDelay");
        _spreadAmountProp = serializedObject.FindProperty("_spreadAmount");
        _offsetAmountProp = serializedObject.FindProperty("_offsetAmount");
        _accuracyProp = serializedObject.FindProperty("_accuracy");
	}

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //EditorGUILayout.IntField(_bulletsAmountProp, new GUIContent("Number of Bullets"));

        EditorGUILayout.IntSlider(_shotDelayProp, 0, 1000, new GUIContent("Shot Delay"));
        EditorGUILayout.IntSlider(_spreadAmountProp, 0, 1000, new GUIContent("Spread"));
        EditorGUILayout.IntSlider(_offsetAmountProp, 0, 1000, new GUIContent("Offset"));
        EditorGUILayout.IntSlider(_accuracyProp, 0, 1000, new GUIContent("Accuracy"));
        EditorGUILayout.IntSlider(_offsetAmountProp, 0, 1000, new GUIContent("Critical Hit Chance"));
        EditorGUILayout.IntSlider(_accuracyProp, 0, 1000, new GUIContent("Critical Hit Damage"));

        EditorGUILayout.PropertyField(_bulletPrefabProp, new GUIContent("Bullet Prefab (Regular)"));
        EditorGUILayout.PropertyField(_shotPointProp, new GUIContent("Bullet Spawn Transform"));

        serializedObject.ApplyModifiedProperties();
        //base.OnInspectorGUI();
    }
}
