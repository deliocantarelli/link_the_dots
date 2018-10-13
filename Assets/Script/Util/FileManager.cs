
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public static class FileManager
{
	public static T ReadJSONFile<T>(string fileName) {
		
		string filePath = Path.Combine(UnityEngine.Application.streamingAssetsPath, fileName);

		if(File.Exists(filePath)) {
			string stringJson = File.ReadAllText(filePath);

			T loadedFile = JsonUtility.FromJson<T>(stringJson);

			return loadedFile;
        }

		Debug.LogError("Could not load file " + fileName);

		return default(T);
	}
}