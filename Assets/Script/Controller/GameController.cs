using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
	public GameShapeSpawnerController spawnerController;

	private void Start()
	{
		spawnerController.RegisterOnShapeCretedCB(OnShapeCreated);
	}

	private void OnShapeCreated(GameShape shape) {
		Debug.Log("Shape created successfully");
	}


}
