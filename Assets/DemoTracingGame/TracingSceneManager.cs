using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingSceneManager : MonoBehaviour
{

    [SerializeField]GameWords words = GameWords.Alif;

    private void Start()
    {
        
        TracingGame.InstantiateGameOnScene(words, ()=> { Debug.Log("This is CallBack"); });
    }
}
