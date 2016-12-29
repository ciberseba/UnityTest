using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	
	public float speed = 10f;
	public float jumpHeigth = 10f;

	private Rigidbody2D rb2d;
	private CircleCollider2D groundCollider;
	private float moveX = 0f;
	private float moveY = 0f;
	private int layer;

	// Use this for initialization
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		groundCollider = GetComponentInChildren<CircleCollider2D>();
		layer = LayerMask.NameToLayer("Player");
	}

	void Update()
	{
		// El método getbuttondown/up debe ser llamado aquí, por como funciona internamente

		if (Input.GetButtonDown("Jump") && isReallyTouchingGround())
			moveY = jumpHeigth;
		if (Input.GetButtonUp("Jump"))
			cutJump();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		moveX = 0;
		
		if (Input.GetAxisRaw("Horizontal") < 0)
			transform.position += Vector3.left * speed * Time.deltaTime;

		if (Input.GetAxisRaw("Horizontal") > 0)
			transform.position += Vector3.right * speed * Time.deltaTime;

		//rb2d.AddForce (new Vector2 (moveX, 0));
		rb2d.AddForce(new Vector2(0, moveY), ForceMode2D.Impulse);

		moveY = 0;
	}

	// Forma simple
	private bool isTouchingGround()
	{
		return groundCollider.IsTouchingLayers();			
	}

	// Forma compleja, pero más exacta. Falta un pequeño detalle: chequear que se colisione con el suelo, y no
	// con la pared (se debe utilizar el hit para determinar el angulo, puede ser haciendo un raycast hacia abajo)
	private bool isReallyTouchingGround()
	{
		RaycastHit2D hit;
		Vector2 pos = transform.position;
		hit = Physics2D.CircleCast(pos, 0.23f, Vector2.down, 0.8f, ~(1 << layer));
		if (hit.collider != null) {
			#if DEBUG
			Vector3 p1 = new Vector3(hit.point.x, hit.point.y + 1);
			Vector3 p2 = new Vector3(hit.point.x, hit.point.y - 1);
			Vector3 p3 = new Vector3(hit.point.x + 1, hit.point.y);
			Vector3 p4 = new Vector3(hit.point.x - 1, hit.point.y);
			Debug.DrawLine(p1, p2, Color.red, 1);
			Debug.DrawLine(p3, p4, Color.red, 1);
			Debug.Log("ray hit -> x : " + hit.point.x + " y: " + hit.point.y + " - Name: " + hit.collider.name);
			#endif

			return true;
		}
		else {
			return false;
		}
	}

	private void cutJump()
	{
		Vector2 vel = rb2d.velocity;
		if (vel.y > 0f)
			vel.y = 0f;
		rb2d.velocity = vel;
	}
}
