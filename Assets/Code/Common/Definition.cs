using UnityEngine;
using System.Collections;


public class Definition 
{
	/// <summary>
	/// 网格原点
	/// </summary>
	public static Vector3 GridOrigin = new Vector3(0,0,0);

	/// <summary>
	/// 网格宽度是10米
	/// </summary>
	public const float GridLength = 10f;

	/// <summary>
	/// 格子数是9*9
	/// </summary>
	public const int GridNum = 9;


	static public float CellSize()
	{
		return GridLength / GridNum;
	}


	#region 坐标位置转换工具

	/// <summary>
	/// 世界坐标转换成网格坐标，
	/// </summary>
	/// <returns>The grid position.</returns>
	/// <param name="worldPosition">World position.</param>
	/// <param name="sideWidth">Side width.</param>
	static public Vector3 WorldToGrid(Vector3 worldPosition)
	{

		float GridCellSize = GridLength / GridNum;

		float x = worldPosition.x - GridOrigin.x;
		float z = worldPosition.z - GridOrigin.z;

		int width = Mathf.FloorToInt(x / GridCellSize);
		int height = Mathf.FloorToInt(z / GridCellSize);

		Vector3 showPosition = new Vector3(width * GridCellSize + GridCellSize/2f, GridOrigin.y,height*GridCellSize+ GridCellSize/2f);
		return showPosition;
	}

	/// <summary>
	/// 世界坐标转换成网格坐标
	/// </summary>
	/// <returns>The grid position.</returns>
	/// <param name="worldPosition">World position.</param>
	/// <param name="sideWidth">Side width.</param>
	static public Vector3 GridToWorld(int x,int z)
	{
		float GridCellSize = GridLength / GridNum;
		Vector3 showPosition = new Vector3(x * GridCellSize + GridCellSize/2f, GridOrigin.y ,z*GridCellSize+ GridCellSize/2f);
		return showPosition;
	}

	static public SGridCoordinate GetNextCoorinate(SGridCoordinate curCoor,EDirection nextDir)
	{
		SGridCoordinate nextCoordinate = curCoor;
		nextCoordinate.Overstep = false;

		switch (nextDir) {
		case EDirection.Left:
			nextCoordinate.x -= 1;
			if (nextCoordinate.x < 0) 
			{nextCoordinate.Overstep = true;}
			break;

		case EDirection.Right:
			nextCoordinate.x += 1;
			if (nextCoordinate.x > Definition.GridNum - 1) 
			{nextCoordinate.Overstep = true;}
			break;

		case EDirection.Up:
			nextCoordinate.z += 1;
			if (nextCoordinate.z > Definition.GridNum - 1) 
			{nextCoordinate.Overstep = true;}

			break;
		case EDirection.Down:
			nextCoordinate.z -= 1;
			if (nextCoordinate.z < 0) 
			{nextCoordinate.Overstep = true;}
			break;
		}

		return nextCoordinate;
	}

	/// <summary>
	/// Gets the next position.
	/// 还原当前位置的高度.
	/// </summary>
	/// <returns>The next position.</returns>
	/// <param name="curPosition">Current position.</param>
	/// <param name="curCoor">Current coor.</param>
	/// <param name="nextDir">Next dir.</param>
	static public Vector3 GetNextPosition(Vector3 curPosition , SGridCoordinate curCoor,EDirection nextDir)
	{
		SGridCoordinate nextCoor = GetNextCoorinate(curCoor,nextDir);
		Vector3 nextPosition = GridToWorld(nextCoor.x,nextCoor.z);
		nextPosition.y = curPosition.y;

		return nextPosition;
	}

	#endregion

}
