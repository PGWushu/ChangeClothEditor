using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class FileManager
{
	public static TextAsset LoadTextAsset(string path)
	{
		return (TextAsset)Resources.Load(path, typeof(TextAsset));
	}

	public static Texture2D LoadTexture(string path)
	{
		return (Texture2D)Resources.Load(path, typeof(Texture2D));
	}

	public static AudioClip LoadAssetsSound(string filename)
	{ 
		AudioClip result = new AudioClip();
		try
		{
			result = (AudioClip)Resources.Load(filename);
		}
		catch (IOException ex)
		{
			Debug.Log(ex.StackTrace);
		}
		return result;
	}

	public static byte[] LoadBin(string path)
	{
		TextAsset textAsset = (TextAsset)Resources.Load(path, typeof(TextAsset));
		if (textAsset == null)
		{
			return null;
		}
		return textAsset.bytes;
	}

	public static string LoadString(string path)
	{
		return Encoding.GetEncoding("UTF-8").GetString(FileManager.LoadBin(path));
	}

	public static string GetFilename(string url)
	{
		return Regex.Replace(url, ".*/", "");
	}

	public static string GetDirName(string url)
	{
		return Regex.Replace(url, "(.*/)(.+)", "$1");
	}
}
