using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARHijaiyahDataManager : Singleton<ARHijaiyahDataManager>
{

    // [SerializeField] private GameObject[] imageTargets;
    [SerializeField] private Image signImage;
    [SerializeField] private Sprite signTrackedSprite;

    void Start(){
        
    }
    public void ChangeSignSprite(){
        signImage.sprite = signTrackedSprite;
    }
    public int SelectedHijaiyahIndex
	{
		get { return PlayerPrefs.GetInt("SelectedHijaiyahIndex",0);}
		set { PlayerPrefs.SetInt("SelectedHijaiyahIndex",value);}
	}


}
