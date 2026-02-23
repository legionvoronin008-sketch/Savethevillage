using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
	private bool paused;
	public GameObject PausedScreen;
	public AudioSource click;
	public  void PauseGame()
	{
		if(paused)
		{
			Time.timeScale = 1;
			PausedScreen.SetActive(false);

		}
		else
		{
			Time.timeScale = 0;
			PausedScreen.SetActive(true);
		}
		paused = !paused;
	}
	public void Onclick()
	{
		click.Play();
	}


}
