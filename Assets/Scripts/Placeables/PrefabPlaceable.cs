using JMiles42.Grid;
using JMiles42.Utilities.Enums;
using UnityEngine;

[CreateAssetMenu]
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

	public override void OnPlacementConfirm(GridPosition gridPos, Vector3 worldPos)
	{
		if(prefabInstance)
		{
			prefabInstance.SetActive(true);
			prefabInstance.transform.position = gridPos.WorldPosition;
			prefabInstance = null;
		}
	}

	public override void OnPlacementCancel()
	{
		if(prefabInstance)
			prefabInstance.SetActive(false);
	}

	public override void OnPlacementUpdate(GridPosition gridPos, Vector3 worldPos)
	{
		if(prefabInstance)
			prefabInstance.transform.position = gridPos.WorldPosition;
	}

	public override void OnPlacementRotate(Direction_LR dir)
	{
		if(prefabInstance)
			prefabInstance.transform.Rotate(dir);
	}
}