/***
 *           主题： 英雄信息显示窗体
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class HeroInfoUIForm : BaseUIForm {
		void Awake () 
        {
		    //窗体性质
            CurrentUIType.UIForms_Type = UIFormType.Fixed;  //固定在主窗体上面显示	
	}
}