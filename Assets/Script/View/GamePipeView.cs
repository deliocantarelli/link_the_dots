using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GamePipeView : EventTrigger
{   
	public GamePlumbingController gamePlumbingController;
    
    public GameObject pipePrefab;

    //erase origin, correct updatePipe as it is just temporary
	private Vector3 origin;
	private Vector3 last;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
	private void InitView(GamePipe pipe) {
		pipe.RegisterOnPipeUpdated(OnPipeUpdated);
		OnPipeUpdated(pipe);
	}
	private void OnPipeUpdated(GamePipe pipe) {
		origin = pipe.StartPoint.SpawnPosition;  //remember to take this off
		last = pipe.CurrentEnd.Position;
		UpdateSize(pipe);
	}

	private void UpdateSize(GamePipe pipe) {
		Vector3 endPos = pipe.CurrentEnd.Position;
		Vector3 startPos = pipe.StartPoint.SpawnPosition;
		endPos = new Vector3(endPos.x, endPos.y);
		startPos = new Vector3(startPos.x, startPos.y);
		float size = Vector3.Distance(startPos, endPos);
		var angleDeg = Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg;
        RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
        rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
		rectTrans.Rotate(0, 0, angleDeg+90);
	}

	public static void CreatePipe(GameObject pipePrefab, GamePipe pipeDef, GameObject parent) {
		Vector3 position = pipeDef.StartPoint.SpawnPosition;
		GameObject newPipeObject = Instantiate(pipePrefab, position, Quaternion.identity);
		newPipeObject.transform.SetParent(parent.transform);
        GamePipeView component = newPipeObject.AddComponent<GamePipeView>();
		component.InitView(pipeDef);
	}

	private void UpdatePipe(Vector3 start, Vector3 position) {
		Vector3 endPos = new Vector3(position.x, position.y);
		Vector3 startPos = new Vector3(start.x, start.y);
        float size = Vector3.Distance(startPos, endPos);
        var angleDeg = Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg;
        RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
        rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);

		rectTrans.SetPositionAndRotation(rectTrans.position, Quaternion.Euler(0,0,angleDeg + 90));
        //rectTrans.Rotate(0, 0, angleDeg + 90);
	}
    
	public override void OnDrag (PointerEventData eventData)
    {
		Vector3 position = Camera.main.ScreenToWorldPoint(eventData.position);
        UpdatePipe(origin, position);
    }

    public override void OnEndDrag (PointerEventData eventData) {
        List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);
		bool hasFound = false;
		foreach(RaycastResult result in results) {
			GamePipeEndView pipeEnd = result.gameObject.GetComponent<GamePipeEndView>();
			Debug.Log(result.gameObject.name);
			if (pipeEnd != null){
				Vector3 newPosition = pipeEnd.gameObject.transform.position;
                UpdatePipe(origin, newPosition);
				last = newPosition;
				hasFound = true;
				break;
			}
		}
		if(!hasFound) {
			UpdatePipe(origin, last);
        }
	}
    
	public Vector3 GetPercentualPosition(float percentual) {
		return Vector3.Lerp(origin, last, percentual);
	}
}
