using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResouresScrechUIForm : BaseUIForm
{
	static ResouresScrechUIForm instance;
	public static ResouresScrechUIForm Instance {
		get {
			if (instance == null)
				instance = new ResouresScrechUIForm();
			return instance;
		}
	}
	 
	private void Awake()
	{
		CurrentUIType.UIForms_Type = UIFormType.PopUp;
		CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
	}
}
