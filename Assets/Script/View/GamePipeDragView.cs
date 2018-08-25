using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePipeDragView : MonoBehaviour
{
	private Vector3 startPos;
    void Start()
    {

    }

	public static GamePipeDragView CreatePipeDrag(GameObject pipeDragPrefab, GameObject parent)
    {
		GameObject newPipeDragObject = Instantiate(pipeDragPrefab, Vector3.zero, Quaternion.identity);
        newPipeDragObject.transform.SetParent(parent.transform);
        GamePipeDragView component = newPipeDragObject.AddComponent<GamePipeDragView>();
		component.gameObject.SetActive(false);
		return component;
    }

	public void StartPipeDrag(Vector3 startPos) {
		gameObject.transform.position = startPos;
		gameObject.SetActive(true);
	}
	public void StopPipeDrag() {
		gameObject.SetActive(false);
	}

	public void UpdatePipeDrag(Vector3 startPos, Vector3 position)
    {
        if (!gameObject.activeSelf)
        {
			StartPipeDrag(startPos);
        }

        Vector3 endPos = new Vector3(position.x, position.y);
        float size = Vector3.Distance(startPos, endPos);
        var angleDeg = Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg;
        RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
        rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);

        rectTrans.SetPositionAndRotation(rectTrans.position, Quaternion.Euler(0, 0, angleDeg + 90));
	}
	public GamePipeEndView FinishPipeDrag(PointerEventData eventData) {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
		GamePipeEndView pipeEndResult = null;
        foreach (RaycastResult result in results)
        {
            GamePipeEndView pipeEnd = result.gameObject.GetComponent<GamePipeEndView>();
            if (pipeEnd != null)
            {
				pipeEndResult = pipeEnd;
                break;
            }
        }
        if (gameObject.activeSelf)
        {
            StopPipeDrag();
        }
		return pipeEndResult;
	}
}
