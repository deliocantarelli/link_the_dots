using UnityEngine;
using System.Collections;

public class GamePipeEndView : MonoBehaviour
{
	public GameShapeType EndType { get; private set; }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
	public void InitPipeEnd() {
	}
    public static void CreatePipeEnd(GameObject pipeEndPrefab, GameObject parent, GamePipeEnd pipeDef)
    {
		GameObject pipeEndObj = Instantiate(pipeEndPrefab, pipeDef.Position, Quaternion.identity);
		pipeEndObj.transform.SetParent(parent.transform);
		GamePipeEndView component = pipeEndObj.AddComponent<GamePipeEndView>();
		component.EndType = pipeDef.Type;
    }

}
