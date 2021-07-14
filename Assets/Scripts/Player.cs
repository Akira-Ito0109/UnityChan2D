using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public float speed = 4f;
	public float jumpPower = 700; 
	public LayerMask groundLayer; 
								
	public GameObject mainCamera;
	public GameObject bullet;
	public LifeScript lifeScript;
	private Rigidbody2D rb2D;
	private Animator anim;
	private bool isGrounded;
	private Renderer rend;
	private bool gameClear = false;
	public Text clearText;

	void Start()
	{
		anim = GetComponent<Animator>();
		rb2D = GetComponent<Rigidbody2D>();
		rend = GetComponent<Renderer>();
	}
	void Update()
	{
		isGrounded = Physics2D.Linecast(
		transform.position + transform.up * 1,
		transform.position - transform.up * 0.05f,
		groundLayer);
		if (!gameClear)
		{
			if (Input.GetKeyDown("space"))
			{
				if (isGrounded)
				{
					anim.SetBool("Dash", false);
					anim.SetTrigger("Jump");
					isGrounded = false;
					rb2D.AddForce(Vector2.up * jumpPower);
				}
			}
		}
		float velY = rb2D.velocity.y;
		bool isJumping = velY > 0.1f ? true : false;
		bool isFalling = velY < -0.1f ? true : false;
		anim.SetBool("isJumping", isJumping);
		anim.SetBool("isFalling", isFalling);
		if (!gameClear)
		{
			if (Input.GetKeyDown("left ctrl"))
			{
				anim.SetTrigger("Shot");
				Instantiate(bullet, transform.position + new Vector3(0f, 1.2f, 0f), transform.rotation);
			}
			if (gameObject.transform.position.y < Camera.main.transform.position.y - 8)
			{
				lifeScript.GameOver();
			}
		}
	}

	void FixedUpdate()
	{
		if (!gameClear)
		{
			float x = Input.GetAxisRaw("Horizontal");
		if (x != 0)
		{
			rb2D.velocity = new Vector2(x * speed, rb2D.velocity.y);
			Vector2 temp = transform.localScale;
			temp.x = x;
			transform.localScale = temp;
			anim.SetBool("Dash", true);
			if (transform.position.x > mainCamera.transform.position.x - 4)
			{
				Vector3 cameraPos = mainCamera.transform.position;
				cameraPos.x = transform.position.x + 4;
				mainCamera.transform.position = cameraPos;
			}
			Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
			Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
			Vector2 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x, min.x + 0.5f, max.x);
			transform.position = pos;
		}
		else
		{
			rb2D.velocity = new Vector2(0, rb2D.velocity.y);
			anim.SetBool("Dash", false);
		}
		}
		else
		{
			clearText.enabled = true;
			anim.SetBool("Dash", true);
			rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
			Invoke("CallTitle", 5);
		}
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy")
		{
			StartCoroutine("Damage");
		}
	}

	IEnumerator Damage()
	{
		gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
		int count = 10;
		while (count > 0)
		{
			rend.material.color = new Color(1, 1, 1, 0);
			yield return new WaitForSeconds(0.05f);
			rend.material.color = new Color(1, 1, 1, 1);
			yield return new WaitForSeconds(0.05f);
			count--;
		}
		gameObject.layer = LayerMask.NameToLayer("Player");
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "ClearZone")
		{
			gameClear = true;
		}
	}

	void CallTitle()
	{
		SceneManager.LoadScene("Title");
	}
}