using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ARAnimateCapture : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject mainUIObj;
    [SerializeField] private Image resultImage;
    

    private string path;

    void Awake(){
        string targetName = PlayerPrefs.GetString("TargetName", "bebek");
        path = Application.persistentDataPath + "/" + targetName + ".png";
        
    }

    void Start(){
        
    }

    // Take a shot immediately
    public void Capture(){
        StartCoroutine(_Capture());
    }

    IEnumerator _Capture()
    {
        Debug.Log("Presistent Datapath: " + Application.persistentDataPath);
        // Debug.Log("Datapath: " + Application.dataPath);
        // Debug.Log("Streaming Assets: " + Application.streamingAssetsPath);

        mainUIObj.SetActive(false);
        

        // We should only read the screen buffer after rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        
        int width = Screen.width;
        int height = width;

        Texture2D tex = new Texture2D(width, width, TextureFormat.RGB24, true);

        // Read screen contents into the texture        
        tex.ReadPixels(new Rect(0, (Screen.height/2f) - (width/2), width, height), 0, 0);
        
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Object.Destroy(tex);

        // Cropper.Instance.SetTexture(tex);

        // For testing purposes, also write to a file in the project folder
        File.WriteAllBytes(path, bytes);
        // File.WriteAllBytes(Application.persistentDataPath + "/Digiwal.png", bytes);
        
        // File.WriteAllBytes(Application.dataPath + "/Digiwal.png", bytes);
        // File.WriteAllBytes(Application.streamingAssetsPath + "/Digiwal.png", bytes);

        yield return new WaitForEndOfFrame();

        ShowResult();

    }

    public void ShowResult(){

        mainUIObj.SetActive(false);
        
        resultPanel.SetActive(true);

        byte[] pngImageByteArray = null;
        
        // string tempPath = Path.Combine(Application.persistentDataPath, "Digiwall.png");
        pngImageByteArray = File.ReadAllBytes(path);

        Texture2D tempTexture = new Texture2D(2, 2);
        tempTexture.LoadImage(pngImageByteArray);

        resultImage.sprite = Sprite.Create(tempTexture,new Rect(0,0, tempTexture.width, tempTexture.height) ,new Vector2(0,0), .01f);

        
    }
}
