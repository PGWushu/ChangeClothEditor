
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ButtonEventUIFrom : BaseUIForm
{
	private string exportPngath = "";
	public void Awake()
	{
		CurrentUIType.UIForms_Type = UIFormType.PopUp;
	}
	string path;
	List<TestInfo> listInfos = new List<TestInfo>();
	private void Start()
	{
		transform.FindChild("Button_Excel").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { SelectFolder("txt", "ExcelToTxt"); });
		//transform.FindChild("Button_ClothPNG").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { SelectFolder("png", "ClothPNG"); });
		//transform.FindChild("Button_BG").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { SelectFolder("png", "BG"); });
		//transform.FindChild("Button_Gestures").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { SelectFolder("png", "Gestures"); });
		transform.FindChild("Button_Model").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
		{
			UIManager.GetInstance().ShowUIForms(ProConst.RESOURECESSCRECH_UIFROM);
			var temp = UIManager.GetInstance().GetUIForms(ProConst.RESOURECESSCRECH_UIFROM);
			var var = temp.GetComponent<LoadTexture>();
			var.imgaePath = Application.streamingAssetsPath + "/Model";
			DirectoryInfo di = new DirectoryInfo(var.imgaePath);
			if (di.GetFiles().Length <= 0)
			{
				UIManager.GetInstance().ShowUIForms(ProConst.SHOWTEXT_UIFORM);
				var show = UIManager.GetInstance().GetUIForms(ProConst.SHOWTEXT_UIFORM);
				show.GetComponent<ShowTxtUIForm>().ts = "不存在人物模型图片";
				return;
			}
			StartCoroutine(var.StartLoad());
		});

		transform.FindChild("Button_Export").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
		{
			ExcelMakerManager.CreateExcelMakerManager();
			var playerPart = StartAlice.playerPart;
			for (int i = 0; i < playerPart.childCount; i++)
			{
				var clothInfo = playerPart.GetChild(i).GetComponent<ClothDressInfo>();
				TestInfo testInfo = new TestInfo();
				testInfo.dressType = (int)clothInfo.eDressType;
				testInfo.iconID = clothInfo.itemID;
				testInfo.model = clothInfo.gameObject.name;
				var pos = clothInfo.transform.localPosition;
				testInfo.modelPos = "[" + pos.x.ToString() + ", " + pos.y.ToString() + ", " + pos.z + "]";
				listInfos.Add(testInfo);
			}
			PrintExcel();
			UIManager.GetInstance().ShowUIForms(ProConst.SHOWTEXT_UIFORM);
			UIManager.GetInstance().GetUIForms(ProConst.SHOWTEXT_UIFORM).GetComponent<ShowTxtUIForm>().ts = "导出成功";
		});
	}
	/// <summary>
	/// 选择导入资源路径
	/// </summary>
	void SelectFolder(string type, string path)
	{
		System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
		if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
		{
			exportPngath = fbd.SelectedPath;
			ExportFiles(type, path);
		}
	}
	/// <summary>
	/// 把指定目录下面的所有资源拷贝到StreamingAssets（Excel,PNG)
	/// </summary>
	/// <param name="fileInfos"></param>
	/// <param name="selectPath"></param>
	void ExportFiles(string type, string path)
	{
		DirectoryInfo di = new DirectoryInfo(exportPngath);
		var fileInfos = di.GetFiles("*." + type);
		if (fileInfos.Length > 1)
		{
			di.Delete();
		}
		string localFolder = Application.streamingAssetsPath + "/" + path;
		if (fileInfos.Length <= 0)
		{
			UIManager.GetInstance().ShowUIForms(ProConst.SHOWTEXT_UIFORM);
			UIManager.GetInstance().GetUIForms(ProConst.SHOWTEXT_UIFORM).GetComponent<ShowTxtUIForm>().ts = "确认路径";
			return;
		}
		for (int i = 0; i < fileInfos.Length; i++)
		{
			string fileFullName = localFolder + "/" + fileInfos[i].Name;
			FileInfo file = new FileInfo(localFolder + "/" + fileInfos[i].Name);
			if (file.Exists) file.Delete();
			fileInfos[i].CopyTo(fileFullName);
			Debug.Log(fileFullName);
		}
		UIManager.GetInstance().ShowUIForms(ProConst.SHOWTEXT_UIFORM);
		UIManager.GetInstance().GetUIForms(ProConst.SHOWTEXT_UIFORM).GetComponent<ShowTxtUIForm>().ts = "导入成功";
	}
	string FormatPath(string path)
	{
		if (string.IsNullOrEmpty(path)) return "";
		string ret = path.Replace('\\', '/');
		ret = ret.Replace('/', '/');
		return ret;
	}
	/// <summary>
	/// 导出
	/// </summary>
	void PrintExcel()
	{
		if (!Directory.Exists(Application.streamingAssetsPath + "/Prints"))
		{
			Directory.CreateDirectory(Application.streamingAssetsPath + "/AliceExcel");
		}
		path = Application.streamingAssetsPath + "/AliceExcel/Excel_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".xls";
		ExcelMakerManager.eInstance.ExcelMaker(path, listInfos);
	}
}