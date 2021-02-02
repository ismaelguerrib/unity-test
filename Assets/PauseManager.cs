using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ig_PauseManager : MonoBehaviour
{
	public static bool ig_gameIsPaused = false;

	public Text ig_pauseTxt;

	void Start()
	{
		ig_pauseTxt.gameObject.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			ig_PauseGame();
		}
	}

	void ig_PauseGame()
	{
		ig_gameIsPaused = !ig_gameIsPaused;
		if (ig_gameIsPaused)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1;
		}
		ig_pauseTxt.gameObject.SetActive(ig_gameIsPaused);
	}


}
