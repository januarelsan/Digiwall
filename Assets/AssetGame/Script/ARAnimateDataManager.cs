using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARAnimateDataManager : Singleton<ARAnimateDataManager>
{

    // [SerializeField] private GameObject[] imageTargets;
    [SerializeField] private Button captureButton;


    void Start(){
        // for (int i = 0; i < imageTargets.Length; i++)
        // {
        //     if(imageTargets[i].GetComponent<ItemTarget>().GetIndex() != ARHijaiyahDataManager.Instance.SelectedHijaiyahIndex){
        //         imageTargets[i].SetActive(false);
        //     }
        // }
    }

    public void ActivateButton(bool value){
        captureButton.enabled = value;
    }
    public int SelectedModelIndex
	{
		get { return PlayerPrefs.GetInt("SelectedModelIndex",0);}
		set { PlayerPrefs.SetInt("SelectedModelIndex",value);}
	}


}
