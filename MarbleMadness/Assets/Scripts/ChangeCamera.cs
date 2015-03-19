using UnityEngine;
using System.Collections;

public class ChangeCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		PlayerController.jumpPower = 35f;
		Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
		CameraController.offset = new Vector3(0, 0, -10);
	}
}
