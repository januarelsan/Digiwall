using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARHijaiyahDataManager : Singleton<ARHijaiyahDataManager>
{

    // [SerializeField] private GameObject[] imageTargets;

    void Start(){
        
    }
    public int SelectedHijaiyahIndex
	{
		get { return PlayerPrefs.GetInt("SelectedHijaiyahIndex",0);}
		set { PlayerPrefs.SetInt("SelectedHijaiyahIndex",value);}
	}


}
