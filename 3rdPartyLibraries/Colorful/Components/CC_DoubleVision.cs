using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Double Vision")]
public class CC_DoubleVision : CC_Base
{
	public Vector2 displace = new Vector2(0.7f, 0.0f);
	public float amount = 1.0f;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetVector("_displace", new Vector2(displace.x / Screen.width, displace.y / Screen.height));
		material.SetFloat("_amount", amount);
		Graphics.Blit(source, destination, material);
	}
}
