using UnityEngine;
using System.Collections;


/// <summary>
/// 网格选择显示控制
/// </summary>
public class GridShowController : MonoBehaviour 
{

	public int width = 10;
	public int height = 10;

	public float Height = -0.05f;

	//--选择网格
	public GameObject PreShowBox;

	void Start () 
	{
	
	}
	

	void Update () 
	{
	
	}

	void OnEnable()
	{
		TouchManager.I.On_TapOne += DelegateTap;
	}
	void OnDisable()
	{
		TouchManager.I.On_TapOne -= DelegateTap;
	}
	void DelegateTap (GameObject clickObject,Vector3 position)
	{
		Show(position);

	}
	/// <summary>
	/// 显示选中网格
	/// </summary>
	/// <param name="position">Position.</param>
	public void Show(Vector3 position)
	{
		//原点是0，0
		float x = position.x ;
		float y = position.z ;

		int width = Mathf.FloorToInt(x);
		int height = Mathf.FloorToInt(y);

		//Debug.Log("width: " + width + " height: " + height);

		Vector3 showPosition = new Vector3(width + 0.5f, Height,height + 0.5f);

		GameObject theShow = GameObject.Instantiate(PreShowBox,showPosition,Quaternion.identity) as GameObject;
		theShow.transform.localEulerAngles = new Vector3(90,0,0);
		theShow.transform.SetParent(transform,true);


		theShow.AddMissingComponent<ToolAutoDestory>().Do();

	}

	public void Show(SGridCoordinate coor)
	{
		Vector3 showPosition = Definition.GridToWorld(coor.x,coor.z);
		GameObject theShow = GameObject.Instantiate(PreShowBox,showPosition,Quaternion.identity) as GameObject;
		theShow.transform.localEulerAngles = new Vector3(90,0,0);
		theShow.transform.SetParent(transform,true);
		theShow.AddMissingComponent<ToolAutoDestory>().Do();

	}


}
