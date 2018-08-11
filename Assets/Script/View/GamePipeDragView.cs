using UnityEngine;

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

    public void StartPipeDrag(GamePipe pipe) {
		startPos = pipe.StartPoint;
		gameObject.transform.position = startPos;
		gameObject.SetActive(true);
	}
	public void StopPipeDrag() {
		gameObject.SetActive(false);
	}

    public void UpdatePipeDrag(Vector3 position)
    {
        Vector3 endPos = new Vector3(position.x, position.y);
        float size = Vector3.Distance(startPos, endPos);
        var angleDeg = Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg;
        RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
        rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);

        rectTrans.SetPositionAndRotation(rectTrans.position, Quaternion.Euler(0, 0, angleDeg + 90));
    }
}
