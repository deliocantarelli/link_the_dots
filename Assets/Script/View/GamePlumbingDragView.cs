using UnityEngine;
using System.Collections;

public class GamePlumbingDragView : MonoBehaviour
{
	public GameObject pipeDragParent;
	public GameObject pipeDragPrefab;

#if UNITY_STANDALONE || UNITY_EDITOR
	private int maxTouchCount = 3;
#else
	private int maxTouchCount = 10;
#endif
	private ArrayList GamePipeDragViews;
	// Use this for initialization
	void Start()
	{
		GamePipeDragViews = new ArrayList(maxTouchCount);
		for (int i = 0; i < maxTouchCount; i++)
		{
			GamePipeDragView pipeDragView = GamePipeDragView.CreatePipeDrag(pipeDragPrefab, pipeDragParent);
			GamePipeDragViews.Add(pipeDragView);
		}
	}

	public GamePipeDragView GetPipeDrag(int id)
	{
#if UNITY_STANDALONE || UNITY_EDITOR
		if(id < 0 && id >= -3) {
			id += maxTouchCount;
#else
		if(id >=0 && id < maxTouchCount) {
#endif
			return GamePipeDragViews[id] as GamePipeDragView;
        }
		return null;
	}
}
