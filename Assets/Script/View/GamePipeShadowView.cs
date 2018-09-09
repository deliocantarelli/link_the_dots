using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GamePipeShadowView : MonoBehaviour
{
	private GamePipe _pipe;

	public void InitView(GamePipe pipe)
	{
		_pipe = pipe;
		pipe.RegisterOnPipeUpdated(OnPipeStateChanged);
		UpdatePipe(pipe.StartPoint, pipe.CurrentEnd);
	}
	public static GamePipeShadowView CreateShadow(GameObject shadowPrefab, GamePipe pipeDef, GameObject parent)
    {
        Vector3 position = pipeDef.StartPoint;
		GameObject newPipeObject = Instantiate(shadowPrefab, position, Quaternion.identity);
        newPipeObject.transform.SetParent(parent.transform);
		GamePipeShadowView component = newPipeObject.AddComponent<GamePipeShadowView>();
		component.InitView(pipeDef);
        return component;
    }
	private void UpdatePipe(Vector3 start, Vector3 position)
    {
        Vector3 endPos = new Vector3(position.x, position.y);
        Vector3 startPos = new Vector3(start.x, start.y);
        float size = Vector3.Distance(startPos, endPos);
        var angleDeg = Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg;
        RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
        rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);

        rectTrans.SetPositionAndRotation(rectTrans.position, Quaternion.Euler(0, 0, angleDeg + 90));
    }
	private void OnPipeStateChanged(GamePipe pipe) {
		if(pipe.State == GamePipeState.FINISHED || pipe.State == GamePipeState.WRONG) {
			Destroy(gameObject);
		}
	}
	private void OnDestroy()
	{
		_pipe.RemoveOnPipeUpdated(OnPipeStateChanged);
	}
}
