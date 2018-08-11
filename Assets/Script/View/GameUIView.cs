using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIView : MonoBehaviour
{
	public Text scoreText;
	public Text lifeText;
	public Text timeText;
    // Use this for initialization
    void Start()
    {
		OnLifeUpdated(LifeController.RegisterOnLifeUpdated(OnLifeUpdated));
		OnScoreUpdated(LifeController.RegisterOnScoreUpdated(OnScoreUpdated));
    }

    // Update is called once per frame
    void Update()
    {
		UpdateTimer();
    }
	void UpdateTimer() {
		timeText.text = "" + LifeController.Instance.GetCurrentTime();
	}
	void OnLifeUpdated(int lifes) {
		lifeText.text = "" + lifes;
	}
	void OnScoreUpdated(int score) {
		scoreText.text = "" + score;
	}

}
