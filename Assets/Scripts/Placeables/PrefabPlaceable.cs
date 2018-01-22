using JMiles42.Grid;
using JMiles42.Utilities.Enums;
using UnityEngine;

public class PrefabPlaceable: BasePlaceable
{
	public GameObject Prefab;
	private GameObject prefabInstance;

	public override void OnPlacementStart(GridPosition gridPos, Vector3 worldPos)
	{
		if(prefabInstance == null)
			prefabInstance = Instantiate(Prefab, worldPos, Quaternion.identity);
		else
			prefabInstance.SetActive(true);
	}

	public override void OnPlacement(GridPosition gridPos, Vector3 worldPos)
	{
		if(prefabInstance)
		{
			prefabInstance.SetActive(true);
			prefabInstance.transform.position = gridPos.WorldPosition;
			prefabInstance = null;
		}
	}

	public override void OnPlacementCancel(GridPosition gridPos, Vector3 worldPos)
	{
		if(prefabInstance)
			prefabInstance.SetActive(false);
	}

	public override void OnPlacementUpdate(GridPosition gridPos, Vector3 worldPos)
	{
		if(prefabInstance)
		{
			prefabInstance.transform.position = worldPos;
		}
	}

	public override void OnPlacementRotate(Direction_LR Rotate)
	{
		if(prefabInstance)
		{
			
		}
	}
}