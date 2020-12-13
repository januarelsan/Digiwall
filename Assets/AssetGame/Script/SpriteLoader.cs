using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SpriteLoader : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private SpriteRenderer whiteRenderer;
    private SpriteRenderer resultSpriteR;
    private string path;

    void Awake(){
        resultSpriteR = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

        path = Application.persistentDataPath + "/" + fileName + ".png";

        if(File.Exists(path)){

            byte[] pngImageByteArray = null;
                        
            pngImageByteArray = File.ReadAllBytes(path);

            Texture2D tempTexture = new Texture2D(2, 2);
            tempTexture.LoadImage(pngImageByteArray);

            resultSpriteR.sprite = Sprite.Create(tempTexture,new Rect(0,0, tempTexture.width, tempTexture.height) ,new Vector2(0.5f,0.5f), 300f);
            whiteRenderer.enabled = true;
        } else {
            resultSpriteR.sprite = null;
            whiteRenderer.enabled = false;
        }

    }

    
}
