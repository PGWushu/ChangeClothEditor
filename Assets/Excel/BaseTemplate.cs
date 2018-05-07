using LibCore;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using Ionic.Zlib;
using System.IO;

public abstract class BaseTemplate
{
	public void Init(bool forceUsePacks = false)
	{
		//	string content = ((TextAsset)Resources.Load(fileName)).text;
		string content = GameObject.Find("_StartGame").GetComponent<StartAlice>().content;
		if (content == null)
		{
			Debug.LogError("Can not find table>>>>> " + content);
			return;
		}
		if (!string.IsNullOrEmpty(content))
		{
			string[] lines = content.Split("\n"[0]);
			for (int i = 0; i < lines.Length; i++)
			{
				//读取配置表
				string[] row = CSVReader.SplitCsvLine(lines[i]);
				if (row.Length > 0)
					LoadData(ref row);
			}
		}
	}

	//返回整个文本字符串
	string[] ReadAllFile(string FileName)
	{
		string[] strs;
		Debug.LogError(File.ReadAllLines(FileName));
		return strs = File.ReadAllLines(FileName);
	}
	//读取文本某一行字符串
	string ReadALine(string FileName, int linenumber) // 参数1:打开的文件(路径+文件命),参数2:读取的行数
	{
		string[] strs = File.ReadAllLines(FileName);
		if (linenumber == 0)
		{
			return "0";
		}
		else
		{
			return strs[linenumber - 1];
		}
	}
	/*重写TXT文档*/
	void WriteAllFile(string FileName, string txt)// 参数1:打开的文件(路径+文件命),参数2:重写所有文档的字符串
	{
		string[] str = txt.Split(';');
		File.WriteAllLines(FileName, str);
	}
	//在TXT文档中插入行
	void WriteALine(string FileName, string txt, int lineNumber)// 参数1:打开的文件(路径+文件命),参数2:重写某行的字符串,参数3,插入的行数
	{
		string[] str = File.ReadAllLines(FileName);
		int strLinesLength = str.Length;
		string[] strNew = new string[strLinesLength + 1];

		bool haveAddLine = false;//是否已经插入行

		for (int i = 0; i < strLinesLength + 1; ++i)
		{
			if (i == lineNumber - 1) //到达插入行,插入并跳过此次下面的添加
			{
				strNew[i] = txt;
				haveAddLine = true;
			}
			else if (!haveAddLine)//还没插入新建行时
			{
				strNew[i] = str[i];
			}
			else//插入之后
			{
				strNew[i] = str[i - 1];
			}
		}
		File.WriteAllLines(FileName, strNew);
	}
	public void ReadText()
	{
		var fileAddress = Path.Combine(Application.streamingAssetsPath, "items.txt");
		FileInfo fInfo0 = new FileInfo(fileAddress);
		string s = "";
		if (fInfo0.Exists)
		{
			StreamReader r = new StreamReader(fileAddress);
			s = r.ReadToEnd();
			Debug.LogError(s);
		}
	}
	public abstract void LoadData(ref string[] colums);
	public abstract string GetFileName();
}
