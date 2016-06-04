using UnityEngine;
using System.Collections;

public class BallMoveHelper : MonoBehaviour {

	private GridInfo _aimGrid;
	private Vector3 _aimPosition;

	void Start () 
	{
		
	}
	
	void Update () 
	{
	
		if(_aimGrid == null || _aimGrid.MCurState >= GridInfo.EState.Occupation)
			return;

		float dis = transform.position.MDistanceIgnoreY(_aimPosition);
		if(dis <= Definition.CellSize() / 2)
		{
			//--如果球体中心点已经进入方格边缘，就表示已经完全占领了网格.
			_aimGrid.NowOccupy();
			//销毁本脚本.
			DestroyImmediate(this);
		}

	}

	public void SetAim(GridInfo aim)
	{
		_aimGrid = aim;
		_aimPosition = Definition.GridToWorld(aim.MCoordinate.x,aim.MCoordinate.z);

	}

}
