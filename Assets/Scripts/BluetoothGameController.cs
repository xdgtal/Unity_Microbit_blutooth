using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BLEFramework.Unity;
using BLEFramework.MiniJSON;

public class BluetoothGameController : MonoBehaviour {

	// Use this for initialization
	void Start () {

		//BLEController.InitBLEFramework();
	}
		
	void OnEnable()
	{
		BLEControllerEventHandler.OnBleDidReceiveDataEvent += HandleOnBleDidReceiveDataEvent;

		BLEControllerEventHandler.OnBleDidDisconnectEvent += HandleOnBleDidDisconnectEvent;
	}

	void OnDisable()
	{
		BLEControllerEventHandler.OnBleDidReceiveDataEvent -= HandleOnBleDidReceiveDataEvent;

		BLEControllerEventHandler.OnBleDidDisconnectEvent -= HandleOnBleDidDisconnectEvent;
	}

	int cnt;

	bool isServerStartRecieve;
	bool isGameStartRecieve;

	void HandleOnBleDidReceiveDataEvent (byte[] data, int numOfBytes)
	{
		String s = Json.GetStringFromBytes (data);
		JSONObject jobj = new JSONObject (s);
		string type = jobj.GetField ("type").str;
	}

	void HandleOnBleDidDisconnectEvent()
	{

	}
}
