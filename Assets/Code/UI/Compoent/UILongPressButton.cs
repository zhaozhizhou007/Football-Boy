using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UILongPressButton : MonoBehaviour ,IPointerEnterHandler, IPointerDownHandler,IPointerExitHandler,IPointerUpHandler{


	[SerializeField]
	UnityEvent m_onLongPress = new UnityEvent();

	float interval = 0.1f;
//	float longPressDelay = 0.5f;

	private bool isTouchDown = false;
	private bool isLongpress = false;
//	private float touchBegin = 0;
	private float lastInvokeTime = 0;


	void Start () {
	
	}
	
	void Update ()
	{
		if (isTouchDown)
		{
	
				if (Time.time - lastInvokeTime > interval)
				{
					m_onLongPress.Invoke();
					lastInvokeTime = Time.time;
				}

//			else
//			{
//				isLongpress = Time.time - touchBegin > longPressDelay;
//			}
		}
	}


	public void OnPointerEnter (PointerEventData eventData)
	{
		//touchBegin = Time.time;
		isTouchDown = true;

	}

	public void OnPointerDown (PointerEventData eventData)
	{
		
		//touchBegin = Time.time;
		isTouchDown = true;
		isLongpress = true;
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		isTouchDown = false;
//		isLongpress = false;
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		isTouchDown = false;
		isLongpress = false;
	}


}
