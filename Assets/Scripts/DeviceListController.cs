using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceListController : MonoBehaviour {

	public Text deviceName;
	public int id;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick()
	{

		for (int i = 0; i < transform.parent.childCount; i++)
			transform.parent.GetChild (i).GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		GetComponent<Image> ().color = new Color (0.8f, 0.8f, 0.8f, 1f);


		GameObject.Find ("bmController").SendMessage ("OnSelectDevice", id);
	}
}
