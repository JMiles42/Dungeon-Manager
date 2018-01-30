using JMiles42;

public class MapCreator: JMilesBehavior
{
	public MapSO Map;
	public MapSettingsSO MapSettings;

	void FixedUpdate()
	{
		MapSettings.Data.Seed = JMiles42.Maths.Random.RandomStrings.GetRandomString(8);
		GenerateMap();
	}

	private void Start()
	{
		GenerateMap();
	}

	public void GenerateMap()
	{
		MapGenerator.Generate(Map.Data, MapSettings.Data);
	}
}