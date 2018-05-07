
using UnityEngine;
using UnityEngine.UI;

public class ClothLayerUIForm : BaseUIForm
{ 
	public GameObject prefab;
	public Transform content;
	public GameObject playerPart;
	private void Awake()
	{
		CurrentUIType.UIForms_Type = UIFormType.PopUp;
		base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
	}

	private void Start()
	{
		playerPart = GameObject.Find("PlayerModel/Part");
		initScrollView();
	}
	/// <summary>
	/// 初始化位置
	/// </summary>
	void initScrollView()
	{
		var list = playerPart.transform.childCount;  
		var gridLayout = content.GetComponent<GridLayoutGroup>();
		var itemW = gridLayout.cellSize.x;
		var itemH = gridLayout.cellSize.y;
		var spacX = gridLayout.spacing.x;
		var spacY = gridLayout.spacing.y;
		var rect = content.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(rect.sizeDelta.x, list * (itemH + spacY));
		for (int i = 0; i < list; i++)
		{
			GameObject gob = Instantiate(prefab) as GameObject;
			gob.transform.SetParent(content.transform);
			var item = gob.transform.GetChild(0).GetComponent<Text>();
			item.text = playerPart.transform.GetChild(i).name;
			gob.name = item.text;
			gob.GetComponent<Button>().onClick.RemoveAllListeners();
			gob.GetComponent<Button>().onClick.AddListener(() =>
			{
				for (int j = 0; j < content.transform.childCount; j++)
				{
				  content.transform.GetChild(j).GetChild(1).gameObject.SetActive(false);
				}
				var g = gob.transform.GetChild(1).gameObject;
				g.SetActive(true);
				g.GetComponent<ClothPartPosUIForm>().modelPartName = gob.name;
			});
		}
	}
}
