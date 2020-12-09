using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARAnimateManager : MonoBehaviour
{
    [SerializeField] private GameObject[] lockIcons;
    [SerializeField] UnityEngine.UI.Image progressBar = null;
    [SerializeField]
    UnityEngine.UI.Text progressText = null;
    public bool isUnlocked;

    
    // Start is called before the first frame update
    void Start()
    {
        
        if (PlayerPrefs.GetInt(GlobalKey.IS_PREMIUM, 0) == 1){
            Unlock();
            
        }
    }


    public void OpenAR(int targetIndex){
        if (isUnlocked){
            // PlayerPrefs.SetInt("TargetIndex", targetIndex);
            ARAnimateDataManager.Instance.SelectedModelIndex = targetIndex;
            GtionProduction.GtionLoading.ChangeScene("Get_Texture");

            //Add Progress
            Debug.Log(PlayerPrefs.GetInt("AnimateGameProgress", 0));
            PlayerPrefs.SetInt("AnimateGameProgress", PlayerPrefs.GetInt("AnimateGameProgress", 0) + 1);
            Debug.Log(PlayerPrefs.GetInt("AnimateGameProgress", 0));

        } else {
            PremiumBuyHandler.InstantiatePremiumBuyOnScene(()=> { Unlock(); });
        }
    }

    void Unlock()
    {
        isUnlocked = true;
        foreach (GameObject lockIcon in lockIcons)        
        {
            lockIcon.SetActive(!isUnlocked);
        }

        float percentProgress = PlayerPrefs.GetInt("AnimateGameProgress", 0);

        progressBar.fillAmount = percentProgress / lockIcons.Length;
        progressText.text = Mathf.Floor(progressBar.fillAmount * 100) + "%";
        
    }
}
