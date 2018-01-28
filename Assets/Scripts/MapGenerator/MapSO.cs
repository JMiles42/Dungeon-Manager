using JMiles42.ScriptableObjects.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MapGen/Map")]
public class MapSO: GenericScriptableObject<Map>
{
	public GeneratorSettingsSO Settings;
}