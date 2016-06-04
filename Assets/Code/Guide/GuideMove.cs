using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using DG.Tweening;

public class GuideMove : AIPath {


	//移动的目标点位.
	public GameObject MoveAim;


	public List<GameObject> Os;

	public AstarPath Astar;

	void DelegateTap (GameObject clickObject,Vector3 position)
	{

		Vector3 gridPosition = Definition.WorldToGrid(position);//ToolUtil.Position2GridPosition(position);
		MoveAim.transform.position = gridPosition;
		Debug.Log("move " + canMove);
		canSearch = true;
		canMove = true;

	}

	public override void OnTargetReached ()
	{
		canSearch = false;
		canMove = false;
	}

	void OnpathEnd(Pathfinding.Path path)
	{
		Debug.Log(path.vectorPath.Count);
	}

	protected override void OnEnable ()
	{
		base.OnEnable ();
		TouchManager.I.On_TapOne += DelegateTap;
	}

	public override void OnDisable()
	{
		TouchManager.I.On_TapOne -= DelegateTap;
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(10,10,100,50),"DO"))
		{
			foreach(var one in Os)
			{
				if(one && one.activeInHierarchy == false)
				{
					one.SetActive(true);
					Astar.Scan();
					break;

				}

			}
		}
	}



}
