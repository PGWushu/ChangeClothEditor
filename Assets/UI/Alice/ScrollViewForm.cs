using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewForm : BaseUIForm
{
	public GameObject prefab;
	public Transform content;
	private void Awake()
	{
		CurrentUIType.UIForms_Type = UIFormType.PopUp;
		base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
	}

	StartAlice startAlice;
	private void OnEnable()
	{
		initScrollView();
	}
	private void Start()
	{
		startAlice = GameObject.Find("_StartGame").GetComponent<StartAlice>();
		initScrollView();
	}
	/// <summary>
	/// 初始化位置
	/// </summary>
	void initScrollView()
	{
		if (content.transform.childCount > 0)
		{
			for (int i = 0; i < content.transform.childCount; i++)
			{
				DestroyObject(content.transform.GetChild(i).gameObject);
			}
		}

		var list = TableManager.Instance.GetTable<ItemTemplate>().showClothList;
		var gridLayout = content.GetComponent<GridLayoutGroup>();
		var itemW = gridLayout.cellSize.x;
		var itemH = gridLayout.cellSize.y;
		var spacX = gridLayout.spacing.x;
		var spacY = gridLayout.spacing.y;
		var rect = content.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(rect.sizeDelta.x, list.Count * (itemH + spacY));
		for (int i = 0; i < list.Count; i++)
		{
			GameObject gob = Instantiate(prefab) as GameObject;
			gob.transform.SetParent(content.transform);
			var item = gob.transform.GetChild(0).GetComponent<Text>();
			item.text = "ID:" + list[i].templateID;
			gob.name = list[i].templateID.ToString();
			gob.GetComponent<Button>().onClick.RemoveAllListeners();
			gob.GetComponent<Button>().onClick.AddListener(() =>
			{
				startAlice.DressOn(gob.name);
				//删除搜索内容显示界面
				UIManager.GetInstance().CloseUIForms(ProConst.SCROLLVIEW_UIFROM);
			});
		}
	}
}
