using UnityEngine;
using System.Collections;

/// <summary>
/// 格子的信息
/// </summary>
public class GridInfo  {

	public enum EState
	{
		Empty = 0,Waiting = 1, Occupation = 2
	}

	//当前占领--需要一个占领信息.
	public GameObject MOccupant;
	public EState MCurState = EState.Empty;
	public SGridCoordinate MCoordinate;

	public GridInfo (SGridCoordinate mCoordinate)
	{
		this.MCoordinate = mCoordinate;
	}
	

	//即将被占领，有球体向这个点飞来。
	//public bool MOccupantSoon;

	/// <summary>
	/// 开始占领，开始要经过一段飞行距离.
	/// </summary>
	public void BeginOccupy(GameObject occupant)
	{
		this.MOccupant = occupant;
		//--进入飞行等待时间.
		MCurState = EState.Waiting;
		
	}

	/// <summary>
	/// 完全占领 .
	/// </summary>
	public void NowOccupy()
	{
		MCurState = EState.Occupation;
	}

	/// <summary>
	/// 格子是否为空,
	/// </summary>
	public bool IsEmpty()
	{
		return MOccupant == null ;
	}

	public void CleanOccupant()
	{
		MOccupant     = null;
		MCurState = EState.Empty;
	}

	/// <summary>
	/// 获取占领者的半径.
	/// </summary>
	public void GetOccupantRadius()
	{
		
	}


}
