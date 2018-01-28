using JMiles42;
using UnityEngine;

public class GridPlacerManager: JMilesBehavior
{
	public GridTilePlaceable GridTilePlaceable;

	private void OnEnable()
	{
		GameplayInputManager.OnPrimaryClick += OnPrimaryClick;
	}

	private void OnPrimaryClick(Vector2 vector2)
	{
		if(!PlaceableManager.Placing)
			PlaceableManager.StartPlacing(GridTilePlaceable);
	}

	private void OnDisable()
	{
		GameplayInputManager.OnPrimaryClick -= OnPrimaryClick;
	}
}