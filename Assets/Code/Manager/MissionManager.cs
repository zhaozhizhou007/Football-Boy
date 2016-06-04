using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : SingletonCom<MissionManager> {

	public GridInfo[,] BallGrid = new GridInfo[Definition.GridNum,Definition.GridNum];

	public GameObject Boss,Ball;
	public GridShowController sc;

	/// <summary>
	/// 存储所有
	/// </summary>
	private List<SGridCoordinate> _RandomGridList = new List<SGridCoordinate> ();


	 void Start ()
	{
		InitGrid();
	}
	
	void Update () 
	{
	}

	/// <summary>
	/// 初始化网格
	/// </summary>
	private void InitGrid()
	{
		_RandomGridList.Clear();
		for(int i = 0;i < Definition.GridNum; i++)
		{
			for(int j = 0;j < Definition.GridNum; j++)
			{
				SGridCoordinate coor = new SGridCoordinate (i,j);

				BallGrid[i,j] = new GridInfo(coor);
				_RandomGridList.Add(coor);
			}
		}
	}
		



	public bool IsEmpty(int x,int y)
	{
		//return !BallGrid[x,y];
		return false;
	}

	public void Set(int x,int y)
	{
		//BallGrid[x,y] = true;
	}

	public void Remove(int x,int y)
	{
//		BallGrid[x,y] = false;
	}

	private IEnumerator _SoShoot()
	{
		while(true)
		{
			yield return new WaitForSeconds(3f);
			ShootBall();

		}

	}


	/// <summary>
	/// Determines whether this instance can move the specified curCoor nextDir.
	/// </summary>
	/// <returns><c>true</c> if this instance can move the specified curCoor nextDir; otherwise, <c>false</c>.</returns>
	/// <param name="curCoor">Current coor.</param>
	/// <param name="nextDir">Next dir.</param>
	public bool CanMove(SGridCoordinate curCoor,EDirection nextDir)
	{
		//---判断目标是不是一个好位置
		SGridCoordinate nextCoor = Definition.GetNextCoorinate(curCoor,nextDir);
		if(nextCoor.Overstep)
		{
			return false;
		}
		GridInfo theGrid = BallGrid[nextCoor.x,nextCoor.z];
		bool result = theGrid.MCurState != GridInfo.EState.Occupation;
		return result;
	}

	//--需要重构
	public void ShootBall()
	{

		int randomIndex = Random.Range(0,_RandomGridList.Count);
		SGridCoordinate coor = _RandomGridList[randomIndex];
		GridInfo theCell = BallGrid[coor.x,coor.z];

		if(theCell.IsEmpty())
		{
			_RandomGridList.RemoveAt(randomIndex);
			Vector3 worldPosition = Definition.GridToWorld(coor.x,coor.z);
			sc.Show(coor);


			GameObject ball = GameObject.Instantiate(Ball,Boss.transform.position,Quaternion.identity) as GameObject;
			MotionParabolic motion = ball.AddMissingComponent<MotionParabolic>();
			motion.Go(Boss.transform.position,worldPosition,30,10);
			motion.SetCallBack(
				delegate() 
				{
					ToolAutoDestory boom = ball.AddMissingComponent<ToolAutoDestory>();
					boom.SetCallBack(delegate {
						theCell.CleanOccupant();
						_RandomGridList.Add(coor);
					});
					boom.Do(20);
				}

			);

			//网格准备被占领.
			theCell.BeginOccupy(ball);
			ball.AddMissingComponent<BallMoveHelper>().SetAim(theCell);
		}

	}

	/// <summary>
	/// 踢回球.需要重构.
	/// </summary>
	/// <param name="oriCoor">角色坐标.</param>
	/// <param name="coor">球所在坐标</param>
	public void KickBackBall(SGridCoordinate oriCoor, SGridCoordinate coor)
	{
		GridInfo theGrid = BallGrid[coor.x,coor.z];
		if(theGrid.MCurState == GridInfo.EState.Occupation)
		{
			GameObject ball = theGrid.MOccupant;
			if(ball != null)
			{
				ToolAutoDestory boom = ball.AddMissingComponent<ToolAutoDestory>();
				boom.Stop();
				boom.Do(5);

				MotionParabolic motion = ball.AddMissingComponent<MotionParabolic>();
				motion.Go(ball.transform.position,Boss.transform.position,30,10);
				motion.SetCallBack(null);

			}

			theGrid.CleanOccupant();

		}
	}


	void OnGUI()
	{
		
		if(GUI.Button(new Rect(30,10,100,50),"Start Shoot"))
		{
			//StartCoroutine(_SoShoot());
			ShootBall();
		}
	}


}
