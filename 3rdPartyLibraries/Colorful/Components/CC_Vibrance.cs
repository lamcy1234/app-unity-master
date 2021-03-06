using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Vibrance")]
public class CC_Vibrance : CC_Base
{
	public float amount = 0.0f;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_amount", amount * 0.02f);
		Graphics.Blit(source, destination, material);
	}
}
