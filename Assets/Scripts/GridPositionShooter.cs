using JMiles42;
using JMiles42.Grid;
using JMiles42.MeshHelpers;
using UnityEditor;
using UnityEngine;

public class GridPositionShooter: JMilesBehavior
{
	public int ViewRange = 15;

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

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		var gP = HandleUtility.GUIPointToWorldRay(new Vector2(Screen.width/2,Screen.height/2)).GetGridPosition();
		var mP = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).GetGridPosition();
		var cP = GridPosition.Zero;
		var col = Gizmos.color;
		for(var x = -ViewRange; x < ViewRange; x++)
		{
			for(var y = -ViewRange; y < ViewRange; y++)
			{
				var gridPos = new GridPosition(x + gP.X, y + gP.Y);
				if(gridPos == mP)
				{
					Gizmos.color = Color.magenta;
				}
				else if(gridPos == cP)
				{
					Gizmos.color = Color.green;
				}
				Gizmos.DrawCube(gridPos.WorldPosition, Vector3.one * 0.95f);
				Gizmos.color = col;
			}
		}
	}
#endif
}