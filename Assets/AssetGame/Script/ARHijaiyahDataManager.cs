using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARHijaiyahDataManager : Singleton<ARHijaiyahDataManager>
{

    // [SerializeField] private GameObject[] imageTargets;

    void Start(){
        // for (int i = 0; i < imageTargets.Length; i++)
        // {
        //     if(imageTargets[i].GetComponent<ItemTarget>().GetIndex() != ARHijaiyahDataManager.Instance.SelectedHijaiyahIndex){
        //         imageTargets[i].SetActive(false);
        //     }
        // }
    }
    public int SelectedHijaiyahIndex
	{
		get { return PlayerPrefs.GetInt("SelectedHijaiyahIndex",0);}
		set { PlayerPrefs.SetInt("SelectedHijaiyahIndex",value);}
	}


}
