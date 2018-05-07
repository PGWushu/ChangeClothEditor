using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothTypeUIForm : BaseUIForm {

	private void Awake()
	{
		CurrentUIType.UIForms_Type = UIFormType.PopUp;  //固定在主窗体上面显示			
	}
}
