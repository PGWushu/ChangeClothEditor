using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTxtUIForm : BaseUIForm
{ 
	Text t;
	public string ts;
	public void Awake()
	{
		CurrentUIType.UIForms_Type = UIFormType.PopUp;
		t = this.transform.FindChild("TS/Text").GetComponent<Text>();
	}
	private void Start()
	{
		StopAllCoroutines();	
		StartCoroutine(Init());
	}
	IEnumerator Init()
	{
		t.text = ts;
		yield return new WaitForSeconds(2);
		UIManager.GetInstance().CloseUIForms(ProConst.SHOWTEXT_UIFORM);
	}
	private void OnEnable()
	{
		StopAllCoroutines();
		StartCoroutine(Init());
	}
}
