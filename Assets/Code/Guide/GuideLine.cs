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

		Vector3 start = Definition.GridOrigin;

		Gizmos.color = Color.green;

		float cellWidth = Definition.CellSize;

		for (int i = 0; i < Definition.GridWidthCount; i++) 
		{
			Gizmos.DrawLine (start + Vector3.right * i * cellWidth, start + Vector3.right * i * cellWidth + Vector3.forward * Definition.GridHeightCount * cellWidth);
		}

		for (int i = 0; i < Definition.GridHeightCount; i++) 
		{
			Gizmos.DrawLine (start + Vector3.forward * i * cellWidth, start + Vector3.forward * i * cellWidth + Vector3.right * Definition.GridWidthCount * cellWidth);
		}

		Gizmos.DrawLine (start + Vector3.forward * Definition.GridHeightCount * cellWidth, start + Vector3.forward * Definition.GridHeightCount * cellWidth + Vector3.right * Definition.GridWidthCount * cellWidth);
		Gizmos.DrawLine (start + Vector3.right * Definition.GridWidthCount * cellWidth, start + Vector3.right * Definition.GridWidthCount * cellWidth + Vector3.forward * Definition.GridHeightCount * cellWidth);

		//--监控当前网格状态.
		for (int i = 0; i < Definition.GridWidthCount; i++) 
		{
			for (int j = 0; j < Definition.GridHeightCount; j++) 
			{
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

				Gizmos.DrawCube (Definition.GridToWorld (info.MCoordinate.x, info.MCoordinate.z), Vector3.one * Definition.CellSize);

			}
		}

	}


}
