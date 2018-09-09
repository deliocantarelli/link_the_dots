using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GamePipeShadowView : MonoBehaviour
{
	public void InitView(GameShape shape, GamePipe pipe)
	{
		shape.RegisterOnStateChanged(OnShapeStateChanged);
		UpdatePipe(pipe.StartPoint, pipe.CurrentEnd);
	}
	public static GamePipeShadowView CreateShadow(GameObject shadowPrefab, GamePipe pipeDef, GameShape shape, GameObject parent)
    {
        Vector3 position = pipeDef.StartPoint;
		GameObject newPipeObject = Instantiate(shadowPrefab, position, Quaternion.identity);
        newPipeObject.transform.SetParent(parent.transform);
		GamePipeShadowView component = newPipeObject.AddComponent<GamePipeShadowView>();
		component.InitView(shape, pipeDef);
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
	private void OnShapeStateChanged(GameShape shape) {
		if(shape.State == GameShapeState.FINISHED) {
			Destroy(gameObject);
		}
	}

}
