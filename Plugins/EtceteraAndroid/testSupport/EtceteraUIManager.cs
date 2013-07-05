using UnityEngine;
using System.Collections.Generic;


public class EtceteraUIManager : MonoBehaviour
{
	public GameObject testPlane;
	
#if UNITY_ANDROID

	void Start()
	{
		// kick off the TTS system so it is ready for use later
		EtceteraAndroid.initTTS();
	}
	
	
	void OnEnable()
	{
		// Listen to the texture loaded methods so we can load up the image on our plane
		EtceteraAndroidManager.albumChooserSucceededEvent += textureLoaded;
		EtceteraAndroidManager.photoChooserSucceededEvent += textureLoaded;
	}
	
	
	void OnDisable()
	{
		EtceteraAndroidManager.albumChooserSucceededEvent -= textureLoaded;
		EtceteraAndroidManager.photoChooserSucceededEvent -= textureLoaded;
	}
	
	
	private string saveScreenshotToSDCard()
	{
		var tex = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
		tex.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0, false );

		var bytes = tex.EncodeToPNG();
		Destroy( tex );
		
		var path = System.IO.Path.Combine( Application.persistentDataPath, "myImage.png" );
		System.IO.File.WriteAllBytes( path, bytes );
		
		return path;
	}
	
	
	void OnGUI()
	{
		float yPos = 5.0f;
		float xPos = 5.0f;
		float width = ( Screen.width >= 800 || Screen.height >= 800 ) ? 320 : 160;
		float height = ( Screen.width >= 800 || Screen.height >= 800 ) ? 55 : 30;
		float heightPlus = height + 3.0f;
	
		
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Show Toast" ) )
		{
			EtceteraAndroid.showToast( "Hi.  Something just happened in the game and I want to tell you but not interrupt you", true );
		}
	
	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Play Video" ) )
		{
			// closeOnTouch has no effect if you are showing controls
			EtceteraAndroid.playMovie( "http://www.daily3gp.com/vids/747.3gp", 0xFF0000, false, EtceteraAndroid.ScalingMode.AspectFit, true );
		}

	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Show Alert" ) )
		{
			EtceteraAndroid.showAlert( "Alert Title Here", "Something just happened.  Do you want to have a snack?", "Yes", "Not Now" );
		}
	
	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Single Field Prompt" ) )
		{
			EtceteraAndroid.showAlertPrompt( "Enter Digits", "I'll call you if you give me your number", "phone number", "867-5309", "Send", "Not a Chance" );
		}
	
	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Two Field Prompt" ) )
		{
			EtceteraAndroid.showAlertPromptWithTwoFields( "Need Info", "Enter your credentials:", "username", "harry_potter", "password", string.Empty, "OK", "Cancel" );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Show Progress Dialog" ) )
		{
			EtceteraAndroid.showProgressDialog( "Progress is happening", "it will be over in just a second..." );
			Invoke( "hideProgress", 1 );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Text to Speech Speak" ) )
		{
			EtceteraAndroid.setPitch( Random.Range( 0, 5 ) );
			EtceteraAndroid.setSpeechRate( Random.Range( 0.5f, 1.5f ) );
			EtceteraAndroid.speak( "Howdy. Im a robot voice" );
		}
		

		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Prompt for Video" ) )
		{
			EtceteraAndroid.promptToTakeVideo( "fancyVideo" );
		}
		

		
		// second column of buttons
		xPos = Screen.width - width - 5.0f;
		yPos = 5.0f;
		
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Show Web View" ) )
		{
			EtceteraAndroid.showWebView( "http://prime31.com" );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Email Composer" ) )
		{
			// grab an attachment for this email
			var path = saveScreenshotToSDCard();
			
			EtceteraAndroid.showEmailComposer( "noone@nothing.com", "Message subject", "click <a href='http://somelink.com'>here</a> for a present", true, path );
		}


		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "SMS Composer" ) )
		{
			EtceteraAndroid.showSMSComposer( "I did something really cool in this game!" );
		}


		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Prompt to Take Photo" ) )
		{
			EtceteraAndroid.promptToTakePhoto( 512, 512, "photo.jpg" );
		}


		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Prompt for Album Image" ) )
		{
			EtceteraAndroid.promptForPictureFromAlbum( 512, 512, "albumImage.jpg" );
		}


		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Save Image to Gallery" ) )
		{
			var path = saveScreenshotToSDCard();
			
			var didSave = EtceteraAndroid.saveImageToGallery( path, "My image from Unity" );
			Debug.Log( "did save to gallery: " + didSave );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Ask For Review" ) )
		{
			// reset just in case you accidentally press dont ask again
			EtceteraAndroid.resetAskForReview();
			EtceteraAndroid.askForReviewNow( "Please rate my app!", "It will really make me happy if you do..." );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Next Scene" ) )
		{
			Application.LoadLevel( "EtceteraTestSceneTwo" );
		}
	}
	
	
	private void hideProgress()
	{
		EtceteraAndroid.hideProgressDialog();
	}

	
	// Texture loading delegates
	public void textureLoaded( string imagePath, Texture2D texture )
	{
		testPlane.renderer.material.mainTexture = texture;
	}
#endif
}
