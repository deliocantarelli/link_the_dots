using UnityEngine;
using System.Collections;

public class GameConfig : MonoBehaviour
{
	private readonly string[] spawners;
    
	public GameConfig()
    {
		this.spawners = new string[1];
		this.spawners[0] = "test";
    }
}
