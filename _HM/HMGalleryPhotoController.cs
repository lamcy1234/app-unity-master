using UnityEngine;
using System;
using System.Collections;

public class HMGalleryPhotoController : MonoBehaviour {
	
	public UISprite Spinner;
	public UITexture productImage;
	public string productImageUrl;
	
	private UIGrid parentGrid;
	
	void Start () {
		loadContent();
		handleRetina();
	}
	
	void OnClick()
	{
		//Debug.Log (productImageUrl);
		//HMAboutSubviewController.Instance.photos.IndexOf(productImageUrl)
		HMAboutSubviewController.Instance.selectPhoto(productImageUrl);
		GUIEventController.UIEventTriggered(GUIEventController.EventName.HMOpenGalleryPressed);
	}
	
	private void loadContent() {
		Texture2D productImageTex = new Texture2D(4, 4, TextureFormat.RGB24, false);
		StartCoroutine(HttpLoadTexture(productImageUrl, productImageTex, () => {
			productImage.mainTexture = productImageTex;
			productImage.MakePixelPerfect();
			
			float photoHeight = Screen.height/3 * 0.8f;
			float photoWidth = photoHeight * 1.5f;
			
			Vector3 scale = new Vector3(photoWidth, photoHeight , productImage.transform.localScale.z);
			productImage.transform.localScale = scale;
			
			gameObject.GetComponent<BoxCollider>().size = scale;
			
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
	
	private void handleRetina ()
	{
		foreach (UIWidget widget in gameObject.transform.parent.GetComponentsInChildren<UIWidget>()) 
		{
			widget.transform.localScale = new Vector3 (widget.transform.localScale.x*2, widget.transform.localScale.y*2, widget.transform.localScale.z);
		}
	}
}
