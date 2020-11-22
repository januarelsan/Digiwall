using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GoToScene(string name){
        GtionProduction.GtionLoading.ChangeScene(name);
    }
}
