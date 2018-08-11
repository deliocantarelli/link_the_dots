using UnityEngine;
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

	private void Award(GameShape shape) {
		currentScore++;
	}
	private void Penality(GameShape shape) {
		currentLifes--;
		CheckDefeatCondition();
	}

	private void CheckDefeatCondition() {
		if(currentLifes <= 0) {
			OnDefeat();	
		}
	}
	private void CheckTimeOut() {
		Debug.Log(GetCurrentTime());
		if(currentTime <= 0) {
			OnDefeat();
		}
	}

	private void OnDefeat() {
		SceneManager.LoadScene("Game");
	}
}
