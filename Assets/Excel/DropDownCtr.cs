using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownCtr : MonoBehaviour
{
	static DropDownCtr instance;
	public static DropDownCtr Instance {
		get {
			if (instance == null)
				instance = new DropDownCtr();
			return instance;
		}
	}
	Dropdown dropDownItem;
	public string[] ShowTextssss;
	public List<string> temoNames;

	void Start()
	{
		dropDownItem = GetComponent<Dropdown>();
		temoNames = new List<string>();
		AddNames();
		UpdateDropDownView(temoNames);

	}
	/// <summary>
	/// 刷新列表
	/// </summary>
	/// <param name="shownames"></param>
	void UpdateDropDownView(List<string> shownames)
	{
		dropDownItem.options.Clear();
		Dropdown.OptionData tempData;

		for (int i = 0; i < shownames.Count; i++)
		{
			tempData = new Dropdown.OptionData();
			tempData.text = shownames[i];
			dropDownItem.options.Add(tempData);
		}
		dropDownItem.captionText.text = shownames[0];
	}

	/// <summary>
	/// 模拟添加数据
	/// </summary>
	void AddNames()
	{
		for (int i = 0; i < ShowTextssss.Length; i++)
		{
			temoNames.Add(ShowTextssss[i]);
		}
	}

	public void ChangeVa()
	{
		Debug.Log(" ss " + ShowTextssss[dropDownItem.value]);
	}
}
