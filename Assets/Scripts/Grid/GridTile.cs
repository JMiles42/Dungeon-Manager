using JMiles42;
using JMiles42.Grid;

public class GridTile: JMilesBehavior
{
	public GridPosition GridPosition;
	public GridTileList List;

	private void OnEnable()
	{
		List?.Add(this);
		GridPosition = Position;
	}

	private void OnDisable()
	{
		List?.Remove(this);
	}
}