using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
	public float speed;

	private int count;
	public Text countText;

	void Start()
	{
		count = 0;
		updateCountText ();
	}

	void FixedUpdate()
	{
		float moveHor = Input.GetAxis ("Horizontal");
		float MoveVert = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHor,0.0f,MoveVert);

		rigidbody.AddForce (movement * speed * Time.deltaTime);


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
