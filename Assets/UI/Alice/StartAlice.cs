
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartAlice : MonoBehaviour
{
	public static Transform startAlice;
	public static Transform playerPart;
	public string content;
	private void Awake()
	{
		playerPart = GameObject.Find("PlayerModel/Part").transform;
	}
	IEnumerator Start()
	{
		StartCoroutine(GetTxt());
		yield return new WaitForSeconds(0.2f);
		TableManager.Instance.InitTable<ItemTemplate>();
		yield return new WaitForSeconds(0.2f);
		//加载4个按钮事件窗体
		UIManager.GetInstance().ShowUIForms(ProConst.BUTTON_EVENT_UIFROM);
		//加载搜索框界面
		UIManager.GetInstance().ShowUIForms(ProConst.INPUTFIELD_UIFROM);
		//加载类型树状显示界面
		UIManager.GetInstance().ShowUIForms(ProConst.CLOTHTYPE_UIFROM);
		//加载模型曾经界面
		UIManager.GetInstance().ShowUIForms(ProConst.CLOTHLAYER_UIFROM);
		//加载界面控制界面
		UIManager.GetInstance().ShowUIForms(ProConst.WINDOW_UIFORM);
	}

	/// <summary>
	/// 穿衣服
	/// </summary>
	public IEnumerator PlayerDressCloth(GameObject go, string iconPath)
	{
		yield return LoadTextureAsync(iconPath);;
		SpriteRenderer spr = go.GetComponent<SpriteRenderer>();
		spr.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
		go.gameObject.SetActive(true);
	}
	Texture2D texture;
	IEnumerator LoadTextureAsync(string iconPath)
	{
		texture = new Texture2D(1, 1);
		string path = Application.streamingAssetsPath + "/ClothPNG/" + iconPath + ".png";
		string fileToLoad = path.Replace("\\", "/");
		if (!fileToLoad.StartsWith("http"))
		{
			fileToLoad = string.Format("file://{0}", fileToLoad);
		}
		WWW www = new WWW(fileToLoad);
		yield return www;
		www.LoadImageIntoTexture(texture);
	}
	/// <summary>
	/// 换装
	/// </summary>
	public void DressOn(string goName)
	{
		var tempID = int.Parse(goName);
		ItemData dd = TableManager.Instance.GetTable<ItemTemplate>().GetData(tempID) as ItemData;
		foreach (Transform item in playerPart.GetComponentInChildren<Transform>())
		{
			var go = item.GetComponent<ClothDressInfo>();
			if ((int)go.eDressType == dd.dressType)
			{
				if (dd.templateID != go.itemID && !go.isDressOn)
				{
					Debug.LogError("开始换装" + dd.templateID);
					go.itemID = dd.templateID;
					go.isDressOn = true;
					StartCoroutine(PlayerDressCloth(go.gameObject, dd.icon));
					CheckDresType(dd.dressType);
					UIManager.GetInstance().GetUIForms(ProConst.CLOTHTYPE_UIFROM).GetComponent<TreeViewControl>().GenerateTreeView();
				}
				else
				{
					go.itemID = 0;
					go.isDressOn = false;
					Debug.LogError("脱掉衣服");
					go.gameObject.SetActive(false);
				}
			}
		}
	}
	/// <summary>
	/// 检查是否有衣服冲突
	/// </summary>
	void CheckDresType(int eDressType)
	{
		switch (eDressType)
		{
			case (int)EDressType.Dress:
				HideCloth(EDressType.Shirt);
				HideCloth(EDressType.Suit);
				HideCloth(EDressType.Trousers);
				break;
			case (int)EDressType.Trousers:
				HideCloth(EDressType.Dress);
				break;
			case (int)EDressType.Suit:
				HideCloth(EDressType.Dress);
				break;
			case (int)EDressType.Shirt:
				HideCloth(EDressType.Dress);
				break;
		}
	}
	void HideCloth(EDressType eDressType)
	{
		for (int i = 0; i < playerPart.childCount; i++)
		{
			var clo = playerPart.GetChild(i).GetComponent<ClothDressInfo>();
			if (clo.eDressType == eDressType)
			{
				clo.itemID = 0;
				clo.isDressOn = false;
				playerPart.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
	/// <summary>
	/// 解析Txt
	/// </summary>
	IEnumerator ParseTxt()
	{
		string path = Application.streamingAssetsPath + "/ExcelToTxt";
		DirectoryInfo di = new DirectoryInfo(path);
		var p = GetCleanFileName(di.GetFiles("*.txt")[0].FullName);
		WWW www = new WWW(p);
		yield return www;
		if (www.isDone)
			content = www.text;
	}
	IEnumerator GetTxt()
	{
		yield return StartCoroutine(ParseTxt());
	}

	private static string GetCleanFileName(string originalFileName)
	{
		string fleToLoad = originalFileName.Replace("\\", "/");
		if (!fleToLoad.StartsWith("http"))
		{
			fleToLoad = string.Format("file://{0}", fleToLoad);
		}
		return fleToLoad;
	}
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				Debug.Log("当前没有触摸在UI上");
				UIManager.GetInstance().CloseUIForms(ProConst.SCROLLVIEW_UIFROM);
				UIManager.GetInstance().CloseUIForms(ProConst.RESOURECESSCRECH_UIFROM);
			}
		}
	}
}