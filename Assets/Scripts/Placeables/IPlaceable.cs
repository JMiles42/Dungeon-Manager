using JMiles42.Grid;
using UnityEngine;

public interface IPlaceable
{
	void OnPlacementStart(GridPosition gridPos, Vector3 worldPos);
	void OnPlacement(GridPosition gridPos, Vector3 worldPos);
	void OnPlacementCancel(GridPosition gridPos, Vector3 worldPos);
	void OnPlacementUpdate(GridPosition gridPos, Vector3 worldPos);
}
