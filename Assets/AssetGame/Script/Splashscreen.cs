using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splashscreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeScene", 6);
    }

    void ChangeScene(){
        SceneManager.LoadScene("HomeScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
