using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
	public float speed;

	public static int count;
	private static int Level1PickUpCount = 0;
	private static int Level2PickUpCount = 0;
	private static int Level3PickUpCount = 0;

	public Text countText;
	public Text timerText;
	public Text highscoreText;
	private float OldyLocation = 0;
	public static Vector3 RespawnLocation;
	public static Rigidbody playerRigidBody;
	public static float jumpPower = 20f;
	public static float timer = 0f;

	private AudioSource source;

	public AudioClip pickupSound;
	public AudioClip levelComplete;
	public AudioClip jumpSound;

	float highScore = 0f;

	public static void resetCounts()
	{
		count = 0;
		Level1PickUpCount = 0;
		Level2PickUpCount = 0;
		timer = 0;
	}

	void Start()
	{
		source = GetComponent<AudioSource>();
		count = 0;
		updateCountText ();
		playerRigidBody = GetComponent<Rigidbody> ();
		RespawnLocation = playerRigidBody.transform.position;
		float highscoretext = PlayerPrefs.GetFloat ("highScore");
		if (highScore == 0) {
			highscoretext = 10000f;
			PlayerPrefs.SetFloat("highScore", highscoretext); 
			PlayerPrefs.Save();
		}
		highscoreText.text = "Highscore: " +string.Format("{0:N1}", highscoretext);

		List<GameObject> pickups = new List<GameObject> ();
		pickups.AddRange (GameObject.FindGameObjectsWithTag ("PickUpl3"));
		int countp = pickups.Count;
	}

	void FixedUpdate()
	{
		float moveHor = Input.GetAxis ("Horizontal");
		float MoveVert = Input.GetAxis("Vertical");
		float jumpers = 0;

		float diff = playerRigidBody.position.y - OldyLocation;
		float diff2 = OldyLocation - playerRigidBody.position.y;
		if (Input.GetKeyDown (KeyCode.Space) && diff < 0.0001f && diff2 < 0.0001f) {
			jumpers = jumpPower;
			source.PlayOneShot (jumpSound);
		}



		Vector3 movement = new Vector3(moveHor, jumpers ,MoveVert);

		playerRigidBody.AddForce (movement * speed * Time.deltaTime);
		OldyLocation = playerRigidBody.position.y;


	}
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Menu.menu.SetActive(!Menu.menu.activeSelf);
			if(Menu.menu.activeSelf)
			{
				Time.timeScale = 0;
			}
			else
			{
				Menu.HideMenu();
				Time.timeScale = 1;
			}
		}
		timer += Time.deltaTime;
		updateTimerText ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Contains ("PickUp"))
			source.PlayOneShot (pickupSound);
		if (other.gameObject.tag == "PickUpl1") {
			other.gameObject.SetActive(false);
			Level1PickUpCount++;
			if(Level1PickUpCount == 12)
			{
				source.PlayOneShot (levelComplete);
				GameObject wall = GameObject.Find("SouthWall 2");
				wall.SetActive(false);
			}
		}
		else if (other.gameObject.tag == "PickUpl2") {
			other.gameObject.SetActive(false);
			Level2PickUpCount++;
			if(Level2PickUpCount == 28)
			{
				source.PlayOneShot (levelComplete);
				GameObject wall = GameObject.Find("BlockerLvl3");
				wall.SetActive(false);
			}
		}
		
		else if (other.gameObject.tag == "PickUpl3") {
			other.gameObject.SetActive(false);
			Level3PickUpCount++;
			if(Level3PickUpCount == 55)
			{

				highScore = PlayerPrefs.GetFloat("highScore");				
				if(highScore > timer)
				{
					PlayerPrefs.SetFloat("highScore", timer); 
					PlayerPrefs.Save();
					highscoreText.text = "Highscore: " + string.Format("{0:N1}", PlayerPrefs.GetFloat("highScore"));
					
					countText.text = "YOU GOT THE HIGHSCORE! Congrats!";
				}
				else
				{					
					countText.text = "YOU WIN!";
				}
			}
		}
		//28
		else if(other.gameObject.tag == "Check Point")
		{
			other.gameObject.SetActive(false);
		}
		updateCountText ();
	}

	void updateCountText()
	{
		countText.text = "Count: " + (Level1PickUpCount + Level2PickUpCount);
	}
	void updateTimerText()
	{
		timerText.text = "Time: " +string.Format("{0:N1}", timer);
	}


}
