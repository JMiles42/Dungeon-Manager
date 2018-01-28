using JMiles42.AdvVar.RuntimeRef;
using JMiles42.Grid;
using JMiles42.Utilities.Enums;
using UnityEngine;

[CreateAssetMenu]
public class GridTilePlaceable: BasePlaceable
{
	public TransformRTRef GridFolder;
	private GridTile placingTile;
	public GridTile Prefab;

	private void CheckTile(GridPosition gridPos)
	{
		if(placingTile == null)
			placingTile = Instantiate(Prefab, gridPos, Quaternion.identity, GridFolder.Reference);
	}

	public override void OnPlacementStart(GridPosition gridPos, Vector3 worldPos)
	{
		CheckTile(gridPos);
		placingTile.gameObject.SetActive(true);
	}

	public override void OnPlacementConfirm(GridPosition gridPos, Vector3 worldPos)
	{
		CheckTile(gridPos);
		if(placingTile)
		{
			placingTile.Position = gridPos;
			placingTile.gameObject.SetActive(true);
		}
		placingTile = null;
		PlaceableManager.StopPlacing();
	}

	public override void OnPlacementCancel()
	{
		if(placingTile)
			placingTile.gameObject.SetActive(false);
	}

	public override void OnPlacementUpdate(GridPosition gridPos, Vector3 worldPos)
	{
		if(placingTile)
			placingTile.Position = gridPos;
	}

	public override void OnPlacementRotate(Direction_LR dir)
	{
		if(placingTile)
			placingTile.transform.Rotate(dir);
	}
}