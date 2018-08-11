using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
	public static LifeController Instance { get; private set; }
	public int startLifes = 3;
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
	}

	// Update is called once per frame
	void Update()
    {

    }

	public void OnShapeFinished(GameShape shape, bool isCorrect) {
		if(!isCorrect) {
			Penality(shape);
		} else {
			Award(shape);
		}
	}
	private void Award(GameShape shape) {
		currentScore++;
		Debug.Log("score: " + currentScore);
	}
	private void Penality(GameShape shape) {
		currentLifes--;
		Debug.Log("lifes: " + currentLifes);
		CheckDefeatCondition();
	}

	private void CheckDefeatCondition() {
		if(currentLifes <= 0) {
			OnDefeat();	
		}
	}

	private void OnDefeat() {
		SceneManager.LoadScene("Game");
	}
}
