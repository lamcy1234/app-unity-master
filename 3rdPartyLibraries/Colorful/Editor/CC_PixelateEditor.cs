using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Pixelate))]
public class CC_PixelateEditor : Editor
{
	SerializedObject srcObj;

	SerializedProperty scale;

	void OnEnable()
	{
		srcObj = new SerializedObject(target);

		scale = srcObj.FindProperty("scale");
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.Slider(scale, 1, 255, "Scale");

		srcObj.ApplyModifiedProperties();
    }
}
