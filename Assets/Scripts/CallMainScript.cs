﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallMainScript : MonoBehaviour
{
	IEnumerator Start()
	{
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene("Main");
	}
}
