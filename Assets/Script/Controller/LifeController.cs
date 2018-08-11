using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
	public static LifeController Instance { get; private set; }
	public int startLifes = 3;
	public int maximumTime = 20;
	private float currentTime;
	private int currentLifes;
	private int currentScore;

	private static Action<int> OnScoreUpdated;
	private static Action<int> OnLifeUpdated;
    // Use this for initialization
    void Start()
    {
		if(!Instance) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
		Init();
    }
	private void OnDestroy()
	{
		Reset();
	}
	public void Reset()
	{
		OnScoreUpdated = null;
		OnLifeUpdated = null;
	}

	private void Init()
	{
		currentLifes = startLifes;
		currentTime = maximumTime;
	}

	// Update is called once per frame
	void Update()
    {
		currentTime -= Time.deltaTime;
		CheckTimeOut();
    }

	public void OnShapeFinished(GameShape shape, bool isCorrect) {
		if(!isCorrect) {
			Penality(shape);
		} else {
			Award(shape);
		}
	}
	public int GetCurrentTime() {
		return Mathf.CeilToInt(currentTime);
	}
	public static int RegisterOnScoreUpdated(Action<int> action) {
		LifeController.OnScoreUpdated += action;
        if (LifeController.Instance)
        {
			return LifeController.Instance.currentScore;
        }
        return 0;
	}
	public static int RegisterOnLifeUpdated(Action<int> action) {
		LifeController.OnLifeUpdated += action;
		if(LifeController.Instance) {
			return LifeController.Instance.currentLifes;
        }
		return 0;
	}
	private void Award(GameShape shape) {
		currentScore++;
		if(OnScoreUpdated != null) {
			OnScoreUpdated(currentScore);
		}
	}
	private void Penality(GameShape shape) {
		currentLifes--;
		if(OnLifeUpdated != null) {
			OnLifeUpdated(currentLifes);
		}
		CheckDefeatCondition();
	}

	private void CheckDefeatCondition() {
		if(currentLifes <= 0) {
			OnDefeat();	
		}
	}
	private void CheckTimeOut() {
		if(currentTime <= 0) {
			OnDefeat();
		}
	}

	private void OnDefeat() {
		SceneManager.LoadScene("Game");
	}
}
