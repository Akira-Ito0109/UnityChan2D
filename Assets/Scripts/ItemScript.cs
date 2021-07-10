using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
	public int healPoint = 20;
	private LifeScript lifeScript;

	void Start()
	{
		lifeScript = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "UnityChan")
		{
			lifeScript.LifeUp(healPoint);
			Destroy(gameObject);
		}
	}
}