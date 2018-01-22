using JMiles42;
using JMiles42.Grid;
using UnityEngine;

public abstract class BasePlaceable: JMilesScriptableObject, IPlaceable
{
	public void OnPlacementStart(GridPosition gridPos, Vector3 worldPos)
	{
		
	}

	public void OnPlacement(GridPosition gridPos, Vector3 worldPos)
	{
		
	}

	public void OnPlacementCancel(GridPosition gridPos, Vector3 worldPos)
	{
		
	}

	public void OnPlacementUpdate(GridPosition gridPos, Vector3 worldPos)
	{
		
	}
}