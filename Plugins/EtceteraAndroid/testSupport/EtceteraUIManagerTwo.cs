using UnityEngine;
using System.Collections.Generic;


public class EtceteraUIManagerTwo : MonoBehaviour
{
#if UNITY_ANDROID
	void OnGUI()
	{
		float yPos = 5.0f;
		float xPos = 5.0f;
		float width = ( Screen.width >= 800 || Screen.height >= 800 ) ? 320 : 160;
		float height = ( Screen.width >= 800 || Screen.height >= 800 ) ? 55 : 30;
		float heightPlus = height + 3.0f;
	

		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Show Inline Web View" ) )
		{
			EtceteraAndroid.inlineWebViewShow( "http://www.prime31.com", 160, 30, Screen.width - 160, Screen.height - 100 );
		}
	
	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Close Inline Web View" ) )
		{
			EtceteraAndroid.inlineWebViewClose();
		}
	
	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Set Url of Inline Web View" ) )
		{
			EtceteraAndroid.inlineWebViewSetUrl( "http://google.com" );
		}
	
	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Set Frame of Inline Web View" ) )
		{
			EtceteraAndroid.inlineWebViewSetFrame( 80, 50, 300, 400 );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus * 2, width, height ), "Previous Scene" ) )
		{
			Application.LoadLevel( "EtceteraTestScene" );
		}
	}

#endif
}
