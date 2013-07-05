using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Threshold))]
public class CC_ThresholdEditor : Editor
{
	SerializedObject srcObj;

	SerializedProperty threshold;

	void OnEnable()
	{
		srcObj = new SerializedObject(target);

		threshold = srcObj.FindProperty("threshold");
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.Slider(threshold, 1, 255, "Threshold");

		srcObj.ApplyModifiedProperties();
    }
}
