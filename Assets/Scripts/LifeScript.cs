using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeScript : MonoBehaviour
{
	RectTransform rt;
	public GameObject unityChan; 
	public GameObject explosion; 
	public Text gameOverText; 
	private bool gameOver = false; 
	void Start()
	{
		rt = GetComponent<RectTransform>();
	}
	void Update()
	{
		if (rt.sizeDelta.y <= 0)
		{
			if (gameOver == false)
			{
				Instantiate(explosion, unityChan.transform.position + new Vector3(0, 1, 0), unityChan.transform.rotation);
			}
			GameOver();
		}
		if (gameOver)
		{
			gameOverText.enabled = true;
			if (Input.GetMouseButtonDown(0))
			{
				SceneManager.LoadScene("Title");
			}
		}
	}

	public void LifeDown(int ap)
	{
		rt.sizeDelta -= new Vector2(0, ap);
	}
	public void LifeUp(int hp)
	{
		rt.sizeDelta += new Vector2(0, hp);
		if (rt.sizeDelta.y > 240f)
		{
			rt.sizeDelta = new Vector2(51f, 240f);
		}
	}
	public void GameOver()
	{
		gameOver = true;
		Destroy(unityChan);
	}
}