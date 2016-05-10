﻿using UnityEngine;
using System.Collections;

public class GameOverHandler : MonoBehaviour {

	public GameData data;
	public GameObject HUD;
	public SceneTransition sceneManager;
	public PlayerContainer annie;
	public PlayerContainer emil;
	public MusicManager music;

	public void Update() {
		//Check for Game Over
		if (data.annieCurrentLife <= 0 && data.emilCurrentLife <= 0 && !data.isGameOver) {
			setGameOver();
		}
	}

	public void setGameOver() {
		data.isGameOver = true;
		HUD.SetActive (false);
		StartCoroutine(go());
	}

	IEnumerator go() {
		//Instantiate(Resources.Load ("Effects/GameOver"), gameOverSpawner.position, Quaternion.Euler(45,0,0));
		music.stopMusic();
		yield return sceneManager.fadeOut();
		Application.LoadLevel("GameOver");
	}
	
	public void Restart() {
		data.isGameOver = false;
		data.annieCurrentLife = data.annieMaxLife;
		data.annieCurrentEnergy = data.annieMaxEnergy;
		data.emilCurrentLife = data.emilMaxLife;
		data.emilCurrentEnergy = data.emilMaxEnergy;
		annie.revive();
		emil.revive();
		GameObject.FindGameObjectWithTag ("Player").transform.position = data.lastCheckpoint;
		sceneManager.gotoScene (data.sceneName, true, false);
		music.changeMusic(music.previousMusic);
		HUD.SetActive (true);
	}
}