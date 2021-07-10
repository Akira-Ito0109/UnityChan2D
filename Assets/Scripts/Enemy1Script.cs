using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
	Rigidbody2D rigidbody2D;
	public int speed = -3;
	public GameObject explosion;
	public GameObject item;
	public int attackPoint = 10;
	private LifeScript lifeScript;
	void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		lifeScript = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();

	}

	void Update()
	{
		rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D col)
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
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "UnityChan")
		{
			lifeScript.LifeDown(attackPoint);
		}
	}
}
