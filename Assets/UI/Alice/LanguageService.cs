using UnityEngine;
using System.Collections.Generic;

public class LanguageService
{
	static Dictionary<string, string> dataAssets = new Dictionary<string, string>();

	static private char[] SplitCharacters = { '\n', '\r', '\t', '=' };
	static bool inited = false;

	static public void Init()
	{  
			dataAssets.Clear();
		TextAsset ta = Resources.Load("Config/conf") as TextAsset;
		string[] lines = ta.text.Split('\n');
		foreach (string line in lines)
		{
			line.Trim();
			if (line.StartsWith("//") || line.StartsWith("#") || line.Length <= 0)
				continue;
			string[] elems = line.Split(SplitCharacters, System.StringSplitOptions.RemoveEmptyEntries);
			if (elems.Length != 2)
			{
				Debug.LogError("line:" + line + " is wrong!!!");
				continue;
			}
			elems[1] = elems[1].Replace("\\n", "\n");
			if (dataAssets.ContainsKey(elems[0].Trim()))
			{
				Debug.LogError("LanguageSelection Key already exists:" + elems[0].Trim());
				continue;
			}
			dataAssets.Add(elems[0].Trim(), elems[1].Trim());
		}
	}

	public static string Get(string key)
	{
		if (!Application.isPlaying)
		{
			Init();
			if (dataAssets.ContainsKey(key))
				return dataAssets[key];
			return string.Empty;
		}
		else
		{
			if (inited == false)
			{
				Init();
				inited = true;
			}
			if (dataAssets.ContainsKey(key))
				return dataAssets[key];
			return string.Empty;
		}
	}
}
