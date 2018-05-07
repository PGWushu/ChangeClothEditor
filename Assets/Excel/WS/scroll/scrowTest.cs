//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;
//enum aItemType
//{
//	One,
//	Two,
//	Three,
//	Four
//}
//public class scrowTest : MonoBehaviour {
//	public GameObject itemPrefab;
//	public ScrollRect scroll;
//	public GridLayoutGroup grid;
//	public int AllCount=30;
//	public Button btn1,btn2,btn3,btn4;
//	private ItemType iType;
//	private int listCount=0;
//	private List<GameObject> itemList = new List<GameObject> ();
//	public Image img;
//	// Use this for initialization
//	void Start () {
//		iType = aItemType.One;
////		initScrollItem ();
//		btn1.onClick.AddListener (OnBtn1Click);
//		btn2.onClick.AddListener (OnBtn2Click);
//		btn3.onClick.AddListener (OnBtn3Click);
//		btn4.onClick.AddListener (OnBtn4Click);
//		OnBtn1Click ();
//		img.transform.SetAsLastSibling ();
//	}
	
//	// Update is called once per frame
//	void Update () {
	
//	}

//	void OnBtn1Click(){
//		iType = aItemType.One;
//		listCount = 18;
//		ResetScrollList ();
//	}

//	void OnBtn2Click(){
//		iType = ItemType.Two;
//		listCount = 40;
//		ResetScrollList ();
//	}

//	void OnBtn3Click(){
//		iType = ItemType.Three;
//		listCount = 25;
//		ResetScrollList ();
//	}

//	void OnBtn4Click(){
//		iType = ItemType.Four;
//		listCount = 18;
//		ResetScrollList ();
//	}

//	void ResetScrollList(){
//		int offsetCount = listCount - itemList.Count;
//		if (offsetCount <= 0) {
////			string data;
//			for (int i = 0; i < itemList.Count; i++) {
//				GameObject itemObj = itemList [i];
//				if(itemObj!=null){
//					itemObj.GetComponent<imageItem> ().setData (iType.ToString()+"------"+i);
//					itemObj.SetActive (true);
//					string data = iType.ToString () + " this is " + i + " grid!";
//					itemObj.transform.FindChild ("btn").GetComponent<Button> ().onClick= new Button.ButtonClickedEvent();
//					itemObj.transform.FindChild ("btn").GetComponent<Button> ().onClick.AddListener (delegate() {
//						Debug.Log(data);
//					});
//				}
//			}
//			int limitCount = -offsetCount+listCount;
//			for (int i = listCount; i < limitCount; i++) {
//				itemList [i].SetActive (false);
//			}
//		} else {
			
////			string data;
//			for (int i = 0; i < itemList.Count; i++) {
//				GameObject itemObj = itemList [i];
//				if(itemObj!=null){
//					itemObj.GetComponent<imageItem> ().setData (iType.ToString()+"------"+i);
//					itemObj.SetActive (true);
//					string data = iType.ToString () + " this is " + i + " grid!";
//					itemObj.transform.FindChild ("btn").GetComponent<Button> ().onClick= new Button.ButtonClickedEvent();
//					itemObj.transform.FindChild ("btn").GetComponent<Button> ().onClick.AddListener (delegate() {
//						Debug.Log(data);
//					});
//				}

//			}

//			for (int i = 0; i < offsetCount; i++) {
//				GameObject itemObj = Instantiate (itemPrefab,grid.transform) as GameObject;
//				itemObj.GetComponent<imageItem> ().setData (iType.ToString()+"------new:"+(i));
//				itemList.Add (itemObj);
//				string data = iType.ToString () + " this is " + i + " grid!";
//				itemObj.transform.FindChild ("btn").GetComponent<Button> ().onClick= new Button.ButtonClickedEvent();
//				itemObj.transform.FindChild ("btn").GetComponent<Button> ().onClick.AddListener (delegate() {
//					Debug.Log(data);
//				});
//			}
		
//		}

//		float colums = grid.constraintCount;//分4列显示

//		int rows = Mathf.CeilToInt (listCount/colums);
//		//设置grid的大小，如果grid太小，则无法滚动，因此grid的大小需要和所有字对象的大小保持一致
//		grid.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical,(grid.cellSize.y+grid.spacing.y)*rows);
//		grid.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal,(grid.cellSize.x+grid.spacing.x)*colums);
//		scroll.verticalNormalizedPosition = 1;//初始化scroll的位置
//		scroll.horizontalNormalizedPosition=0;
//	}


//}
