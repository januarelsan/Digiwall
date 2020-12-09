using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#pragma warning disable 0219
#endif

public class RenderTextureCamera : MonoBehaviour
{
	[Space(20)]
	public int TextureResolution = 512;

    private string screensPath;
    private int TextureResolutionX;
	private int TextureResolutionY;

	private Camera Render_Texture_Camera;
	private RenderTexture CameraOutputTexture;

    [SerializeField] private Image resultImage;
    [SerializeField] private Image maskImage;
    [SerializeField] private Sprite[] maskSprites;

    private string path;

    private int modelIndex;

    void Awake(){
        string targetName = PlayerPrefs.GetString("TargetName", "bebek");
        path = Application.persistentDataPath + "/" + targetName + ".png";
        
    }

    public void SetModelIndex(int index){
        modelIndex = index;
    }

    public RenderTexture GetRenderTexture()
    {
        return CameraOutputTexture;
    }

	void Start() 
	{
		Render_Texture_Camera = GetComponent<Camera>();
		StartRenderingToTexture();
	}

    void StartRenderingToTexture()      // Note: RenderTexture will be delayed by one frame
    {
        if (transform.lossyScale.x >= transform.lossyScale.y)
        {
            TextureResolutionX = TextureResolution;
            TextureResolutionY = (int)(TextureResolution * transform.lossyScale.y / transform.lossyScale.x);
        }

        if (transform.lossyScale.x < transform.lossyScale.y)
        {
            TextureResolutionX = (int)(TextureResolution * transform.lossyScale.x / transform.lossyScale.y);
            TextureResolutionY = TextureResolution;
        }

        if (CameraOutputTexture)
        {
            Render_Texture_Camera.targetTexture = null;
            CameraOutputTexture.Release();
            CameraOutputTexture = null;
        }

        CameraOutputTexture = new RenderTexture(TextureResolutionX, TextureResolutionY, 0);
        CameraOutputTexture.Create();
        Render_Texture_Camera.targetTexture = CameraOutputTexture;

        if (transform.parent) gameObject.layer = transform.parent.gameObject.layer;
        Render_Texture_Camera.cullingMask = 1 << gameObject.layer;
    }
	
    public void MakeScreen() 
	{
        StartRenderingToTexture();  // Restart

        if (screensPath == null) 
		{
            #if UNITY_ANDROID && !UNITY_EDITOR
			screensPath = Application.temporaryCachePath;       // Also you can create a custom folder, like: screensPath = "/sdcard/DCIM/RegionCapture";

            #elif UNITY_IPHONE && !UNITY_EDITOR
            screensPath = Application.temporaryCachePath;       // Also you can use persistent DataPath on IOS: screensPath = Application.persistentDataPath;

            #else
            screensPath = Application.dataPath + "/Screens";    // Editor Mode

            #endif
            if (!Directory.Exists(screensPath))
            Directory.CreateDirectory(screensPath);
        }
        StartCoroutine(TakeScreen());
    }

    private IEnumerator TakeScreen() 
	{
        yield return new WaitForEndOfFrame();

        Texture2D FrameTexture = new Texture2D(CameraOutputTexture.width, CameraOutputTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = CameraOutputTexture;
        FrameTexture.ReadPixels(new Rect(0, 0, CameraOutputTexture.width, CameraOutputTexture.height), 0, 0);
        RenderTexture.active = null;

        FrameTexture.Apply();
		saveImg(FrameTexture.EncodeToPNG());

        resultImage.sprite = Sprite.Create(FrameTexture,new Rect(0,0, FrameTexture.width, FrameTexture.height) ,new Vector2(0,0), .01f);
        maskImage.sprite = maskSprites[ARAnimateDataManager.Instance.SelectedModelIndex];
        // resultImage.SetNativeSize();
    }

    private string saveImg(byte[] imgPng)
    {

        // string fileName = screensPath + "/screen_" + System.DateTime.Now.ToString("dd_MM_HH_mm_ss") + ".png";
        string targetName = "";
        // string targetName = PlayerPrefs.GetString("TargetName", "bebek");
        switch (ARAnimateDataManager.Instance.SelectedModelIndex)
        {
            case 0:
                targetName = "bebek";
                break;
            case 1:
                targetName = "cacing";
                break;
            case 2:
                targetName = "kapal";
                break;
            case 3:
                targetName = "kereta";
                break;
            case 4:
                targetName = "kupu";
                break;
            case 5:
                targetName = "mobil";
                break;
            case 6:
                targetName = "paus";
                break;
            case 7:
                targetName = "pesawat";
                break;
            case 8:
                targetName = "rocket";
                break;
            case 9:
                targetName = "gajah";
                break;
            
            default:
                break;
        }

        Debug.Log("Target Name to Save " + targetName);
        Debug.Log("Model Index " + modelIndex);

        path = Application.persistentDataPath + "/" + targetName + ".png";
        string fileName = path;

        Debug.Log("write to " + fileName);

        File.WriteAllBytes(fileName, imgPng);

		#if UNITY_EDITOR
		AssetDatabase.Refresh();
		#endif

        return fileName;
    }
}