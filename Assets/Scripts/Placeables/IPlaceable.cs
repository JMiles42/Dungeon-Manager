using JMiles42.Grid;
using JMiles42.Utilities.Enums;
using UnityEngine;

public interface IPlaceable
{
	void OnPlacementStart(GridPosition gridPos, Vector3 worldPos);
	void OnPlacement(GridPosition gridPos, Vector3 worldPos);
	void OnPlacementCancel(GridPosition gridPos, Vector3 worldPos);
	void OnPlacementUpdate(GridPosition gridPos, Vector3 worldPos);
	void OnPlacementRotate(Direction_LR Rotate);
}
