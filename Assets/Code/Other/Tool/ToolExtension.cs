using UnityEngine;
using System.Collections;

public static class ToolExtension
{

	/// <summary>
	/// 添加缺失的脚本.
	/// </summary>
	/// <returns>The missing component.</returns>
	/// <param name="go">Go.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	static public T AddMissingComponent<T> (this GameObject go) where T : Component
	{
		#if UNITY_FLASH
		object comp = go.GetComponent<T>();
		#else
		T comp = go.GetComponent<T> ();
		#endif
		if (comp == null) {
		#if UNITY_EDITOR
			
		#endif
			comp = go.AddComponent<T> ();
		}
		#if UNITY_FLASH
		return (T)comp;
		#else
		return comp;
		#endif
	}



	#region Distance and Direction

	/// <summary>
	/// 设置物体的先前方向, 并且是忽略Y轴的位置.
	/// </summary>
	/// <param name="tran">Tran.</param>
	/// <param name="target">Target.</param>
	static public void MSetForward (this GameObject tran, Vector3 targetPos)
	{
		//tran.LookAt ();
		Vector3 thePos = tran.transform.position;
		thePos.y = 0;
		targetPos.y = 0;
		tran.transform.forward = (targetPos - thePos).normalized;

	}

	static public void MSetForward (this GameObject tran, GameObject target)
	{
		if (target == null)
			return;

		MSetForward (tran, target.transform.position);
	}


	/// <summary>
	/// 计算物体到点的方向.
	/// </summary>
	/// <returns>The direction ignore y.</returns>
	/// <param name="noumenon">Noumenon.</param>
	/// <param name="aimPos">Aim position.</param>
	static public Vector3 MDirectionIgnoreY(this GameObject noumenon, Vector3 aimPos)
	{
		Vector3 bodyPos = noumenon.transform.position;
		bodyPos.y = 0;
		aimPos.y = 0;

		return (aimPos - bodyPos).normalized;

	}
	static public Vector3 MDirectionIgnoreY(this GameObject noumenon, GameObject aim)
	{
		return MDirectionIgnoreY(noumenon,aim.transform.position);
	}
	static public Vector3 MDirectionIgnoreY(this Vector3 oriPos,Vector3 aimPos){
		oriPos.y = 0;
		aimPos.y = 0;
		return (aimPos - oriPos).normalized;

	}


	/// <summary>
	/// 计算距离并且忽略Y轴.
	/// </summary>
	/// <param name="">.</param>
	/// <param name="aimPos">Aim position.</param>
	static public float MDistanceIgnoreY (this GameObject noumenon, Vector3 aimPos)
	{
		Vector3 bodyPos = noumenon.transform.position;
		bodyPos.y = 0;
		aimPos.y = 0;

		return Vector3.Distance (bodyPos, aimPos);

	}

	/// <summary>
	/// 计算距离并且忽略Y轴.
	/// </summary>
	/// <param name="">.</param>
	/// <param name="aimPos">Aim position.</param>
	static public float MDistanceIgnoreY (this GameObject noumenon, GameObject aim)
	{
		Vector3 bodyPos = noumenon.transform.position;
		bodyPos.y = 0;
		Vector3 aimPos = aim.transform.position;
		aimPos.y = 0;

		return Vector3.Distance (bodyPos, aimPos);

	}
	static public float MDistanceIgnoreY (this Vector3 noumenon, Vector3 aimPos)
	{
		noumenon.y = 0;
		aimPos.y = 0;
		
		return Vector3.Distance (noumenon, aimPos);
		
	}

	/// <summary>
	/// 位置偏移.
	/// </summary>
	/// <returns>The positon offect.</returns>
	/// <param name="pos">Position.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	static public Vector3 MPositonOffect(this Vector3 pos, float x,float y,float z)
	{
		pos.x += x;
		pos.y += y;
		pos.z += z;

		return pos;
	}

	#endregion

	#region Parent and Child

	/// <summary>
	/// 查找子物体.
	/// </summary>
	/// <returns>The find child.</returns>
	/// <param name="parent">Parent.</param>
	/// <param name="tranName">Tran name.</param>
	static public Transform MFindChild (this Transform parent, string tranName)
	{
//		Debug.Log (parent.name);

		Transform targetTran = null;
		foreach (Transform child in parent) 
		{
			if (child.name.Equals (tranName)) 
			{
				targetTran = child;
				break;
			} else 
			{
				targetTran = child.MFindChild (tranName);
				if (targetTran != null) 
				{
					break;
				}
			}
		}
		return targetTran;

	}

	static public int MGetLiveChildCount(this Transform parent)
	{
		int count = 0;
		foreach (Transform child in parent) 
		{
			//if(child.InverseTransformDirection ())
			if (child.gameObject.activeInHierarchy)
				++count;
		}

		return count;
	}

	/// <summary>
	/// 设置父物体.
	/// </summary>
	/// <param name="non">Non.</param>
	/// <param name="parent">Parent.</param>
	static public void MSetParent (this Transform non, Transform parent)
	{
		
		non.transform.parent = parent.transform;
		non.transform.localPosition = Vector3.zero;
		non.transform.localRotation = Quaternion.identity;

	}

	#endregion

	#region 层级 标签
	static public void MSetLayer(this GameObject obj,string layerName)
	{
		int layer = LayerMask.NameToLayer (layerName);
		foreach(Transform t in obj.GetComponentsInChildren<Transform>())
		{
			t.gameObject.layer = layer;
		}

	}
	/// <summary>
	/// 删除子节点
	/// </summary>
	/// <param name="obj">Object.</param>
	static public void MCleanChildren(this Transform obj)
	{

		int count = obj.childCount;
		for(int i = 0 ; i < count ;i ++)
		{
			Object.Destroy (obj.GetChild (i).gameObject);
		}

	}
	#endregion

}
