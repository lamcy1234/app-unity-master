using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Radial Blur")]
public class CC_RadialBlur : MonoBehaviour
{
	public float amount = 0.1f;
	public Vector2 center = new Vector2(0.5f, 0.5f);
	public int quality = 1;

	public Shader shaderLow;
	public Shader shaderMed;
	public Shader shaderHigh;

	private Shader _currentShader;
	private Material _material;

	void Start()
	{
		// Disable if we don't support image effects
		if (!SystemInfo.supportsImageEffects)
		{
			enabled = false;
			return;
		}
	}

	bool CheckShader()
	{
		// Disable the image effect if the shader can't
		// run on the users graphics card
		if (!_currentShader || !_currentShader.isSupported)
			return false;

		return true;
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("amount", amount);
		_material.SetVector("center", center);

		if (!CheckShader())
		{
			Graphics.Blit(source, destination);
			return;
		}

		Graphics.Blit(source, destination, _material);
	}

	Material material
	{
		get
		{
			if (quality == 0)
				_currentShader = shaderLow;
			else if (quality == 1)
				_currentShader = shaderMed;
			else if (quality == 2)
				_currentShader = shaderHigh;

			if (_material == null)
			{
				_material = new Material(_currentShader);
				_material.hideFlags = HideFlags.HideAndDontSave;
			}
			else _material.shader = _currentShader;

			return _material;
		} 
	}
	
	void OnDisable()
	{
		if (_material)
			DestroyImmediate(_material);
	}
}
