using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
	Rigidbody2D rb2D;
	public int speed = -3;
	public GameObject explosion;
	public int attackPoint = 10;
	public GameObject item;

	private LifeScript lifeScript;
	private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
	private bool _isRendered = false;

	void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
		lifeScript = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();
	}

	void Update()
	{
		if (_isRendered)
		{
			rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
		}
		if (gameObject.transform.position.y < Camera.main.transform.position.y - 8 ||
gameObject.transform.position.x < Camera.main.transform.position.x - 10)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (_isRendered)
		{
			if (col.tag == "Bullet")
			{
				Destroy(gameObject);
				Instantiate(explosion, transform.position, transform.rotation);
				if (Random.Range(0, 4) == 0)
				{
					Instantiate(item, transform.position, transform.rotation);
				}
			}
		}
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "UnityChan")
		{
			lifeScript.LifeDown(attackPoint);
		}
	}
	void OnWillRenderObject()
	{
		if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
		{
			_isRendered = true;
		}
	}
}