using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GamePipeView : EventTrigger
{   
	public GamePlumbingController gamePlumbingController;
	public GamePlumbingDragView gamePlumbingDragView;
    
    public GameObject pipePrefab;
	public GameObject pipeDragPrefab;

	//erase origin, correct updatePipe as it is just temporary
	private GamePipe updatedPipe;
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
	public static void CreatePipe(GameObject pipePrefab, GamePipe pipeDef, GameObject parent, GamePlumbingController plumbingController, GamePlumbingDragView plumbingDragView) {
		Vector3 position = pipeDef.StartPoint;
		GameObject newPipeObject = Instantiate(pipePrefab, position, Quaternion.identity);
		newPipeObject.transform.SetParent(parent.transform);
		GamePipeView component = newPipeObject.AddComponent<GamePipeView>();
		component.InitView(pipeDef, plumbingController, plumbingDragView);
	}
	private void InitView(GamePipe pipe, GamePlumbingController plumbingController, GamePlumbingDragView plumbingDragView) {
        pipe.RegisterOnPipeUpdated(OnPipeUpdated);
		gamePlumbingDragView = plumbingDragView;
		gamePlumbingController = plumbingController;
        OnPipeUpdated(pipe);
    }
    private void OnPipeUpdated(GamePipe pipe) {
        updatedPipe = pipe;
        origin = pipe.StartPoint;  //remember to take this off
        last = pipe.CurrentEnd;
		UpdatePipe(origin, last);
    }
    private void UpdatePipeEnd(Vector3 position) {
        UpdatePipe(origin, position);
        gamePlumbingController.UpdatePipeEnd(updatedPipe, position);
    }

	private void UpdatePipe(Vector3 start, Vector3 position) {
		Vector3 endPos = new Vector3(position.x, position.y);
		Vector3 startPos = new Vector3(start.x, start.y);
        float size = Vector3.Distance(startPos, endPos);
        var angleDeg = Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg;
        RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
        rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);

		rectTrans.SetPositionAndRotation(rectTrans.position, Quaternion.Euler(0,0,angleDeg + 90));
	}
    
	public override void OnDrag (PointerEventData eventData)
    {
		Vector3 position = Camera.main.ScreenToWorldPoint(eventData.position);
		int id = eventData.pointerId;
		Debug.Log(id);
		GamePipeDragView dragView = gamePlumbingDragView.GetPipeDrag(id);
		if(!dragView.gameObject.activeSelf) {
			dragView.StartPipeDrag(updatedPipe);
		}
		dragView.UpdatePipeDrag(position);
    }

    public override void OnEndDrag (PointerEventData eventData) {
        List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);
		foreach(RaycastResult result in results) {
			GamePipeEndView pipeEnd = result.gameObject.GetComponent<GamePipeEndView>();
			if (pipeEnd != null){
				Vector3 newPosition = pipeEnd.gameObject.transform.position;
				UpdatePipeEnd(newPosition);
				break;
			}
		}
        int id = eventData.pointerId;
        GamePipeDragView dragView = gamePlumbingDragView.GetPipeDrag(id);
		if(dragView.gameObject.activeSelf) {
			dragView.StopPipeDrag();
		}
	}
    
}
