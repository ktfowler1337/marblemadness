using UnityEngine;
using System.Collections;

public class fallingOff : MonoBehaviour {
	 GameObject player;
	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			player.transform.position = PlayerController.RespawnLocation;
			PlayerController.playerRigidBody.isKinematic = true;
			PlayerController.playerRigidBody.velocity = Vector3.zero;
			PlayerController.playerRigidBody.isKinematic = false;
		}
	}
}
