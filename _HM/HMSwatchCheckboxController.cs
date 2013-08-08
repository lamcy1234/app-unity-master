using UnityEngine;
using System;
using System.Collections;

public class HMSwatchCheckboxController : MonoBehaviour {
	public UITexture CheckboxTexture;
	public UISprite Spinner;
	public GameObject CheckboxBackground;
	public GameObject CheckboxCheckmark;
	public GameObject Bg;
	public Vector2 size;
	
	private string SwatchUrl;
	
	private ProductMaterial productMaterial;
	private UnityProductModel unityProductModel;
	private UICheckbox checkbox;
	
	private Vector3 checkboxLocalScale;
	private float scaleRatito = 0.85f;
	
	void OnEnable () {
		checkbox = GetComponent<UICheckbox>();
		checkboxLocalScale = new Vector3 (size.x * scaleRatito, size.y * scaleRatito, 1);
		
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.size = checkboxLocalScale;
		CheckboxCheckmark.transform.localScale = checkboxLocalScale * 1.1f;
		CheckboxBackground.transform.localScale = checkboxLocalScale;
		Bg.transform.localScale = checkboxLocalScale;
	}
	
	public void initData (UnityProductModel model, ProductMaterial mat, string GameObjectName)
	{
		this.SwatchUrl = mat.swatchURL2x;
		gameObject.transform.name = GameObjectName;
		
		this.productMaterial = mat;
		this.unityProductModel = model;
		
		if ((model.activeUnityProductMaterial.name == mat.name) && (checkbox != null)) {
			checkbox.startsChecked = true;
		}
	}
	
	public void loadCheckBoxTexture() {
		Texture2D Tex = new Texture2D(4, 4, TextureFormat.RGB24, false);
		StartCoroutine(HttpLoadTexture(SwatchUrl, Tex, () => {
			CheckboxTexture.mainTexture = Tex;
			//CheckboxTexture.material.SetTexture("_ShadowTex", Tex);
			CheckboxTexture.MakePixelPerfect();
			
			CheckboxTexture.transform.localScale = checkboxLocalScale * 0.5f;
						
			if (Spinner != null) {
				Destroy(Spinner.gameObject);
			}
			
		}));
	}
	
	IEnumerator HttpLoadTexture(string url, Texture2D inTex, Action onComplete) {
		WWW www = new WWW(url);
		yield return www;
		
		www.LoadImageIntoTexture(inTex);
		
		onComplete();
	}
	
	void OnClick()
	{
		if (checkbox != null) {
			if (!checkbox.isChecked) {
				int materialIndex = unityProductModel.productModel.productMaterials.IndexOf(productMaterial);
				unityProductModel.changeMaterial(materialIndex);
			}
		}
	}
	
}
