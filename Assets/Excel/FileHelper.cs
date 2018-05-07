using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class FileHelper
{
    /// <summary>
    /// Helper function -- whether the disk access is allowed.
    /// </summary>

    static public bool fileAccess
    {
        get
        {
            return Application.platform != RuntimePlatform.WindowsWebPlayer &&
                Application.platform != RuntimePlatform.OSXWebPlayer;
        }
    }

    /// <summary>
    /// Save the specified binary data into the specified file.
    /// </summary>

    static public bool Save(string fileName, byte[] bytes)
    {
#if UNITY_WEBPLAYER || UNITY_FLASH || UNITY_METRO || UNITY_WP8
		return false;
#else
        try
        {
            if (!fileAccess) return false;

            if (File.Exists(fileName))
                File.Delete(fileName);

            string strPath = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }

            FileStream file = File.Create(fileName);
            file.Write(bytes, 0, bytes.Length);
            file.Close();
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("FileHelper Save(string, byte[]) Exception: Message:" + ex.Message + " StackTrace:" + ex.StackTrace);
         Debug.LogError("FileHelper Save(string, byte[]) Exception: Message:" + ex.Message + " StackTrace:" + ex.StackTrace);
        }
        return false;
#endif
    }

    /// <summary>
    /// Load all binary data from the specified file.
    /// </summary>

    static public byte[] LoadBytes(string fileName)
    {
#if UNITY_WEBPLAYER || UNITY_FLASH || UNITY_METRO || UNITY_WP8
		return null;
#else
        try
        {
            if (!fileAccess) return null;

            if (File.Exists(fileName))
            {
                return File.ReadAllBytes(fileName);
            }
        }
        catch(Exception ex)
        {
            Debug.LogError("FileHelper LoadBytes(string) Exception: Message:" + ex.Message + " StackTrace:" + ex.StackTrace);
           Debug.LogError("FileHelper LoadBytes(string) Exception: Message:" + ex.Message + " StackTrace:" + ex.StackTrace);
        }
        return null;
#endif
    }

    static public bool Save(string fileName, string content)
    {
        if (string.IsNullOrEmpty(content))
            return true;

#if UNITY_WEBPLAYER || UNITY_FLASH || UNITY_METRO || UNITY_WP8
		return false;
#else
        try
        {
            if (!fileAccess) return false;

            if (File.Exists(fileName))
                File.Delete(fileName);

            string strPath = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }

            File.WriteAllText(fileName, content);
            return true;
        }
        catch(Exception ex)
        {
            Debug.LogError("FileHelper Save(string, string) Exception: Message:" + ex.Message + " StackTrace:" + ex.StackTrace);
          Debug.LogError("FileHelper Save(string, string) Exception: Message:" + ex.Message + " StackTrace:" + ex.StackTrace);
        }
        return false;
#endif
    }

    static public string Load(string fullPath)
    {
#if UNITY_WEBPLAYER || UNITY_FLASH || UNITY_METRO || UNITY_WP8
		return null;
#else
        try
        {
            if (!fileAccess) return null;

            if (File.Exists(fullPath))
            {
                return File.ReadAllText(fullPath);
            }
        }
        catch(Exception ex)
        {
            Debug.LogError("FileHelper Load Exception: Message:" + ex.Message + " StackTrace:" + ex.StackTrace);
           Debug.LogError("FileHelper Load Exception: Message:" + ex.Message + " StackTrace:" + ex.StackTrace);
        }
        return null;
#endif
    }

    static public bool Exists(string filename)
    {
        return File.Exists(filename);
    }
}
