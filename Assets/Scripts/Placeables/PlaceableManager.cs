using JMiles42.AdvVar;
using JMiles42.Generics;
using JMiles42.Grid;
using JMiles42.Utilities;
using JMiles42.Utilities.Enums;
using UnityEngine;

public class PlaceableManager: Singleton<PlaceableManager>
{
	private static BasePlaceable placeable;
	public Vector3Reference InputPosition;

	public static void StartPlacing(PrefabPlaceable placer)
	{
		StopPlacing();
		placeable = placer;
		Vector3 mp;
		GridPosition gp;
		GetMpGp(out mp, out gp);
		placeable.OnPlacementStart(gp, mp);
	}

	public static void StopPlacing()
	{
		if(placeable != null)
		{
			placeable.OnPlacementCancel();
			placeable = null;
		}
	}

	private void Update()
	{
		if (!placeable)
			return;



		//HACK: Remove input pos update from here to allow touch
		InputPosition = Input.mousePosition;
		//Hack: Remove hardcoded camera ref
		Vector3 mp;
		GridPosition gp;
		GetMpGp(out mp, out gp);

		if (Input.GetKeyDown(KeyCode.Space))
			placeable.OnPlacementStart(gp, mp);
		if (Input.GetKeyDown(KeyCode.Mouse0))
			placeable.OnPlacementConfirm(gp, mp);
		if (Input.GetKeyDown(KeyCode.Mouse1))
			placeable.OnPlacementCancel();
		if (Input.GetKeyDown(KeyCode.A))
			placeable.OnPlacementRotate(Direction_LR.Left);
		if (Input.GetKeyDown(KeyCode.D))
			placeable.OnPlacementRotate(Direction_LR.Right);
		placeable.OnPlacementUpdate(gp, mp);
	}

	private static void GetMpGp(out Vector3 mp, out GridPosition gp)
	{
		var ray = Camera.main.ScreenPointToRay(Instance.InputPosition);
		mp = ray.GetPosOnY();
		gp = mp.GetGridPosition();
	}
}