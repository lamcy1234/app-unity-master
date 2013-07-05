using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/LED")]
public class CC_Led : CC_Base
{
	public float scale = 80.0f;
	public float brightness = 1.0f;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_scale", scale);
		material.SetFloat("_brightness", brightness);
		Graphics.Blit(source, destination, material);
	}
}
