using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingSceneManager : MonoBehaviour
{
    public bool loopAll = true;
    [SerializeField]GameWords words = GameWords.Alif;

    private void Start()
    {
        if (loopAll)
        {
            
            CallScene();
            words = GameWords.Alif;
        }
        else {
            TracingGame.InstantiateGameOnScene(words, () => { Debug.Log("This is CallBack"); });
        }
    }

    public void CallScene() {
        int id = (int)words + 1;
        if (id >= 28)
            return;

        words = (GameWords)(id);
        Invoke("ShowUp" , 3);
    }

    void ShowUp()
    {
        TracingGame.InstantiateGameOnScene(words, () => { Debug.Log("This is CallBack"); } , ()=> { Debug.Log("this is Next"); CallScene(); });
    }
}
