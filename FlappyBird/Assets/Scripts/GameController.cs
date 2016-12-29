using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController instance;
	public GameObject GameOverText;
	public Text MaxScoreText;
	public bool gameOver = false;
	public float scrollSpeed = -1.5f;
	public int score = 0;
	public Text scoreText;
	public int maxScore = 0;

	// Use this for initialization
	void Awake () 
	{
		if (instance == null) {
			instance = this;
		} else if (instance != null) 
		{
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		MaxScoreText.text = "Máximo: " + this.maxScore.ToString(); 

		if (score > maxScore) 
		{
			this.maxScore = score;
		}

		if (gameOver == true && Input.GetMouseButtonDown(0)) 
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void BirdScored()
	{
		if (gameOver) 
		{
			return;
		}
		score++;
		scoreText.text = "Puntos: " + score.ToString();
	}

	public void BirdDied()
	{
		GameOverText.SetActive (true);
		gameOver = true;
	}
}
