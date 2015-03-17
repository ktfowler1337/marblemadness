using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
	public float speed;

	private int count;
	public Text countText;
	private float OldyLocation = 0;

	void Start()
	{
		count = 0;
		updateCountText ();
	}

	void FixedUpdate()
	{
		float moveHor = Input.GetAxis ("Horizontal");
		float MoveVert = Input.GetAxis("Vertical");
		float jumpers = 0;

		float diff = GetComponent<Rigidbody> ().position.y - OldyLocation;
		float diff2 = OldyLocation - GetComponent<Rigidbody> ().position.y;
		if (Input.GetKeyDown (KeyCode.Space) && diff < 0.0001f && diff2 < 0.0001f) {
			jumpers = 20.0f;
		}

		Vector3 movement = new Vector3(moveHor, jumpers ,MoveVert);

		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
		OldyLocation = GetComponent<Rigidbody>().position.y;

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PickUp") {
			other.gameObject.SetActive(false);
			count++;
			updateCountText();
		}
			
	}

	void updateCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if(count >= 11)
			countText.text = "You Win!!";
	}

}
