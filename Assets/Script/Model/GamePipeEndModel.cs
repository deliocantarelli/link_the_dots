using UnityEngine;
using System.Collections;

public class GamePipeEndModel : MonoBehaviour
{
	GamePipeEnd pipeEnd;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
	void SetPipeEnd(GameShapeType type, Vector3 position) {
		pipeEnd = new GamePipeEnd(type, position);
	}
    

  //  public static GameObject CreatePipeEnd(GameShapeType type, Vector3 position)
    //{
  //      GameObject gameObject = new GameObject("pipeEnd");
		//GamePipeEndModel component = gameObject.AddComponent<GamePipeEndModel>();
        //component.setPipeEnd(type, position);
        //return gameObject;
    //}
}
