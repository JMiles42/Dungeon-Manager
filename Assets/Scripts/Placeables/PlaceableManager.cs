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
	private static bool StartedPlacingThisFrame;
	public CameraRTRef Camera;
	public Vector2Reference InputPosition;

	[SerializeField] [DisableEditing] private BasePlaceable placeable;

	public static bool Placing => Instance.placeable != null;

	public static void StartPlacing(BasePlaceable placer)
	{
		StopPlacing();
		Instance.placeable = placer;
		var mp = GetWorldPositionOfMouse();
		StartedPlacingThisFrame = true;
		Instance.placeable.OnPlacementStart(mp.GetGridPosition(), mp);
	}

	public static void StopPlacing()
	{
		if(Instance.placeable == null)
			return;
		Instance.placeable.OnPlacementCancel();
		Instance.placeable = null;
	}

	private void OnEnable()
	{
		GameplayInputManager.OnPrimaryClick += OnPrimaryClick;
	}

	private void OnDisable()
	{
		GameplayInputManager.OnPrimaryClick -= OnPrimaryClick;
	}

	private void OnPrimaryClick(Vector2 mousePos)
	{
		if(!placeable)
			return;
		if(StartedPlacingThisFrame)
			return;
		var mp = GetWorldPositionOfMouse();
		var gp = mp.GetGridPosition();
		placeable.OnPlacementConfirm(gp, mp);
	}

	private void Update()
	{
		if(!placeable)
			return;

		var mp = GetWorldPositionOfMouse();
		var gp = mp.GetGridPosition();
		if(Input.GetKeyDown(KeyCode.A))
			placeable.OnPlacementRotate(Direction_LR.Left);
		if(Input.GetKeyDown(KeyCode.D))
			placeable.OnPlacementRotate(Direction_LR.Right);
		placeable.OnPlacementUpdate(gp, mp);
	}

	private void LateUpdate()
	{
		StartedPlacingThisFrame = false;
	}

	private static Vector3 GetWorldPositionOfMouse()
	{
		var ray = Instance.Camera.Reference.ScreenPointToRay(Instance.InputPosition.Value);
		return ray.GetPosOnY();
	}
}