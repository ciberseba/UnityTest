using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public float speed = 10f;
	public float jumpHeigth = 10f;

	private Rigidbody2D rb2d;
	private CircleCollider2D groundCollider;
	private float moveX = 0f;
	private float moveY = 0f;

	// Use this for initialization
	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D> ();
		groundCollider = GetComponentInChildren<CircleCollider2D> ();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		moveX = 0;
		moveY = 0;

		if(Input.GetButtonDown ("Jump"))
			moveY = jumpHeigth;
		
		if (Input.GetAxisRaw ("Horizontal") < 0)
			transform.position += Vector3.left * speed * Time.deltaTime;

		if (Input.GetAxisRaw ("Horizontal") > 0)
			transform.position += Vector3.right * speed * Time.deltaTime;

		//rb2d.AddForce (new Vector2 (moveX, 0));
		rb2d.AddForce (new Vector2 (0, moveY), ForceMode2D.Impulse);
	}
}
