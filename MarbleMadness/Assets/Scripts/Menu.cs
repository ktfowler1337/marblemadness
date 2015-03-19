using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	
	public static GameObject menu;
	private static GameObject aboutImg;
	private static GameObject helpImg;
	List<GameObject> pickups;
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		aboutImg = GameObject.Find ("AboutImage");
		helpImg = GameObject.Find ("HelpImage");

		helpImg.SetActive (false);
		aboutImg.SetActive (false); 
		pickups = new List<GameObject> ();
		pickups.AddRange (GameObject.FindGameObjectsWithTag ("PickUpl1"));
		pickups.AddRange (GameObject.FindGameObjectsWithTag ("PickUpl2"));
		pickups.AddRange (GameObject.FindGameObjectsWithTag ("PickUpl3"));
		pickups.AddRange (GameObject.FindGameObjectsWithTag ("Blocker"));
		pickups.AddRange (GameObject.FindGameObjectsWithTag ("Check Point"));

		Button b = GameObject.Find ("Start Level 1").GetComponent<Button> ();
		b.onClick.AddListener (() => StartLevel (1));

		 b = GameObject.Find ("Start Level 2").GetComponent<Button> ();
		b.onClick.AddListener (() => StartLevel (2));

		 b = GameObject.Find ("Start Level 3").GetComponent<Button> ();
		b.onClick.AddListener (() => StartLevel (3));

		b = GameObject.Find ("About").GetComponent<Button> ();
		b.onClick.AddListener (() => ShowAbout ());

		b = GameObject.Find ("Help").GetComponent<Button> ();
		b.onClick.AddListener (() => ShowHelp ());

		b = GameObject.Find ("Restart").GetComponent<Button> ();
		b.onClick.AddListener (() => StartLevel (1));


		menu = GameObject.Find("Menu");
		menu.SetActive (false);

	}

	public static void HideMenu()
	{
		helpImg.SetActive (false);
		aboutImg.SetActive (false);
	}

	private void ShowAbout()
	{
		helpImg.SetActive (false);
		aboutImg.SetActive (true);
	}

	private void ShowHelp()
	{
		helpImg.SetActive (true);
		aboutImg.SetActive (false);
	}
	
	void StartLevel(int number)
	{
		ResetWorld ();
		PlayerController.resetCounts ();
		if (number == 1) {
			PlayerController.RespawnLocation = GameObject.Find ("Level 1 Respawn").transform.position;
		}
		else if (number == 2) {
			PlayerController.RespawnLocation = GameObject.Find ("Level 2 Respawn").transform.position;
		}
		else if (number == 3) {
			PlayerController.RespawnLocation = GameObject.Find ("Level 3 Respawn").transform.position;
		}
		Camera.main.transform.rotation = Quaternion.Euler(45, 0, 0);
		CameraController.offset = new Vector3(0, 10, -10);
		player.transform.position = PlayerController.RespawnLocation;
	}

	void ResetWorld()
	{
		foreach (GameObject pickup in pickups) {
			pickup.SetActive(true);
		}

	}

	void Update()
	{

	}
}
