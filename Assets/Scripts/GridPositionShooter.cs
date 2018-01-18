using JMiles42;
using JMiles42.MeshHelpers;
using JMiles42.Grid;
using UnityEngine;

public class GridPositionShooter: JMilesBehavior
{
	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			var go = PrimitiveHelper.CreatePrimitive(PrimitiveType.Cube, true);
			var gP = Camera.main.ScreenPointToRay(Input.mousePosition).GetGridPosition();
			go.transform.position = gP;
			Debug.Log(gP);
		}
	}
}