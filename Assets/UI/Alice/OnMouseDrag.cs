using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseDrag : MonoBehaviour
{ 
	void Start()
	{
		StartCoroutine(OnMouseDown());
	}
	public Camera c ;
	public string modelPartName;

	IEnumerator OnMouseDown()
	{ 
		Vector3 screenSpace = c .WorldToScreenPoint(transform.position); 
		Vector3 offset = transform.position - c .ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
		while (Input.GetMouseButton(0))
		{ 
			Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
			Vector3 curPosition = c .ScreenToWorldPoint(curScreenSpace) + offset;
			transform.position = curPosition;
			yield return new WaitForFixedUpdate(); 
		}
	}
}
