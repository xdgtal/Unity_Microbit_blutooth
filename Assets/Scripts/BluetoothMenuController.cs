using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BLEFramework.Unity;
using BLEFramework.MiniJSON;

public class BluetoothMenuController : MonoBehaviour {


	public Text infoMessage;
	public Text cText;

	private int clientCount = 0;
	public GameObject searching;

	public GameObject deviceLists;

	public bool isBleInitialized;

	public Button SearchButton;
	public Button ConnectButton;

	// Use this for initialization
	void Start () {
//		if(isBleInitialized==false)
//			BLEController.InitBLEFramework();
	}
		
	void OnEnable()
	{
		
	}

	void OnDisable()
	{
		BLEControllerEventHandler.OnBleDidInitializeEvent -= HandleOnBleDidInitializeEvent;
		BLEControllerEventHandler.OnBleDidInitializeErrorEvent -= HandleOnBleDidInitializeErrorEvent;

		BLEControllerEventHandler.OnBleDidCompletePeripheralScanEvent -= HandleOnBleDidCompletePeripheralScanEvent;
		BLEControllerEventHandler.OnBleDidConnectEvent -= HandleOnBleDidConnectEvent;
		BLEControllerEventHandler.OnBleDidDisconnectEvent += HandleOnBleDidDisconnectEvent;
		BLEControllerEventHandler.OnBleDidConnectErrorEvent -= HandleOnBleDidConnectErrorEvent;
		BLEControllerEventHandler.OnBleDidReceiveDataEvent -= HandleOnBleDidReceiveDataEvent;
	}
	void HandleOnBleDidConnectErrorEvent(string message)
	{
		cText.text = "Can not connect to device";
	}
	void HandleOnBleDidInitializeEvent (string message)
	{
		infoMessage.GetComponent<Text>().text = "initialize" + message;

		if (message == "Success")
			isBleInitialized = true;

	}

	void HandleOnBleDidInitializeErrorEvent (string errorMessage)
	{
		infoMessage.text = errorMessage;
	}
	public GameObject listBtn;
	public Transform listScroll;

	void HandleOnBleDidCompletePeripheralScanEvent (List<object> peripherals)
	{						
		deviceLists.SetActive (true);


		float width = listScroll.GetComponent<RectTransform> ().rect.width;
		float height = deviceLists.GetComponent<RectTransform> ().rect.height;

		listScroll.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (width, listScroll.GetComponent<GridLayoutGroup> ().cellSize.y);
		for (int i = 0; i < listScroll.transform.childCount; i++)
			UnityEngine.Object.Destroy (listScroll.transform.GetChild (i).gameObject);


		for (int i = 0; i < peripherals.Count; i++) {
			GameObject obj = Instantiate (listBtn);
			obj.transform.SetParent (listScroll);
			obj.transform.localScale = Vector3.one;
			obj.GetComponent<DeviceListController> ().deviceName.text = (string)peripherals [i];
			obj.GetComponent<DeviceListController> ().id = i;
		}

		cText.text = "";
		searching.SetActive (false);
		SearchButton.interactable = true;
		ConnectButton.interactable = true;


	}

	void HandleOnBleDidDisconnectEvent()
	{
		
	}

	void HandleOnBleDidConnectEvent ()
	{
		infoMessage.text = "connecting done.";

		JSONObject jobj = new JSONObject ();
		jobj.AddField ("type", "handshake");

	}

	void HandleOnBleDidReceiveDataEvent (byte[] data, int numOfBytes)
	{

		JSONObject jobj = JSONObject.Create (Json.GetStringFromBytes (data));


		if (jobj.GetField("type") == null)
			return;

		if (jobj.GetField ("type").str == "handshake") {

			cText.text = jobj.GetField ("name").str;
			SearchButton.interactable = false;
			ConnectButton.interactable = false;
			searching.SetActive (value: false);
			BLEController.SendData (Json.GetBytesFromString (jobj.ToString ()));
		} else if (jobj.GetField ("type").str == "play"){
		
		}
	}

	// calling method

	public void OnConnect()
	{
		bool result = BLEController.ConnectPeripheralAtIndex(selecteDevIdx);
		deviceLists.SetActive (false);
		searching.SetActive (value: true);
		cText.text = "Connecting...";
	}
		
	public void SearchBLEDevices()
	{
		BLEController.InitBLEFramework();

		deviceLists.SetActive (value: false);
		searching.SetActive (true);
		SearchButton.interactable = false;

		cText.text = "Searching...";
		BLEController.ScanForPeripherals();	
	}

	public void TurnOnBLE()
	{
        if (isBleInitialized == false)
            BLEController.InitBLEFramework();

        BLEControllerEventHandler.OnBleDidInitializeEvent += HandleOnBleDidInitializeEvent;
        BLEControllerEventHandler.OnBleDidInitializeErrorEvent += HandleOnBleDidInitializeErrorEvent;
        BLEControllerEventHandler.OnBleDidCompletePeripheralScanEvent += HandleOnBleDidCompletePeripheralScanEvent;
        BLEControllerEventHandler.OnBleDidConnectEvent += HandleOnBleDidConnectEvent;
        BLEControllerEventHandler.OnBleDidDisconnectEvent += HandleOnBleDidDisconnectEvent;
        //BLEControllerEventHandler.OnBleDidConnectErrorEvent += HandleOnBleDidConnectErrorEvent;
        BLEControllerEventHandler.OnBleDidReceiveDataEvent += HandleOnBleDidReceiveDataEvent;

        cText.text = "No search result";

        this.clientCount = 0;
    }

	public int selecteDevIdx;
	public void OnSelectDevice(int id)
	{
		selecteDevIdx = id;
		infoMessage.text = selecteDevIdx.ToString ();
	}
}
