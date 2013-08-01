using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChairNavContainerController : MonoBehaviour {
	public GameObject chairCheckBoxPrefab;
	private List<Product> products;
	
	void OnEnable()
	{
		products = SayduckARViewController.Instance.getProductList();
		
		setupRightMenu();
		
		if (AppGUISettings.IS_RETINA) {
			handleRetina();
		}
		
		loadCheckboxTextures();
	}
	
	private void setupRightMenu()
	{
		int gap = 5;
		float checkboxHeight = chairCheckBoxPrefab.GetComponent<BoxCollider>().size.x;
		
		if (AppGUISettings.IS_RETINA) {
			gap *= 2;
			checkboxHeight *= 2;
		}
		
		float screenH = (checkboxHeight + gap) * products.Count;
		float div = screenH / products.Count;
		float halfScreen = screenH / 2;
		
		int i=0;
		foreach (Product product in products)
		{
			GameObject go = Instantiate(chairCheckBoxPrefab) as GameObject;
			go.transform.parent = gameObject.transform;
			go.GetComponent<HMChairCheckboxController>().initData(product);
			go.GetComponent<UICheckbox>().radioButtonRoot = gameObject.transform;
			if (SayduckARViewController.Instance.getCurrentProductId() == product.id) {
				go.GetComponent<UICheckbox>().startsChecked = true;
			}
			
		
			float posY = -halfScreen + (div * i) + (div / 2);
			float posX = Screen.width / 3;
			go.transform.localPosition = new Vector3(posX, -posY, go.transform.localPosition.z);
			
			i++;
		}
	}
	
	private void loadCheckboxTextures()
	{
		foreach (HMChairCheckboxController cbController in gameObject.GetComponentsInChildren<HMChairCheckboxController>()) {
			cbController.loadContent();
		}
	}
	
	private void handleRetina ()
	{
		foreach (GameObject checkbox in gameObject.Children()) {
			BoxCollider bc = checkbox.GetComponent<BoxCollider>();
			bc.size = new Vector3(bc.size.x * 2, bc.size.y * 2, bc.size.z);
			
			foreach (GameObject go in checkbox.Children()) {
				go.transform.localScale = new Vector3 (go.transform.localScale.x * 2, go.transform.localScale.y * 2, go.transform.localScale.z);
			}
		}
	}
	
	
}
