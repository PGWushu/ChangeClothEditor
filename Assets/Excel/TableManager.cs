using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TableManager
{
    static TableManager instance;
    public static TableManager Instance
    {
        get
        {
            if (instance == null)
                instance = new TableManager();
            return instance;
        }
    }

    Dictionary<string, BaseTemplate> tables = new Dictionary<string, BaseTemplate>();

    void AddTable(string name, BaseTemplate table)
    {
        if (tables.ContainsKey(name))
        {
            tables[name] = table;
        }
        else
        {
            tables.Add(name, table);  
		}
    }

    void RemoveTable(string name)
    {
        if (tables.ContainsKey(name))
        {
            tables.Remove(name);
        }
    }

    public T GetTable<T>() where T : BaseTemplate
    {
        foreach(BaseTemplate table in tables.Values)
        { 
            if (table.GetType() == typeof(T))
            {
                return table as T;
            }
        }
        return null;
    }

    public BaseTemplate GetTable(string name)
    {
        if (tables.ContainsKey(name))
        {
            return tables[name];
        }
        return null;
    }

    public void InitTable<T>() where T : BaseTemplate, new()
    {
        T t = new T();
        t.Init();
        AddTable(t.GetFileName(), t);
    }
	 
    public void InitTable(string name)
    {
        BaseTemplate table = GetTable(name);
        if (table == null)
        {
            Debug.LogError("table "+name+" never inited!!!");
            return;
        }

        Type type = table.GetType();
        BaseTemplate t = type.InvokeMember(null, BindingFlags.CreateInstance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, null, new object[0]) as BaseTemplate;
        t.Init();
        AddTable(t.GetFileName(), t);
    }

	public static Texture GetItemIcon(string name)
	{ 
		string path = Application.dataPath + name + ".bytes";
		byte[] texData = FileHelper.LoadBytes(path);
		if (texData == null)
		{ 
			texData = FileManager.LoadBin("Textures/ItemIcons/" + name);
			if (texData == null)
			{
				Debug.LogError("Can`t find Item icon! Icon name:" + name);
			}
		}
		Texture2D texIcon = new Texture2D(0, 0, TextureFormat.ARGB32, false, true);
		texIcon.filterMode = FilterMode.Bilinear;
		texIcon.wrapMode = TextureWrapMode.Clamp;
		texIcon.LoadImage(texData);
		return texIcon;
	}
}
