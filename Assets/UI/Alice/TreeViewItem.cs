using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TreeViewItem : MonoBehaviour
{
    
    public TreeViewControl Controler;
 
    public bool IsExpanding = false;
 
    private int _hierarchy = 0;
 
    private TreeViewItem _parent;
 
    private List<TreeViewItem> _children;

    void Awake()
    { 
        transform.FindChild("ContextButton").GetComponent<Button>().onClick.AddListener(ContextButtonClick);
        transform.FindChild("TreeViewButton").GetComponent<Button>().onClick.AddListener(delegate () {
            Controler.ClickItem(gameObject);
        });	
	}
	private void Start()
	{
		ContextButtonClick();
	}
	/// <summary>
	/// 点击上下文菜单按钮，元素的子元素改变显示状态
	/// </summary>
	void ContextButtonClick()
    {
        if (IsExpanding)
        {
            transform.FindChild("ContextButton").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 90);
            IsExpanding = false;
            ChangeChildren(this, false);
        }
        else
        {
            transform.FindChild("ContextButton").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
            IsExpanding = true;
            ChangeChildren(this, true);
        }

        //刷新树形菜单
        Controler.RefreshTreeView();
    }
    /// <summary>
    /// 改变某一元素所有子元素的显示状态
    /// </summary>
    void ChangeChildren(TreeViewItem tvi, bool value)
    {
        for (int i = 0; i < tvi.GetChildrenNumber(); i++)
        {
            tvi.GetChildrenByIndex(i).gameObject.SetActive(value);
            ChangeChildren(tvi.GetChildrenByIndex(i), value);
        }
    }

    #region 属性访问
    public int GetHierarchy()
    {
        return _hierarchy;
    }
    public void SetHierarchy(int hierarchy)
    {
        _hierarchy = hierarchy;
    }
    public TreeViewItem GetParent()
    {
        return _parent;
    }
    public void SetParent(TreeViewItem parent)
    {
        _parent = parent;
    }
    public void AddChildren(TreeViewItem children)
    {
        if (_children == null)
        {
            _children = new List<TreeViewItem>();
        }
        _children.Add(children);
    }
    public void RemoveChildren(TreeViewItem children)
    {
        if (_children == null)
        {
            return;
        }
        _children.Remove(children);
    }
    public void RemoveChildren(int index)
    {
        if (_children == null || index < 0 || index >= _children.Count)
        {
            return;
        }
        _children.RemoveAt(index);
    }
    public int GetChildrenNumber()
    {
        if (_children == null)
        {
            return 0;
        }
        return _children.Count;
    }
    public TreeViewItem GetChildrenByIndex(int index)
    {
        if (index >= _children.Count)
        {
            return null;
        }
        return _children[index];
    }
    #endregion
}
