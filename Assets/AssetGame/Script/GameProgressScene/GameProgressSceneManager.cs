﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressSceneManager : MonoBehaviour
{
    static GameProgressSceneManager _Main = null;
    public static GameProgressSceneManager Main {
        get {
            return _Main;
        }
        set {
            _Main = value;
        }
    }

    [SerializeField] ItemHandler item = null;
    [SerializeField] Transform itemContainer = null;
    [SerializeField] UnityEngine.UI.Image progressBar = null;
    [SerializeField]
    UnityEngine.UI.Text progressText = null;

    ItemHandler[] items = null;

    // Start is called before the first frame update
    void Start()
    {
        Main = this;

        //Loading Data
        string saveDataString = PlayerPrefs.GetString(GlobalKey.GAME_PROGRESS, "");
        if (saveDataString == "") {
            DataSaveHelper helper = new DataSaveHelper();
            //helper.unlock[0] = true;
            saveDataString = JsonUtility.ToJson(helper);
            PlayerPrefs.SetString(GlobalKey.GAME_PROGRESS, saveDataString);
        }

        DataSaveHelper saveData = JsonUtility.FromJson<DataSaveHelper>(saveDataString);
        float percentProgress = 0;
        items = new ItemHandler[saveData.clears.Length];

        bool autoUnlock = true;
        for (int i = 0; i < items.Length; i++) {
            
            items[i] = Instantiate(item , itemContainer);
            items[i].Init(i, saveData.clears[i] , autoUnlock && i<1);

            autoUnlock = false;
            if (saveData.clears[i])
            {
                percentProgress++;
                autoUnlock = true;
            }
        }

        progressBar.fillAmount = percentProgress/ items.Length;
        progressText.text = Mathf.Floor(progressBar.fillAmount * 100) + "%";


    }



    public void SaveProgress() {
        float percentProgress = 0;
        DataSaveHelper saveData = new DataSaveHelper();

        bool autoUnlock = false;
        bool premiumUser = PlayerPrefs.GetInt(GlobalKey.IS_PREMIUM, 0) == 1;
        for (int i = 0; i < saveData.clears.Length; i++) {
            saveData.clears[i] = items[i].isCleared;

            if ( (autoUnlock && i<3)|| premiumUser) {
                items[i].isUnlocked = true;
                autoUnlock = false;
            }

            if (items[i].isCleared)
            {
                percentProgress++;
                autoUnlock = true;
            }
        }
        
        progressBar.fillAmount = percentProgress / items.Length;
        progressText.text = Mathf.Floor(progressBar.fillAmount * 100) + "%";

        PlayerPrefs.SetString(GlobalKey.GAME_PROGRESS, JsonUtility.ToJson(saveData) );
    }

    public bool isPreviousItemCleared(GameWords words) {
        if (words == GameWords.Alif)
            return true;

        int idx = (int)words;
        return items[idx-1].isCleared;
    }

    public void BackToTracingGameProgressScene()
    {
        GtionProduction.GtionLoading.ChangeScene("TracingGameProgressScene");
    }


}

public class DataSaveHelper{
    public bool[] clears = new bool[29];
    
    
}
