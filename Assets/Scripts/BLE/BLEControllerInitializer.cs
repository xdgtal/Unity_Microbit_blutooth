namespace BLEFramework.Unity
{
	using UnityEngine;
	using System.Collections;
	
	public class BLEControllerInitializer : MonoBehaviour 
	{
		

		void Start()
		{
			
		}
		
		
		public void InitBLEFramework()
		{
			//BLEController.InitBLEFramework();
		}
		
		void OnEnable()
		{
			//BLEControllerEventHandler.OnBleDidInitializeEvent += HandleOnBleDidInitializeEvent;
			//BLEControllerEventHandler.OnBleDidInitializeErrorEvent += HandleOnBleDidInitializeErrorEvent;
		}
		
		void HandleOnBleDidInitializeErrorEvent (string errorMessage)
		{
			//Debug.Log ("BLEControllerInitializer: HandleOnBleDidInitializeEvent: Error initializing BLE Framework: "  + errorMessage);	
		}
		void HandleOnBleDidInitializeEvent (string message)
		{
			//Debug.Log ("BLEControllerInitializer: HandleOnBleDidInitializeEvent: BLE framework successful initialization");
		}
		
		void OnDisable()
		{
			//BLEControllerEventHandler.OnBleDidInitializeEvent -= HandleOnBleDidInitializeEvent;
			//BLEControllerEventHandler.OnBleDidInitializeErrorEvent -= HandleOnBleDidInitializeErrorEvent;
		}
			
	}
}