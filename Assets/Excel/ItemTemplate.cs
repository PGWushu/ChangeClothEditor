using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemData
{
	public int templateID;
	public string name;
	public string icon;
	public int dressType;
}

public class ItemTemplate : BaseTemplate
{
	public Dictionary<int, ItemData> dAllItems = new Dictionary<int, ItemData>();
	private int dressCount = 0;
	private Dictionary<int, ItemData> DAllItems { get { return dAllItems; } }

	public List<ItemData> showClothList = new List<ItemData>();
	public List<Texture> showGesturesList = new List<Texture>();
	public List<Texture> showBGList = new List<Texture>();
	public List<Texture> showPlayerList = new List<Texture>();
	/// <summary>
	/// 储存衣服层级信息
	/// </summary>
	public Dictionary<string, Vector3> clothLayerInfo = new Dictionary<string, Vector3>();

	public override string GetFileName()
	{
		return "items";
	}

	public override void LoadData(ref string[] colums)
	{
		int start = 0;
		int itemid = TypeParser.intParse(colums[start]);
		if (itemid <= 0)
		{
			return;
		}

		ItemData data = new ItemData();
		data.templateID = itemid;
		start++;
		data.name = colums[start];
		start++;
		data.icon = colums[start];
		start++;
		data.dressType = TypeParser.intParse(colums[start]);
		AddData(data);
	}

	public void AddData(ItemData itemBase)
	{
		dAllItems.Add(itemBase.templateID, itemBase);
	}

	public bool HaveItem(int templateID)
	{
		if (dAllItems.ContainsKey(templateID))
			return true;
		return false;
	}

	public ItemData GetData(int templateID)
	{
		if (dAllItems.ContainsKey(templateID))
			return dAllItems[templateID];
		if (templateID > 10)
		{
			Debug.LogError("ItemData is null! id = " + templateID);
			Debug.LogError("ItemData is null! id = " + templateID);
		}
		return null;
	}

	public int GetDressCount()
	{
		return dressCount;
	}
}

