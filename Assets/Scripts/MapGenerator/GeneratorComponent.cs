using JMiles42;

public class GeneratorComponent: JMilesBehavior
{
	public MapSO Map;
	public GeneratorSettingsSO Settings;

	public void Generate()
	{
		Map.Data = null;
		Map.Data = Generator.GenerateMap(Settings);
	}
}