using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Grayscale")]
public class CC_Grayscale : CC_Base
{
	public float redLuminance = 0.30f;
	public float greenLuminance = 0.59f;
	public float blueLuminance = 0.11f;
	public float amount = 1.0f;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_rLum", redLuminance);
		material.SetFloat("_gLum", greenLuminance);
		material.SetFloat("_bLum", blueLuminance);
		material.SetFloat("_amount", amount);
		Graphics.Blit(source, destination, material);
	}
}
