using UnityEngine;
using System.Collections;

public class GameElement : MonoBehaviour
{
    private Application _application = null;
    private Application Application { get { return _application ?? (_application = FindObjectOfType<Application>());}}
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
        Debug.Log(_application != null);
	}
}
