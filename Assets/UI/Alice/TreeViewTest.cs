using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class TreeViewTest : MonoBehaviour
{
	public TreeViewControl TreeView;

	private void Start()
	{
		//生成数据
		List<TreeViewData> datas = new List<TreeViewData>();
		TreeViewData data = new TreeViewData();

		var clothList = new List<ItemData>(TableManager.Instance.GetTable<ItemTemplate>().dAllItems.Values);
		foreach (EDressType step in Enum.GetValues(typeof(EDressType)))
		{
			data = new TreeViewData();
			data.Name = step.ToString();
			data.ParentID = -1;
			datas.Add(data);
		}
		foreach (var item in clothList)
		{
			data = new TreeViewData();
			data.Name = item.templateID + "  " + item.name;
			data.ParentID = item.dressType;
			data.ClothID = item.templateID;
			datas.Add(data);
		}
		//指定数据源
		TreeView.Data = datas;
		//重新生成树形菜单
		TreeView.GenerateTreeView();
		//刷新树形菜单
		TreeView.RefreshTreeView();
		//注册子元素的鼠标点击事件
		TreeView.ClickItemEvent += CallBack;
	}

	void Update()
	{
		////判断树形菜单中名为“ 第一章 ”的元素是否被勾选
		//if (Input.GetKeyDown(KeyCode.A))
		//{
		//	bool isCheck = TreeView.ItemIsCheck("第一章");
		//	Debug.Log("当前树形菜单中的元素 第一章 " + (isCheck ? "已被选中！" : "未被选中！"));
		//}
		////获取树形菜单中所有被勾选的元素
		//if (Input.GetKeyDown(KeyCode.S))
		//{
		//	List<string> items = TreeView.ItemsIsCheck();
		//	for (int i = 0; i < items.Count; i++)
		//	{
		//		Debug.Log("当前树形菜单中被选中的元素有：" + items[i]);
		//	}
		//}
	}

	void CallBack(GameObject item)
	{
		Debug.Log("点击了 " + item.transform.FindChild("TreeViewText").GetComponent<Text>().text);
	}
}
