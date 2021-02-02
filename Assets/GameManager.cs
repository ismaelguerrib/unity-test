using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public enum States
	{
		wait, play, levelup, dead
	}
	public static States state;

	int level;
	int score;
	int lives;

	public Text ig_levelTxt;
	public Text ig_scoreTxt;
	public Text ig_livesTxt;

	public Text ig_messageTxt;


	GameObject ig_player;
	public GameObject ig_ennemies; 
	public GameObject ig_explosion;


	Camera ig_camera;
	float height, width;


	public GameObject ig_waitToStart; // panel





	void Start()
	{


		ig_messageTxt.gameObject.SetActive(false);

		ig_player = GameObject.FindWithTag("Player");

		ig_camera = Camera.main;
		height = ig_camera.orthographicSize;
		width = height * ig_camera.aspect;


		ig_waitToStart.gameObject.SetActive(true);
		int highscore = PlayerPrefs.GetInt("highscore");
		if (highscore > 0)
		{
			ig_messageTxt.text = "A previous heroe of the rebel army made this highscore : " + highscore;
			ig_messageTxt.gameObject.SetActive(true);
		}

		state = States.wait;
	}

	public void ig_LaunchGame()
	{
		// interface
		ig_waitToStart.gameObject.SetActive(false);
		ig_messageTxt.gameObject.SetActive(false);
		// restaurer après une partie
		ig_player.SetActive(true);
		GameObject[] ig_ennemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject ig_enemy in ig_ennemies)
		{
			Destroy(ig_enemy);
		}
		// lancer une partie
		ig_InitGame();
		ig_LoadLevel();
		ig_UpdateTexts();
	}



	void ig_LoadLevel()
	{
		state = States.play;

		// instancier 3 ennemys (selon le niveau)
		// - savoir ce qu'est un ennemy (public ...)
		// - faire une boucle 1 à 3
		//       instancier un ennemy : dans les limites de l'écran
		for (int i = 0; i < 2 + level; i++)
		{
			float x = Random.Range(-width, width);
			float y = Random.Range(-height, height);
			Instantiate(ig_ennemies, new Vector2(x, y), Quaternion.identity);
		}
	}

	void ig_InitGame()
	{
		level = 1;
		score = 0;
		lives = 5;
	}

	void ig_UpdateTexts()
	{
		ig_levelTxt.text = "level: " + level;
		ig_scoreTxt.text = "score: " + score;
		ig_livesTxt.text = "lives: " + lives;
	}


	public void ig_AddScore(int points)
	{
		score += points;
		ig_UpdateTexts();
	}

	private void Update()
	{
		if (state == States.play)
		{
			ig_EndOfLevel();
		}
	}

	void ig_EndOfLevel()
	{
		GameObject[] ig_ennemies = GameObject.FindGameObjectsWithTag("Enemy");
		if (ig_ennemies.Length == 0)
		{
			StartCoroutine(ig_LevelUp());
		}
	}

	IEnumerator ig_LevelUp()
	{
		state = States.levelup;
		// afficher message "level up"
		ig_messageTxt.text = "More ennemies are comming ! ";
		ig_messageTxt.gameObject.SetActive(true);
		// marquer une pause
		yield return new WaitForSecondsRealtime(3f);
		// cacher le message
		ig_messageTxt.gameObject.SetActive(false);
		level += 1;
		ig_LoadLevel();
		ig_UpdateTexts();
	}

	public void ig_KillPlayer()
	{
		StartCoroutine(ig_PlayerAgain());
	}

	IEnumerator ig_PlayerAgain()
	{
		state = States.dead;

		GameObject ig_explosionGo = Instantiate(ig_explosion, ig_player.transform.position, Quaternion.identity);

		lives -= 1;
		ig_player.SetActive(false);
		ig_UpdateTexts();
		if (lives <= 0)
		{
			yield return new WaitForSecondsRealtime(2f);
			Destroy(ig_explosionGo);
			ig_GameOver();
		}
		else
		{
			yield return new WaitForSecondsRealtime(3f);
			Destroy(ig_explosionGo);
			ig_player.SetActive(true);
			state = States.play;
		}
	}

	void ig_GameOver()
	{
		state = States.wait;

		int highscore = PlayerPrefs.GetInt("highscore");
		if (score > highscore)
		{
			PlayerPrefs.SetInt("highscore", score);
			ig_messageTxt.text = "Congrats champ, you are our new heroe ! You made a new higscore : " + score;
		}
		else
		{
			ig_messageTxt.text = "You loose, the Empire is too strong...\n the highscore is still " + highscore;
		}

		

		ig_messageTxt.gameObject.SetActive(true);
		ig_waitToStart.gameObject.SetActive(true);
	}




}
