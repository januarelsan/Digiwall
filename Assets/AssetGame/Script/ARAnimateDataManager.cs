using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARAnimateDataManager : Singleton<ARAnimateDataManager>
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
    public int SelectedModelIndex
	{
		get { return PlayerPrefs.GetInt("SelectedModelIndex",0);}
		set { PlayerPrefs.SetInt("SelectedModelIndex",value);}
	}


}
