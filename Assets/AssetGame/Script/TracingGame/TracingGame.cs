using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameWords {
    Alif,
    Ba,
    Ta,
    Tsa,
    Jim,
    Ha,
    Kho,//7
    Dal,
    Dzal,
    Ro,
    Za,
    Sin,
    Syin,
    Shad,
    Dhad,//15
    Tho,
    Zho,
    Ain,
    Ghain,
    Fa,
    Qaf,
    Kaf,
    Lam,
    Mim,
    Nun,
    Wau,
    ha,
    Lam_Alif,
    Hamzah,
    Ya
}

public class TracingData {
    public string word = "Fa";
    public byte[] color = null;
    public string[] music = null;
    public string objPath = "Words/20";
}

[RequireComponent(typeof(AudioSource))]
public class TracingGame : MonoBehaviour
{
    static TracingGame _Main = null;
    public static TracingGame Main {
        get {
            return _Main;
        }
        private set {
            _Main = value;
        }
    }
    public static void InstantiateGameOnScene(GameWords word , OnComplete callback = null) {
        if (Main == null)
        {
            TracingGame tempMain = Resources.Load<TracingGame>("TracingGame/TracingGame");
            
            Main = Instantiate(tempMain);
            Main.audioSource = Main.GetComponent<AudioSource>();
            Main.anim = Main.GetComponent<Animator>();
            Main.canvas.worldCamera = Camera.main;
        }

        Main.onComplete = callback;

        /*
        
        string path = "Words/" + id.ToString();
        WordActionHandler tempWord = Resources.Load<WordActionHandler>(path);
        if (Main.actionHandler != null)
            Destroy(Main.actionHandler.gameObject);

        Main.actionHandler = Instantiate(tempWord, Main.transform);
        */

        int id = (int)word + 1;
        Main.data = JsonUtility.FromJson<TracingData>(Resources.Load<TextAsset>("TracingData/" + id).text);


        //Main.StartGame();
    }

    public delegate void OnComplete();
    public OnComplete onComplete = null;
        

    //GameWords currentWord = GameWords.Alif;
    TracingData data = null;
    [SerializeField] Canvas canvas = null;
    [SerializeField] UnityEngine.UI.Text wordName = null;
    WordActionHandler actionHandler = null;
    [SerializeField] PointerFollower pointer = null;
    AudioClip[] audioClip = null;
    AudioSource audioSource = null;
    Animator anim = null;

    private void StartGame()
    {
        if (actionHandler != null)
            Destroy(actionHandler.gameObject);

        actionHandler = Instantiate(Resources.Load<WordActionHandler>(data.objPath), transform);
        wordName.text = data.word;
        wordName.color = new Color(data.color[0], data.color[1], data.color[2]);
        audioClip = new AudioClip[4] {  Resources.Load<AudioClip>(data.music[0]),
                                        Resources.Load<AudioClip>(data.music[1]),
                                        Resources.Load<AudioClip>(data.music[2]),
                                        Resources.Load<AudioClip>(data.music[3]), };

        pointer.word = actionHandler;
        actionHandler.pointer = pointer;
    }

    // Update is called once per frame
    public void GameCleared()
    {
        PlayBasic();
        anim.SetTrigger("Next");
        if(onComplete != null)
            onComplete();
    }

    public void PlayDommah() {
        audioSource.clip = audioClip[0];
        audioSource.Play();
    }
    public void PlayKasrah()
    {
        audioSource.clip = audioClip[2];
        audioSource.Play();
    }
    public void PlayFathah()
    {
        audioSource.clip = audioClip[1];
        audioSource.Play();
    }
    public void PlayBasic() {
        audioSource.clip = audioClip[3];
        audioSource.Play();
    }

    public void Close() {
        actionHandler.Hide();
        anim.SetTrigger("Close");
    }

    public void SelfDestruct()
    {
        Main = null;
        Destroy(this.gameObject);
    }

}
