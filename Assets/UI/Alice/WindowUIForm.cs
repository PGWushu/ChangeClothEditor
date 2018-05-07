using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UI;

public class WindowUIForm : BaseUIForm
{

	[DllImport("user32.dll")]
	public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

	[DllImport("user32.dll")]
	static extern IntPtr GetForegroundWindow();

	const int SW_SHOWMINIMIZED = 2; //{最小化, 激活}  
	const int SW_SHOWMAXIMIZED = 3;//最大化  
	const int SW_SHOWRESTORE = 1;//还原  
	void Awake()
	{
		CurrentUIType.UIForms_Type = UIFormType.PopUp;
		base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
	}
	public Transform contecnt;
	private void Start()
	{
		contecnt.FindChild("Min").GetComponent<Button>().onClick.AddListener(() => 
		{
			//最小化   
			ShowWindow(GetForegroundWindow(), SW_SHOWMINIMIZED);
		});
		contecnt.FindChild("Max").GetComponent<Button>().onClick.AddListener(() =>
		{
			//最大化  
			ShowWindow(GetForegroundWindow(), SW_SHOWMAXIMIZED);
		});
		contecnt.FindChild("Close").GetComponent<Button>().onClick.AddListener(() =>
		{
			Application.Quit();
		});
	}
}
