using JMiles42;
using JMiles42.Grid;
using JMiles42.Utilities.Enums;
using UnityEngine;

public abstract class BasePlaceable: JMilesScriptableObject, IPlaceable
{
	public abstract void OnPlacementStart(GridPosition gridPos, Vector3 worldPos);
	public abstract void OnPlacement(GridPosition gridPos, Vector3 worldPos);
	public abstract void OnPlacementCancel(GridPosition gridPos, Vector3 worldPos);
	public abstract void OnPlacementUpdate(GridPosition gridPos, Vector3 worldPos);
	public abstract void OnPlacementRotate(Direction_LR Rotate);
}