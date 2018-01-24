using JMiles42;
using UnityEngine;

public class PlaceAbleTester : JMilesBehavior
{
	public BasePlaceable placeable;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			PlaceableManager.StartPlacing(placeable);
		}
	}
}
