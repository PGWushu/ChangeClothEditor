 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	public class StartProject : MonoBehaviour {

		void Start () {
            Log.Write(GetType()+"/Start()/");
            //加载登陆窗体
            UIManager.GetInstance().ShowUIForms(ProConst.LOGON_FROMS);         			
		}		
}