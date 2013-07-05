using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Posterize))]
public class CC_PosterizeEditor : Editor
{
	SerializedObject srcObj;

	SerializedProperty levels;

	void OnEnable()
	{
		srcObj = new SerializedObject(target);

		levels = srcObj.FindProperty("levels");
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.IntSlider(levels, 2, 255, "Levels");

		srcObj.ApplyModifiedProperties();
    }
}
