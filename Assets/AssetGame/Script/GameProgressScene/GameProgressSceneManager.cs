using System.Collections;
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
            saveDataString = JsonUtility.ToJson(new DataSaveHelper());
            PlayerPrefs.SetString(GlobalKey.GAME_PROGRESS, saveDataString);
        }

        DataSaveHelper saveData = JsonUtility.FromJson<DataSaveHelper>(saveDataString);
        float percentProgress = 0;
        items = new ItemHandler[saveData.progress.Length];
        for (int i = 0; i < items.Length; i++) {
            items[i] = Instantiate(item , itemContainer);
            items[i].Init(i, saveData.progress[i] , i<10);
            if (saveData.progress[i])
                percentProgress++;
        }

        progressBar.fillAmount = percentProgress/ items.Length;
        progressText.text = Mathf.Floor(progressBar.fillAmount * 100) + "%";


    }

    public void SaveProgress() {
        float percentProgress = 0;
        DataSaveHelper saveData = new DataSaveHelper();
        for (int i = 0; i < saveData.progress.Length; i++) {
            saveData.progress[i] = items[i].isCleared;
            if (items[i].isCleared)
                percentProgress++;
        }
        
        progressBar.fillAmount = percentProgress / items.Length;
        progressText.text = Mathf.Floor(progressBar.fillAmount * 100) + "%";

        PlayerPrefs.SetString(GlobalKey.GAME_PROGRESS, JsonUtility.ToJson(saveData) );
    }


}

public class DataSaveHelper{
    public bool[] progress = new bool[30];
}
