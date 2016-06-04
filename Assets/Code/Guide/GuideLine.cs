using UnityEngine;
using System.Collections;

public class GuideLine : MonoBehaviour
{

	void Start ()
	{
		
	}

	void Update ()
	{
		
	}

	void OnDrawGizmos ()
	{

		int num = Definition.GridNum;
		Vector3 start = Definition.GridOrigin;

		Gizmos.color = Color.green;

		float cellWidth = Definition.GridLength / Definition.GridNum;

		for (int i = 0; i < num; i++) {
			Gizmos.DrawLine (start + Vector3.forward * i * cellWidth, start + Vector3.forward * i * cellWidth + Vector3.right * num * cellWidth);
			Gizmos.DrawLine (start + Vector3.right * i * cellWidth, start + Vector3.right * i * cellWidth + Vector3.forward * num * cellWidth);
		}
		Gizmos.DrawLine (start + Vector3.forward * num * cellWidth, start + Vector3.forward * num * cellWidth + Vector3.right * num * cellWidth);
		Gizmos.DrawLine (start + Vector3.right * num * cellWidth, start + Vector3.right * num * cellWidth + Vector3.forward * num * cellWidth);

		//--监控当前网格状态.
		for (int i = 0; i < Definition.GridNum; i++) {
			for (int j = 0; j < Definition.GridNum; j++) {
				GridInfo info = MissionManager.I.BallGrid [i, j];
				if(info == null)
					continue;
				if (info.MCurState == GridInfo.EState.Occupation) 
				{
					Gizmos.color = Color.black;
				} else if (info.MCurState == GridInfo.EState.Waiting) 
				{
					Gizmos.color = Color.yellow;
				} else {
					Gizmos.color = Color.green;
				}

				Gizmos.DrawCube (Definition.GridToWorld (info.MCoordinate.x, info.MCoordinate.z), Vector3.one * Definition.CellSize ());

			}
		}

	}


}
