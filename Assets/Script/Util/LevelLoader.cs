public static class LevelLoader
{   
	public static GameConfig LoadLevel(string fileName) {
		return FileManager.ReadJSONFile<GameConfigSerializable>("level/"+ fileName + ".json").ToGameConfig();
	}
}
