using JMiles42.AdvVar;
using JMiles42.AdvVar.RuntimeRef;
using JMiles42.Attributes;
using JMiles42.Generics;
using JMiles42.Grid;
using JMiles42.Utilities;
using JMiles42.Utilities.Enums;
using UnityEngine;

public class PlaceableManager: Singleton<PlaceableManager>
{
	public CameraRTRef Camera;
	public Vector3Reference InputPosition;

	[SerializeField] [DisableEditing] private BasePlaceable placeable;

	public static void StartPlacing(BasePlaceable placer)
	{
		StopPlacing();
		Instance.placeable = placer;
		var mp = GetMpGp();
		Instance.placeable.OnPlacementStart(mp.GetGridPosition(), mp);
	}

	public static void StopPlacing()
	{
		if(Instance.placeable != null)
		{
			Instance.placeable.OnPlacementCancel();
			Instance.placeable = null;
		}
	}

	private void Update()
	{
		if(!placeable)
			return;
		var mp = GetMpGp();
		var gp = mp.GetGridPosition();
		if(Input.GetKeyDown(KeyCode.Space))
			placeable.OnPlacementStart(gp, mp);
		if(Input.GetKeyDown(KeyCode.Mouse0))
			placeable.OnPlacementConfirm(gp, mp);
		if(Input.GetKeyDown(KeyCode.Mouse1))
			placeable.OnPlacementCancel();
		if(Input.GetKeyDown(KeyCode.A))
			placeable.OnPlacementRotate(Direction_LR.Left);
		if(Input.GetKeyDown(KeyCode.D))
			placeable.OnPlacementRotate(Direction_LR.Right);
		placeable.OnPlacementUpdate(gp, mp);
	}

	private static Vector3 GetMpGp()
	{
		var ray = Instance.Camera.Reference.ScreenPointToRay(Instance.InputPosition);
		return ray.GetPosOnY();
	}
}