using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_FastVignette))]
public class CC_FastVignetteEditor : Editor
{
	SerializedObject srcObj;

	SerializedProperty sharpness;
	SerializedProperty darkness;

	void OnEnable()
	{
		srcObj = new SerializedObject(target);

		sharpness = srcObj.FindProperty("sharpness");
		darkness = srcObj.FindProperty("darkness");
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.Slider(sharpness, -100.0f, 100.0f, "Sharpness");
		EditorGUILayout.Slider(darkness, 0.0f, 100.0f, "Darkness");

		srcObj.ApplyModifiedProperties();
    }
}
