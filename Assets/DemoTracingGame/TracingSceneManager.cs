using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingSceneManager : MonoBehaviour
{

    private void Start()
    {
        TracingGame.InstantiateGameOnScene(GameWords.Fa , ()=> { Debug.Log("This is CallBack"); });
    }
}
