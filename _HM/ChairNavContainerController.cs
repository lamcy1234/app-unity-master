using UnityEngine;
using System.Collections;

public class ChairNavContainerController : MonoBehaviour {
	public GameObject[] buttons;
	
	void OnEnable() {
		GUIEventController.registerEventHandler(GUIEventController.EventName.HMEmbody1Pressed, handleHMEmbody1Pressed);
		GUIEventController.registerEventHandler(GUIEventController.EventName.HMAeronPressed, handleHMAeronPressed);
		GUIEventController.registerEventHandler(GUIEventController.EventName.HMEmbody2Pressed, handleHMEmbody2Pressed);
		GUIEventController.registerEventHandler(GUIEventController.EventName.HMCellePressed, handleHMCellePressed);
		GUIEventController.registerEventHandler(GUIEventController.EventName.HMSaylPressed, handleHMSaylPressed);

	}
	
	void OnDisable() {
		GUIEventController.deregisterEventHandler(GUIEventController.EventName.HMEmbody1Pressed, handleHMEmbody1Pressed);
		GUIEventController.deregisterEventHandler(GUIEventController.EventName.HMAeronPressed, handleHMAeronPressed);
		GUIEventController.deregisterEventHandler(GUIEventController.EventName.HMEmbody2Pressed, handleHMEmbody2Pressed);
		GUIEventController.deregisterEventHandler(GUIEventController.EventName.HMCellePressed, handleHMCellePressed);
		GUIEventController.deregisterEventHandler(GUIEventController.EventName.HMSaylPressed, handleHMSaylPressed);
	}
	
	void Start() {		
		
		if (buttons.Length > 0) 
		{
			handleRetina();
			setupRightMenu();
			setCurrentProductActive();
		}
		
	}
	
	private void handleHMEmbody1Pressed ()
	{
		loadProduct(0);
	}
	
	private void handleHMAeronPressed ()
	{
		//loadProduct(1);
	}
	
	private void handleHMEmbody2Pressed ()
	{
		//loadProduct(2);
	}
	
	private void handleHMCellePressed ()
	{
		loadProduct(1);
	}
	
	private void handleHMSaylPressed ()
	{
		loadProduct(2);
	}
	
	private void loadProduct (int id) {
		Debug.Log (id);
		SayduckARViewController.Instance.loadProduct(id);
	}
	
	
	private void setupRightMenu()
	{
		int gap = 5;
		if (AppGUISettings.IS_RETINA) {
			gap *= 2;
		}
		float screenH = (buttons[0].GetComponent<BoxCollider>().size.y + gap) * buttons.Length;
		float div = screenH / buttons.Length;
		float halfScreen = screenH / 2;
		
		for (int i = 0; i < buttons.Length; i++) {
			float posY = -halfScreen + (div * i) + (div / 2);
			float posX = Screen.width / 3;
			buttons[i].transform.localPosition = new Vector3(posX, -posY, buttons[i].transform.localPosition.z);
		}
	}
	
	private void setCurrentProductActive ()
	{
		int currentProductId = SayduckARViewController.CURRENT_ID;
		GameObject go = buttons[currentProductId];
		go.GetComponent<UICheckbox>().startsChecked = true;
	}
	
	private void handleRetina ()
	{
		if (AppGUISettings.IS_RETINA) {
			for (int i = 0; i < buttons.Length; i++) 
			{
				BoxCollider bc = buttons[i].GetComponent<BoxCollider>();
				bc.size = new Vector3(bc.size.x * 2, bc.size.y * 2, bc.size.z);
				
				foreach (GameObject go in buttons[i].Children())
				{
					go.transform.localScale = new Vector3 (go.transform.localScale.x * 2, go.transform.localScale.y * 2, go.transform.localScale.z);
					go.GetComponent<UIWidget>().MakePixelPerfect();
				}
			}
		}
	}
	
}
