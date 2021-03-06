﻿using UnityEngine;

public class BattleMenuController : SSController
{
    public void GoBattleFight()
    {
        SSSceneManager.Instance.Screen("DemoBattle");
    }

	public void GoMainPage()
	{
		SSSceneManager.Instance.Screen("MainMenu");
	}

	void OnEnable()
	{
		GetComponent<AudioSource>().Play();
	}

	void OnDisable()
	{
		GetComponent<AudioSource>().Stop();
	}
}
