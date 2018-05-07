using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerPrefsInfo
{
	public int modelPos;
	public string modelName;
};
public class PlayerPrefsManager
{
	public List<PlayerPrefsInfo> playModelInfo = new List<PlayerPrefsInfo>();
	public static PlayerPrefsManager eInstance;
	public static PlayerPrefsManager PlayerPrefsMan()
	{
		if (eInstance == null)
		{
			eInstance = new PlayerPrefsManager();
		}
		return eInstance;
	}
	public void SetPlayerPrefs(string model, string pos)
	{
		if (!PlayerPrefs.HasKey(model))
		{
			PlayerPrefs.SetString(model, pos);
		}	 
	}
}
