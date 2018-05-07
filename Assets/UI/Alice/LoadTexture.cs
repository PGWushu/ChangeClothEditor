using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadTexture : MonoBehaviour
{
	static LoadTexture instance;
	public static LoadTexture Instance {
		get {
			if (instance == null)
				instance = new LoadTexture();
			return instance;
		}
	}
	private GameObject go;
	private StartAlice startAlice;
	private void Start()
	{
		go = StartAlice.playerPart.parent.FindChild("Model").gameObject;
		startAlice = GameObject.Find("_StartGame").GetComponent<StartAlice>();
	}
	public string imgaePath;
	public RectTransform content;
	public List<Texture2D> textures;

	public IEnumerator StartLoad()
	{
		content.gameObject.GetComponent<Carousel>().enabled = false;
		if (content.transform.childCount > 0)
		{
			for (int i = 0; i < content.transform.childCount; i++)
			{
				DestroyObject(content.transform.GetChild(i).gameObject);
			}
		}

		Application.runInBackground = true;
		StartCoroutine(LoadTextures());
		yield return new WaitForEndOfFrame();

	}
	IEnumerator LoadTextures()
	{
		textures = new List<Texture2D>();
		DirectoryInfo di = new DirectoryInfo(imgaePath);
		var files = di.GetFiles("*.png");
		foreach (var file in files)
		{
			yield return LoadTextureAsync(file.FullName, AddLoadTextureToCollection);
		}
		StartCoroutine(CreateImages());
	}
	void AddLoadTextureToCollection(Texture2D texture)
	{
		textures.Add(texture);
	}
	IEnumerator CreateImages()
	{
		var prefab = content.parent.GetChild(0).gameObject;
		for (int i = 0; i < textures.Count; i++)
		{
			int j = i;
			GameObject imageObject = Instantiate(prefab, prefab.transform.localPosition, Quaternion.identity);
			imageObject.transform.SetParent(content);
			imageObject.transform.name = textures[i].name;
			imageObject.transform.FindChild("Text").GetComponent<Text>().text = textures[i].name;
			var s = imageObject.GetComponent<Image>();
			imageObject.transform.localPosition = Vector3.zero;
			imageObject.SetActive(true);
			s.sprite = Sprite.Create(textures[i], new Rect(0, 0, textures[i].width, textures[i].height), Vector2.zero);
			var btn = imageObject.GetComponent<Button>();
			btn.onClick.AddListener(
				 delegate ()
				 {
					 OnChangePlayerModel(textures[j]);
				 }
				 );
		}
		yield return new WaitForEndOfFrame();
		content.gameObject.GetComponent<Carousel>().enabled = true;
		content.gameObject.GetComponent<Carousel>().AutoLoop = true;
	}

	IEnumerator LoadTextureAsync(string originaFileName, Action<Texture2D> result)
	{
		string fileToLoad = GetCleanFileName(originaFileName);
		WWW www = new WWW(fileToLoad);
		yield return www;
		Texture2D loadTexture = new Texture2D(1, 1);
		loadTexture.name = Path.GetFileNameWithoutExtension(fileToLoad);
		www.LoadImageIntoTexture(loadTexture);
		result(loadTexture);
	}
	private static string GetCleanFileName(string originalFileName)
	{
		string fleToLoad = originalFileName.Replace("\\", "/");
		if (!fleToLoad.StartsWith("http"))
		{
			fleToLoad = string.Format("file://{0}", fleToLoad);
		}
		return fleToLoad;
	}
	void OnChangePlayerModel(Texture2D texture2D)
	{
		SpriteRenderer spr = go.GetComponent<SpriteRenderer>();
		spr.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
	}
}