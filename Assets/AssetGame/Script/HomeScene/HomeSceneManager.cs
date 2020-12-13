using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneManager : MonoBehaviour
{
    [SerializeField] AudioClip clip = null;

    [Header("Mute")]
    [SerializeField] Sprite[] btnAudio = null;
    [SerializeField] UnityEngine.UI.Image btnAudioImage = null;

    // Start is called before the first frame update
    void Start()
    {
        GtionProduction.GtionBGM.Mute( PlayerPrefs.GetInt("isMuted", 0) == 1 );
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

    public void MuteUnmute() {
        int i = PlayerPrefs.GetInt("isMuted", 0);
        i = (i+1) % 2;
        PlayerPrefs.SetInt("isMuted", i);
        GtionProduction.GtionBGM.Mute( i == 1);
        btnAudioImage.sprite = btnAudio[i];
    }
  
}
