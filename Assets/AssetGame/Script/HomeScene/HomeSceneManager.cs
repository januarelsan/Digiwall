using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneManager : MonoBehaviour
{
    [SerializeField] AudioClip clip = null;

    // Start is called before the first frame update
    void Start()
    {
        GtionProduction.GtionBGM.Play(clip);
    }


    public void MoveScene(string name) {
        if (name == "")
            return;

        GtionProduction.GtionLoading.ChangeScene(name);
    }

    public void OpenMarket() {
        
        PremiumBuyHandler.InstantiatePremiumBuyOnScene();
        //else
    }
  
}
