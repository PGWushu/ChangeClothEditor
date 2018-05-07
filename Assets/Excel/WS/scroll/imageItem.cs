using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class imageItem : MonoBehaviour {
	public Text text;
	public Button btn;

	void Start(){
		if(btn){
//			btn.onClick.AddListener (OnBtnClick);
		}

	}
	// Use this for initialization
	public void setData(string msg){
		text.text = msg;
	}

	void OnBtnClick(){
		Debug.Log ("this is a button:"+text.text);
	
	}
}
