using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class TwitterAndroidManager : MonoBehaviour
{
#if UNITY_ANDROID
	// Fired when a login is successful.  Provides the users screen name
	public static event Action<string> loginDidSucceedEvent;
	
	// Fired when a login fails with the error that occurred
	public static event Action<string> loginDidFailEvent;
	
	// Fired when a request succeeds.  The returned object will be either an ArrayList or a Hashtable depending on the request
	public static event Action<object> requestSucceededEvent;
	
	// Fired when a request fails with the error message
	public static event Action<string> requestFailedEvent;
	
	// Fired when the Twitter Plugin is initialized and ready for use.  Do not call any other methods until this fires!
	public static event Action twitterInitializedEvent;


	void Awake()
	{
		// Set the GameObject name to the class name for easy access from Obj-C
		gameObject.name = this.GetType().ToString();
		DontDestroyOnLoad( this );
	}


	public void loginDidSucceed( string username )
	{
		if( loginDidSucceedEvent != null )
			loginDidSucceedEvent( username );
	}


	public void loginDidFail( string error )
	{
		if( loginDidFailEvent != null )
			loginDidFailEvent( error );
	}


	public void requestSucceeded( string response )
	{
		if( requestSucceededEvent != null )
			requestSucceededEvent( Prime31.MiniJSON.jsonDecode( response ) );
	}


	public void requestFailed( string error )
	{
		if( requestFailedEvent != null )
			requestFailedEvent( error );
	}


	public void twitterInitialized( string empty )
	{
		if( twitterInitializedEvent != null )
			twitterInitializedEvent();
	}
#endif
}

