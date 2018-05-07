using System;
using UnityEngine;
using UnityEngine.UI;
public class ClothPartPosUIForm : MonoBehaviour
{
	InputField InputField_X;
	InputField InputField_Y;
	InputField InputField_Z;
	public string modelPartName;
	ClothDressInfo clothDressInfo;
	BoxCollider boxCollider;
	private void Start()
	{
		Initialized();
		if (clothDressInfo != null)
		{
			if (clothDressInfo.isDressOn)
			{
				clothDressInfo.isCanDrag = true;
				boxCollider.enabled = true;
			}
			else
			{
				Debug.LogError("还没有穿衣服");
			}
		}
	}
	private void OnEnable()
	{
		if (clothDressInfo != null)
		{
			if (clothDressInfo.isDressOn)
			{
				clothDressInfo.isCanDrag = true;
				boxCollider.enabled = true;
			}
			else
			{
				Debug.LogError("还没有穿衣服");
			}
		}
	}
	public void Initialized()
	{
		InputField_X = this.transform.FindChild("InputField_X").GetComponent<InputField>();
		InputField_Y = this.transform.FindChild("InputField_Y").GetComponent<InputField>();
		InputField_Z = this.transform.FindChild("InputField_Z").GetComponent<InputField>();

		var clothPartPos = StartAlice.playerPart.FindChild(modelPartName).localPosition;
		clothDressInfo = StartAlice.playerPart.FindChild(modelPartName).GetComponent<ClothDressInfo>();
		boxCollider = StartAlice.playerPart.FindChild(modelPartName).GetComponent<BoxCollider>();
		InputField_X.text = ((double)clothPartPos.x).ToString();
		InputField_Y.text = ((double)clothPartPos.y).ToString();
		InputField_Z.text = ((double)clothPartPos.z).ToString();
		InputField_X.onValueChanged.RemoveAllListeners();
		InputField_Y.onValueChanged.RemoveAllListeners();
		InputField_Z.onValueChanged.RemoveAllListeners();
		InputField_X.onValueChanged.AddListener(delegate { ChangeInput(); });
		InputField_Y.onValueChanged.AddListener(delegate { ChangeInput(); });
		InputField_Z.onValueChanged.AddListener(delegate { ChangeInput(); });
	}
	private void ChangeInput()
	{
		if (clothDressInfo.isDressOn)
		{
			var x = (float)Convert.ToDouble(InputField_X.text);
			var y = (float)Convert.ToDouble(InputField_Y.text);
			var z = (float)Convert.ToDouble(InputField_Z.text);
			StartAlice.playerPart.FindChild(modelPartName).localPosition = new Vector3(x, y, z);
		}
		else
		{
			Debug.LogError("请先穿上衣服");
		}
	}
	private void OnDisable()
	{
		if (clothDressInfo != null)
		{
			clothDressInfo.isCanDrag = false;
			boxCollider.enabled = false;
		}
	}
	private void FixedUpdate()
	{
		var clothPartPos = StartAlice.playerPart.FindChild(modelPartName).localPosition;
		InputField_X.text = ((double)clothPartPos.x).ToString();
		InputField_Y.text = ((double)clothPartPos.y).ToString();
		InputField_Z.text = ((double)clothPartPos.z).ToString();
	}
}
