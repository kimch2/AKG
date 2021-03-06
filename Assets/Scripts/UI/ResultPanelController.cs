﻿using UnityEngine;
using UnityEngine.UI;

public class ResultPanelController : MonoBehaviour 
{
	public bool Victory 
	{
		set 
		{
			text.text = (value) ? "You Win" : "You Lose";
		}
	}

	private Text text;

	public void OnRestartClicked()
	{
		SSSceneManager.Instance.Screen("MainMenu");
		gameObject.SetActive(false);
    }

	void Awake()
	{
		text = transform.Find("Text").GetComponent<Text>();
	}
}
