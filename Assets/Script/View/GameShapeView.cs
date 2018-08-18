using UnityEngine;
using System.Collections;


public class ShapeAnimationName
{
	private ShapeAnimationName(string value) { Value = value; }

    public string Value { get; set; }

	public static ShapeAnimationName SPAWINING_ANIMATION { get { return new ShapeAnimationName("SpawnAnimation"); } }
	public static ShapeAnimationName SPAWN_DELAY { get { return new ShapeAnimationName("SpawnDelay"); } }
}

public class GameShapeView : MonoBehaviour
{
	GameShape shape;

	Animator animator;
    // Use this for initialization
    void Start()
	{

    }

    // Update is called once per frame
    void Update()
    {
        if(shape.State == GameShapeState.SPAWNING) {
			Debug.Log(!animator.GetCurrentAnimatorStateInfo(0).IsName(ShapeAnimationName.SPAWINING_ANIMATION.Value) && !animator.GetCurrentAnimatorStateInfo(0).IsName(ShapeAnimationName.SPAWN_DELAY.Value));
			if(!animator.GetCurrentAnimatorStateInfo(0).IsName(ShapeAnimationName.SPAWINING_ANIMATION.Value) && !animator.GetCurrentAnimatorStateInfo(0).IsName(ShapeAnimationName.SPAWN_DELAY.Value)) {
				shape.UpdateState(GameShapeState.MOVING);
				Debug.Log("got inside");
			}
        }
    }

	private void OnPositionUpdated(Vector3 newPosition) {
		gameObject.transform.position = newPosition;
	}
	private void OnGameShapeFinished(GameShape shape, Vector3 newPosition, bool isCorrect) {
		gameObject.transform.position = newPosition;
        shape.RemoveOnShapeFinished(OnGameShapeFinished);
        shape.RemoveOnPositionUpdated(OnPositionUpdated);
		//Destroy(gameObject);
	}

	private void InitShapeView(GameShape gameShape) {
		shape = gameShape;
		animator = gameObject.GetComponent<Animator>();
		gameShape.RegisterOnPositionUpdated(OnPositionUpdated);
		gameShape.RegisterOnShapeFinished(OnGameShapeFinished);
	}
    
	public static void CreateShape(GameObject shapePrefab, GameShape shape, GameObject parent) {
		GameObject shapeObj = Instantiate(shapePrefab, shape.Position, Quaternion.identity);
		shapeObj.transform.SetParent(parent.transform);
		GameShapeView shapeView = shapeObj.AddComponent<GameShapeView>();
		shapeView.InitShapeView(shape);
	}
}
