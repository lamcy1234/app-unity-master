using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Frost")]
public class CC_Frost : CC_Base
{
	public float scale = 1.2f;
	public float sharpness = 40f;
	public float darkness = 35f;
	public bool enableVignette = true;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_scale", scale);
		material.SetFloat("_enableVignette", enableVignette ? 1.0f : 0.0f);
		material.SetFloat("_sharpness", sharpness * 0.01f);
		material.SetFloat("_darkness", darkness * 0.02f);
		Graphics.Blit(source, destination, material);
	}
}
