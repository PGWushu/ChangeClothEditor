using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScrollMove : MonoBehaviour
{
	static ScrollMove instance;
	public static ScrollMove Instance {
		get {
			if (instance == null)
				instance = new ScrollMove();
			return instance;
		}
	}
	public RectOffset padding;
	public Vector2 spacing;

	public Transform grid;
	public ScrollRect scrollRect;
	public GameObject prefab;
	private List<GameObject> prefabsList = new List<GameObject>();
	private float itemHeight = 40;
	private int uIndex = 0;
	private int bIndex = 5;
	public int itemcount = 15;
	float lastYpos = 0;
	private int lastMoveIndex = 0;
	RectTransform gridRec;
	int instantiateCount = 0;
	private void Awake()
	{
		 
	}
	private void Start()
	{
		UpdateView();
	}
	/// <summary>
	/// 更新列表
	/// </summary>
	/// <returns></returns>
	public void UpdateView()
	{
		itemHeight = prefab.GetComponent<RectTransform>().sizeDelta.y + spacing.y;
		//instantiateCount = Mathf.CeilToInt(scrollRect.GetComponent<RectTransform>().sizeDelta.y / itemHeight) + 1;
		gridRec = grid.GetComponent<RectTransform>();
		scrollRect.onValueChanged = new ScrollRect.ScrollRectEvent();
		scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
		Rect rec = gridRec.rect;
		gridRec.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, itemcount * itemHeight);
		initScrollView();
	}
	void initScrollView()
	{
		for (int i = 0; i < instantiateCount; i++)
		{
			GameObject gob = Instantiate(prefab, grid) as GameObject;
			gob.transform.localPosition = new Vector3(0, -i * itemHeight, 0);
			gob.transform.FindChild("Text").GetComponent<Text>().text = "" + i;
			gob.name = "item" + i;
			prefabsList.Add(gob);
		}
		lastMoveIndex = Mathf.FloorToInt(Mathf.Abs(gridRec.anchoredPosition.y / itemHeight));
	}

	void OnScrollValueChanged(Vector2 vec)
	{
		Vector2 gridPos = gridRec.anchoredPosition;
		int curMoveIndex = Mathf.FloorToInt(Mathf.Abs(gridPos.y / itemHeight));// 获取移动的格数
		int offset = Mathf.Abs(curMoveIndex - lastMoveIndex);//偏移的格数
		for (int i = 0; i < offset; i++)
		{
			if (gridRec.anchoredPosition.y > 0 && gridPos.y - lastYpos > 0 && bIndex < itemcount - 1)
			{//max num is 15 rows,向上滑动
				uIndex++;
				bIndex++;
				UpdateList(true);
			}
			else if (uIndex > 0 && gridPos.y - lastYpos < 0 && (gridRec.anchoredPosition.y + itemHeight * 6) < itemHeight * itemcount)
			{
				Debug.Log((gridRec.anchoredPosition.y + itemHeight * 6) + "--1--" + itemHeight * itemcount);
				uIndex--;
				bIndex--;
				UpdateList(false);
			}
			else
			{
				Debug.Log((gridRec.anchoredPosition.y + itemHeight * 5) + "--2--" + itemHeight * itemcount);
			}
		}

		lastYpos = gridPos.y;
		lastMoveIndex = curMoveIndex;

	}

	void UpdateList(bool isUp)
	{
		int index = 0;
		if (!isUp)
		{
			//向下滑动
			index = prefabsList.Count - 1;
			GameObject gob = prefabsList[index];
			gob.transform.FindChild("Text").GetComponent<Text>().text = "" + (uIndex);
			gob.transform.SetAsFirstSibling();
			prefabsList.RemoveAt(index);
			prefabsList.Insert(0, gob);
			Debug.Log("uIndex=" + uIndex);
			gob.transform.localPosition = new Vector3(0, -uIndex * itemHeight, 0);

		}
		else
		{
			//向上滑动
			GameObject gob = prefabsList[index];
			//          Debug.Log ("bIndex=" + bIndex+"index="+index);
			gob.transform.FindChild("Text").GetComponent<Text>().text = "" + (bIndex);

			prefabsList.RemoveAt(index);
			prefabsList.Add(gob);
			gob.transform.SetAsFirstSibling();
			gob.transform.localPosition = new Vector3(0, -bIndex * itemHeight, 0);
		}

	}

	void init()
	{
		RectTransform prefabRect = prefab.GetComponent<RectTransform>();
		prefabRect.pivot = new Vector2(0.5f, 1);
		RectTransform gridRect = prefab.GetComponent<RectTransform>();
		gridRect.pivot = new Vector2(0.5f, 1);
		gridRec.anchoredPosition = new Vector2(gridRec.anchoredPosition.x, 0);
	}
	private void OnDestroy()
	{
		itemcount = 0;
	}
}