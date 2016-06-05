using UnityEngine;
using System.Collections;


public class Definition 
{
	/// <summary>
	/// 网格原点
	/// </summary>
	public static Vector3 GridOrigin = new Vector3(0,0,0);



	/// <summary>
	/// 单个网格长1米
	/// </summary>
	public const float CellSize = 1f;

	/// <summary>
	/// 角色可移动的格子数是9*9
	/// </summary>
	public const int GridWidthCount = 9;

	/// <summary>
	/// 角色可以移动的区域高度
	/// </summary>
	public const int GridPlayerHeightCount = 9;

	/// <summary>
	/// 总的网格是9 * 14；
	/// </summary>
	public const int GridHeightCount = 14;



	/// <summary>
	/// 敌人所属区域： 9 * 5
	/// </summary>
	/// <returns>The enemy height.</returns>
	static public int GridEnemyHeight()
	{
		return GridHeightCount - GridPlayerHeightCount;
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

		float x = worldPosition.x - GridOrigin.x;
		float z = worldPosition.z - GridOrigin.z;

		int width = Mathf.FloorToInt(x / CellSize);
		int height = Mathf.FloorToInt(z / CellSize);

		Vector3 showPosition = new Vector3(width * CellSize + CellSize/2f, GridOrigin.y,height*CellSize+ CellSize/2f);
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
		Vector3 showPosition = new Vector3(x * CellSize + CellSize/2f, GridOrigin.y ,z*CellSize+ CellSize/2f);
		return showPosition;
	}

	/// <summary>
	/// 获取区域类型
	/// </summary>
	/// <param name="coorX">Coor x.</param>
	/// <param name="coorZ">Coor z.</param>
	static public EAreaType GetAreaType(int coorX,int coorZ)
	{
		EAreaType aType = EAreaType.Player;
		if(coorX < 0 || coorX >= GridWidthCount || coorX < 0|| coorZ >= GridHeightCount)
		{
			//--界外.
			aType = EAreaType.Out;
		}
		else if(coorZ >= GridPlayerHeightCount)
		{
			aType  = EAreaType.Enemy;
		}

		return aType;
	}

	static public SGridCoordinate GetNextCoorinate(SGridCoordinate curCoor,EDirection nextDir)
	{
		SGridCoordinate nextCoor = curCoor;

		switch (nextDir) {
		case EDirection.Left:
			nextCoor.x -= 1;
			break;
		case EDirection.Right:
			nextCoor.x += 1;
			break;
		case EDirection.Up:
			nextCoor.z += 1;
			break;
		case EDirection.Down:
			nextCoor.z -= 1;
			break;
		}

		nextCoor.MAreaType = GetAreaType(nextCoor.x,nextCoor.z);

		return nextCoor;
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
