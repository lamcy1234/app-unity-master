using UnityEngine;
using System;
using System.Collections;

public class HMChairCheckboxController : MonoBehaviour {
	public UITexture CheckboxTexture;
	public UILabel ItemLabel;
	public UISprite Spinner;
	
	private string TextureUrl;
	private string ProductId;
	
	public void initData (Product product)
	{
		this.TextureUrl = product.thumbnailImageUrl;
		this.ProductId = product.id;
		gameObject.transform.name = product.name;
		ItemLabel.text = product.name;
	}
	
	public void loadCheckBoxTexture() {
		Texture2D Tex = new Texture2D(4, 4, TextureFormat.RGB24, false);
		StartCoroutine(HttpLoadTexture(TextureUrl, Tex, () => {
			CheckboxTexture.mainTexture = Tex;
			CheckboxTexture.MakePixelPerfect();
			
			Vector3 scale = gameObject.GetComponent<BoxCollider>().size;
			CheckboxTexture.transform.localScale = scale * 0.6f;
						
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
		SayduckARViewController.Instance.displayProductModel(this.ProductId);
	}
}
