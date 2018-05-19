using UnityEngine;
using System.Collections;
using System.IO;

public class MainController : MonoBehaviour
{
	public GameConfig config;
    // Use this for initialization
    void Start()
	{
        DontDestroyOnLoad(gameObject);
		config = new GameConfig();
    }

    // Update is called once per frame
    void Update()
    {

    }


}
