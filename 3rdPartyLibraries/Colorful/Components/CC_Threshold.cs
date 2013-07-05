using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Threshold")]
public class CC_Threshold : CC_Base
{
	public float threshold = 128f;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_threshold", threshold / 255);
		Graphics.Blit(source, destination, material);
	}
}
