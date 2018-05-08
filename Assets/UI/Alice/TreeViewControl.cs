using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TreeViewControl : MonoBehaviour
{
	[HideInInspector]
	public List<TreeViewData> Data = null;

	public GameObject Template;

	public Transform TreeItems;

	public int VerticalItemSpace = 2;

	public int HorizontalItemSpace = 25;

	public int ItemWidth = 230;

	public int ItemHeight = 35;

	public delegate void ClickItemdelegate(GameObject item);
	public event ClickItemdelegate ClickItemEvent;

	private List<GameObject> _treeViewItems;

	private List<GameObject> _treeViewItemsClone;
	private int _yIndex = 0;
	private int _hierarchy = 0;

	void Awake()
	{
		ClickItemEvent += ClickItemTemplate;
	}
	/// <summary>
	/// 鼠标点击子元素事件
	/// </summary>
	public void ClickItem(GameObject item)
	{
		ClickItemEvent(item);
	}
	void ClickItemTemplate(GameObject item)
	{

	}

	/// <summary>
	/// 返回指定名称的子元素是否被勾选
	/// </summary>
	public bool ItemIsCheck(string itemName)
	{
		for (int i = 0; i < _treeViewItems.Count; i++)
		{
			if (_treeViewItems[i].transform.FindChild("TreeViewText").GetComponent<Text>().text == itemName)
			{
				return _treeViewItems[i].transform.FindChild("TreeViewToggle").GetComponent<Toggle>().isOn;
			}
		}
		return false;
	}
	/// <summary>
	/// 返回树形菜单中被勾选的所有子元素名称
	/// </summary>
	public List<string> ItemsIsCheck()
	{
		List<string> items = new List<string>();

		for (int i = 0; i < _treeViewItems.Count; i++)
		{
			if (_treeViewItems[i].transform.FindChild("TreeViewToggle").GetComponent<Toggle>().isOn)
			{
				items.Add(_treeViewItems[i].transform.FindChild("TreeViewText").GetComponent<Text>().text);
			}
		}

		return items;
	}

	/// <summary>
	/// 生成树形菜单
	/// </summary>
	public void GenerateTreeView()
	{
		//删除可能已经存在的树形菜单元素
		if (_treeViewItems != null)
		{
			for (int i = 0; i < _treeViewItems.Count; i++)
			{
				Destroy(_treeViewItems[i]);
			}
			_treeViewItems.Clear();
		}
		//重新创建树形菜单元素
		_treeViewItems = new List<GameObject>();
		for (int i = 0; i < Data.Count; i++)
		{
			GameObject item = Instantiate(Template);

			if (Data[i].ParentID == -1)
			{
				item.GetComponent<TreeViewItem>().SetHierarchy(0);
				item.GetComponent<TreeViewItem>().SetParent(null);
				item.transform.FindChild("TreeViewToggle").gameObject.SetActive(false);
			}
			else
			{
				TreeViewItem tvi = _treeViewItems[Data[i].ParentID].GetComponent<TreeViewItem>();
				item.GetComponent<TreeViewItem>().SetHierarchy(tvi.GetHierarchy() + 1);
				item.GetComponent<TreeViewItem>().SetParent(tvi);
				tvi.AddChildren(item.GetComponent<TreeViewItem>());
				var toggle = item.transform.FindChild("TreeViewToggle").gameObject;
				item.transform.FindChild("ContextButton").gameObject.SetActive(false);
				CheckShow(Data[i].ClothID, toggle);
			}

			item.transform.name = Data[i].Name;
			item.transform.FindChild("TreeViewText").GetComponent<Text>().text = Data[i].Name;
			item.transform.SetParent(TreeItems);
			item.transform.localPosition = Vector3.zero;
			item.transform.localScale = Vector3.one;
			item.transform.localRotation = Quaternion.Euler(Vector3.zero);
			item.SetActive(true);

			_treeViewItems.Add(item);
		}
	}

	/// <summary>
	/// 刷新树形菜单
	/// </summary>
	public void RefreshTreeView()
	{
		_yIndex = 0;
		_hierarchy = 0;

		//复制一份菜单
		_treeViewItemsClone = new List<GameObject>(_treeViewItems);

		//用复制的菜单进行刷新计算
		for (int i = 0; i < _treeViewItemsClone.Count; i++)
		{
			//已经计算过或者不需要计算位置的元素
			if (_treeViewItemsClone[i] == null || !_treeViewItemsClone[i].activeSelf)
			{
				continue;
			}

			TreeViewItem tvi = _treeViewItemsClone[i].GetComponent<TreeViewItem>();

			_treeViewItemsClone[i].GetComponent<RectTransform>().localPosition = new Vector3(tvi.GetHierarchy() * HorizontalItemSpace, _yIndex, 0);
			_yIndex += (-(ItemHeight + VerticalItemSpace));
			if (tvi.GetHierarchy() > _hierarchy)
			{
				_hierarchy = tvi.GetHierarchy();
			}

			if (tvi.IsExpanding)
			{
				RefreshTreeViewChild(tvi);
			}

			_treeViewItemsClone[i] = null;
		}

		//重新计算滚动视野的区域
		float x = _hierarchy * HorizontalItemSpace + ItemWidth;
		float y = Mathf.Abs(_yIndex);
		transform.GetComponent<ScrollRect>().content.sizeDelta = new Vector2(x, y);

		//清空复制的菜单
		_treeViewItemsClone.Clear();
	}
	/// <summary>
	/// 刷新元素的所有子元素
	/// </summary>
	void RefreshTreeViewChild(TreeViewItem tvi)
	{
		for (int i = 0; i < tvi.GetChildrenNumber(); i++)
		{
			tvi.GetChildrenByIndex(i).gameObject.GetComponent<RectTransform>().localPosition = new Vector3(tvi.GetChildrenByIndex(i).GetHierarchy() * HorizontalItemSpace, _yIndex, 0);
			_yIndex += (-(ItemHeight + VerticalItemSpace));
			if (tvi.GetChildrenByIndex(i).GetHierarchy() > _hierarchy)
			{
				_hierarchy = tvi.GetChildrenByIndex(i).GetHierarchy();
			}

			if (tvi.GetChildrenByIndex(i).IsExpanding)
			{
				RefreshTreeViewChild(tvi.GetChildrenByIndex(i));
			}

			int index = _treeViewItemsClone.IndexOf(tvi.GetChildrenByIndex(i).gameObject);
			if (index >= 0)
			{
				_treeViewItemsClone[index] = null;
			}
		}
	}

	/// <summary>
	/// 判断是否开眼
	/// </summary>
	void CheckShow(int tempID, GameObject g)
	{
		ItemData dd = TableManager.Instance.GetTable<ItemTemplate>().GetData(tempID) as ItemData;
		var playerPart = GameObject.Find("PlayerModel/Part").transform;
		foreach (Transform item in playerPart.GetComponentInChildren<Transform>())
		{
			var go = item.GetComponent<ClothDressInfo>();
			var togger = g.GetComponent<Toggle>();
			if ((int)go.eDressType == dd.dressType)
			{ 
				if (go.itemID == tempID)
				{
					togger.isOn = true ;
				}
				else
				{
					togger.isOn = false;
				}
				togger.onValueChanged.RemoveAllListeners();
				togger.onValueChanged.AddListener(delegate
				{ 
				 item.gameObject.SetActive(togger.isOn);
				});
			}		
		}
	}
}

