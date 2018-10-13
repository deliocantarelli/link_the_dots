using UnityEngine;
using System.Collections;
using System;

public class GamePipeEndController : MonoBehaviour
{
	private GamePipeEnd[] pipeEnds;
	private Action<GamePipeEnd> onPipeEndAdded;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void SetEndPipeConfig(GameConfig gameConfig) {
      
		PipeEndConfig[] pipeEndConfigs = gameConfig.pipeEnds;
		pipeEnds = new GamePipeEnd[pipeEndConfigs.Length];

		for (int index = pipeEndConfigs.Length-1; index >= 0; index--)
        {
			PipeEndConfig config = pipeEndConfigs[index];
			pipeEnds[index] = new GamePipeEnd(config.shapeType, config.position);
        }

		if(onPipeEndAdded != null) {
			foreach(GamePipeEnd pipeEnd in pipeEnds) {
				onPipeEndAdded(pipeEnd);
            }
        }
	}
	public GamePipeEnd[] GetPipeEnds() {
		return pipeEnds;
	}
	public void RegisterOnPipeEndAdded(Action<GamePipeEnd> action) {
		onPipeEndAdded += action;
	}
}
