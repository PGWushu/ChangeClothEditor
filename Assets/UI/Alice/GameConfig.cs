using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LangType
{
    ZHCN = 1,//简体中文
    ZHTW = 2,//台湾繁体中文
    ENUS = 3,//美国英语
}

public class GameConfig
{
    static Dictionary<string, string> dataAssets = new Dictionary<string, string>();

    static char[] SplitCharacters = { '\n', '\r', '\t', '=' };

	public static void Init () 
    {
        TextAsset ta = Resources.Load("Config/conf") as TextAsset;
        string[] lines = ta.text.Split('\n');
        foreach(string line in lines)
        {
            string key, value;
            if (GetEquationKV(line, out key, out value))
                dataAssets.Add(key, value);
        }
	}

    public static string Get(string key)
    {
        if (dataAssets.ContainsKey(key))
            return dataAssets[key];
        return "";
    }
	public static bool GetEquationKV(string line, out string key, out string value)
	{
		key = string.Empty;
		value = string.Empty;
		if (string.IsNullOrEmpty(line))
			return false;
		line.Trim();
		if (line.StartsWith("//") || line.StartsWith("#") || line.Length <= 0)
			return false;
		int idx = line.IndexOf('=');
		if (idx < 0 || idx >= line.Length)
			return false;
		key = line.Substring(0, idx).Trim().Replace("\"", "");
		value = (idx + 1 < line.Length ? line.Substring(idx + 1, line.Length - idx - 1) : "").Trim().Replace("\"", "");
		if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
			return false;
		return true;
	}
}
