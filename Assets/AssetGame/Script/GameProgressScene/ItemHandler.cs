using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    GameWords word = GameWords.Alif;
    [SerializeField] UnityEngine.UI.Text textTitle = null;
    [SerializeField] GameObject lockIcon = null;
    [SerializeField] GameObject clearIcon = null;

    private int wordId;

    public bool isUnlocked {
        get
        {
            return !lockIcon.activeInHierarchy;
        }
        set
        {
            lockIcon.SetActive(!value);
            // textTitle.enabled = !value;
            //_isCleared = value;
        }
    }
    //bool _isCleared = false;
    public bool isCleared {
        get {
            return clearIcon.activeInHierarchy;
        }
        private set {
            clearIcon.SetActive(value);
            if (!value) {
                isUnlocked = false;
            }
            else
            {
                isUnlocked = true;
            }
            //_isCleared = value;
        }
    }

    public void Init(int wordId , bool isCleared , bool isUnLocked = false)
    {

        //int id = (int)word + 1;
        this.wordId = wordId;
        
        string path = "TracingData/" + (wordId + 1);
        Debug.Log(path);
        TextAsset ta = Resources.Load<TextAsset>(path);

        TracingData data = JsonUtility.FromJson<TracingData>(ta.text);
        word = (GameWords)wordId;
        textTitle.text = data.word;

        if (isCleared)
        {
            this.isCleared = true;
            //clearIcon.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt(GlobalKey.IS_PREMIUM, 0) ==1 || isUnLocked)
            Unlock();

    }

    public void PlayGame() {
        if (isUnlocked || isCleared)
        {
            //TracingGame.InstantiateGameOnScene(word, () => { SetCleared(); GameProgressSceneManager.Main.SaveProgress(); });
            ARHijaiyahDataManager.Instance.SelectedHijaiyahIndex = this.wordId;
            GtionProduction.GtionLoading.ChangeScene("ARTracingGame");
        }
        else {
            PremiumBuyHandler.InstantiatePremiumBuyOnScene(()=> { GameProgressSceneManager.Main.SaveProgress(); });
        }
    }

    public void Spawn(){
        TracingGame.InstantiateGameOnScene(word, () => { SetCleared(); GameProgressSceneManager.Main.SaveProgress(); });
    }

    // Update is called once per frame
    void SetCleared()
    {
        isCleared = true;
        //clearIcon.gameObject.SetActive(true);
        GameProgressSceneManager.Main.SaveProgress();
    }

    void Unlock()
    {
        isUnlocked = true;
        //lockIcon.gameObject.SetActive(false);
    }
}
