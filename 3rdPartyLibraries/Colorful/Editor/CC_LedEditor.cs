using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Led))]
public class CC_LedEditor : Editor
{
	SerializedObject srcObj;

	SerializedProperty scale;
	SerializedProperty brightness;

	void OnEnable()
	{
		srcObj = new SerializedObject(target);

		scale = srcObj.FindProperty("scale");
		brightness = srcObj.FindProperty("brightness");
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.Slider(scale, 1, 255, "Scale");
		EditorGUILayout.Slider(brightness, 0.0f, 10.0f, "Brightness");

		srcObj.ApplyModifiedProperties();
    }
}
