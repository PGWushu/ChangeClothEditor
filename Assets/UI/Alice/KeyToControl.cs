using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyToControl : MonoBehaviour
{
	public float speed = 1;
	Vector3 movement;
	ClothDressInfo clothDressInfo;
	private void Start()
	{
		clothDressInfo = this.GetComponent<ClothDressInfo>();
	}
	void FixedUpdate()
	{
		if (clothDressInfo.isCanDrag)
		{
			float h = Input.GetAxisRaw("Horizontal");
			float v = Input.GetAxisRaw("Vertical");
			Move(h, v);
		}
	}
	void Move(float h, float v)
	{
		movement.Set(h, v, 0);
		movement = movement.normalized * speed * Time.deltaTime;
		transform.position = movement + transform.position;
	}
}
