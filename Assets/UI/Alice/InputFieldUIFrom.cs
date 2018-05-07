using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldUIFrom : BaseUIForm
{
	List<ItemData> dAllItems = new List<ItemData>();
	public InputField input;
	void Awake()
	{
		CurrentUIType.UIForms_Type = UIFormType.PopUp;
		dAllItems = new List<ItemData>(TableManager.Instance.GetTable<ItemTemplate>().dAllItems.Values);
		input.onValueChanged.AddListener(delegate
		{
			Compared(input.text.Trim());
			ShowScrollView();
		});
	}

	/// 将查找文字与库里的数据对比，然后生成列表  
	/// </summary>  
	public void Compared(string inputStr)
	{  
		TableManager.Instance.GetTable<ItemTemplate>().showClothList.Clear();
		var ch = inputStr.ToArray();
		if (ch.Length > 0)
		{
			if (!validateNum(inputStr))
			{
				for (int i = 0; i < dAllItems.Count; i++)
				{
					if (dAllItems[i].name.Contains(inputStr))
					{
						if (!TableManager.Instance.GetTable<ItemTemplate>().showClothList.Contains(dAllItems[i]))
							TableManager.Instance.GetTable<ItemTemplate>().showClothList.Add(dAllItems[i]);
					}
				}
			}
			else
			{
				for (int i = 0; i < dAllItems.Count; i++)
				{
					if (dAllItems[i].templateID.ToString().Contains(inputStr))
					{
						if (!TableManager.Instance.GetTable<ItemTemplate>().showClothList.Contains(dAllItems[i]))
							TableManager.Instance.GetTable<ItemTemplate>().showClothList.Add(dAllItems[i]);
					}
				}
			}
		}
	}
	/// <summary>
	/// 验证文本框输入为整数
	/// </summary>
	/// <param name="strNum">输入字符</param>
	/// <returns>返回一个bool类型的值</returns>
	public static bool validateNum(string strNum)
	{
		return Regex.IsMatch(strNum, "^[0-9]*$");
	}

	/// <summary>
	/// 是否需要显示搜索结果
	/// </summary>
	void ShowScrollView()
	{ 
		if (TableManager.Instance.GetTable<ItemTemplate>().showClothList.Count > 0)
		{
			//加载搜索内容显示界面
			UIManager.GetInstance().ShowUIForms(ProConst.SCROLLVIEW_UIFROM);
		}
		if (TableManager.Instance.GetTable<ItemTemplate>().showClothList.Count <= 0 || input.text == string.Empty)
		{
			//删除搜索内容显示界面
			UIManager.GetInstance().CloseUIForms(ProConst.SCROLLVIEW_UIFROM);
		}
	}
}

