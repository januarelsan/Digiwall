using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

namespace GtionProduction
{

    public class GtionLoading : MonoBehaviour
    {
        static GtionLoading _loading;
        public static GtionLoading loading
        {
            get
            {
                if (_loading == null)
                {
                    GameObject prefab = Resources.Load<GameObject>("General/CanvasLoading");

                    GameObject temp = Instantiate(prefab);
                    _loading = temp.GetComponent<GtionLoading>();

                    DontDestroyOnLoad(_loading.gameObject);

                }
                return _loading;
            }
            set
            {
                _loading = value;
                //DontDestroyOnLoad(_loading.gameObject);
            }
        }

        public static bool isOpening
        {
            get { return  loading.isOpen; }
        }

        [Header("Loading Landscape")]
        public Text loadingText = null;
        public Image loadingBar = null;
        [Tooltip("The Animation Name Should be 'OpenLoading' and 'HideLoading'")]
        public Animation anim = null;

        
        [Header("Loading Potrait")]
        public Text loadingText2 = null;
        public Image loadingBar2 = null;
        [Tooltip("The Animation Name Should be 'OpenLoading' and 'HideLoading'")]
        public Animation anim2 = null;

        DeviceOrientation currentOrientation = DeviceOrientation.Portrait;

        public Color[] lodingBarColor = new Color[2];

        bool isOpen = false;

        UnityEngine.Events.UnityAction responBack = null;

        public static void ChangeScene(string sceneName)
        {
            if (Screen.height > Screen.width)
            {
                loading.currentOrientation = DeviceOrientation.Portrait;
            }
            else {
                loading.currentOrientation = DeviceOrientation.LandscapeLeft;
            }

            OpenLayerLoading(() =>
            {
                StartMoveScene(sceneName);
            });
        }

        public static void OpenLayerLoading(UnityEngine.Events.UnityAction responBack = null)
        {
            SetAmountLoading(0);
            if(loading.currentOrientation == DeviceOrientation.LandscapeLeft){
                loading.anim.gameObject.SetActive(true);
                loading.anim.Play("OpenLoading"); //.Play("OpenLoading");
            }else{
                loading.anim2.gameObject.SetActive(true);
                loading.anim2.Play("OpenLoading"); //.Play("OpenLoading");
            }

            loading.isOpen = true;

            if (responBack != null)
            {
                loading.InvokeResponBack(responBack, 0.7f);
            }
        }

        public static void HideLayerLoading(UnityEngine.Events.UnityAction responBack = null)
        {

            if(loading.currentOrientation == DeviceOrientation.LandscapeLeft){
                loading.anim.Play("HideLoading");
            }else{
                loading.anim2.Play("HideLoading");
            }
            loading.SetInactiveLoadingObject();
            SetAmountLoading(1);
            loading.isOpen = false;
            if (responBack != null)
            {
                loading.InvokeResponBack(responBack, 0.7f);
            }
        }

        public static void SetAmountLoading(float Amount)
        {
            Amount = Mathf.Clamp(Amount, 0.02f, 1f);
            if(loading.currentOrientation == DeviceOrientation.Portrait)
                loading.loadingBar2.fillAmount = Amount;
            else
                loading.loadingBar.fillAmount = Amount;

        }


        public void InvokeResponBack(UnityEngine.Events.UnityAction responBack, float time)
        {
            this.responBack = responBack;
            Invoke("RunResponBack", time);
        }

        void RunResponBack()
        {

            if (responBack != null)
            {
                responBack.Invoke();
                responBack = null;
            }
        }


        public void SetInactiveLoadingObject()
        {
            Invoke("SetInactive", 0.8f);
        }

        void SetInactive()
        {
            if(currentOrientation == DeviceOrientation.Portrait)
                anim2.gameObject.SetActive(false);
            else
                anim.gameObject.SetActive(false);
        }


        private void Update()
        {
            if (isOpen)
            {
                Color nextColor = Color.Lerp(lodingBarColor[0], lodingBarColor[1], (Mathf.Sin(Time.time * 5) * 0.5f) + 0.5f);
                loadingBar.color = nextColor;
            }
        }

        string nextSceneName = "";

        public static void StartMoveScene(string sceneName)
        {
            loading.nextSceneName = sceneName;
            loading.Invoke("StartMoveScene", 0.8f);
        }

        void StartMoveScene()
        {
            StartCoroutine(LoadYourAsyncScene(nextSceneName));
        }

        float currentProgress = 0;
        float velocityProgress = 0;

        IEnumerator LoadYourAsyncScene(string sceneName)
        {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            velocityProgress = 0;
            currentProgress = 0;

            string start = loadingText.text;
            string i = ".";
            int loop = 0;
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                loop--;
                if (loop <= 0)
                {
                    i += " .";
                    loop = 10;
                }

                loadingText.text = start + i;
                currentProgress = Mathf.SmoothDamp(currentProgress, asyncLoad.progress, ref velocityProgress, 0.5f);
                GtionLoading.SetAmountLoading(currentProgress);
                if (i.Length == 6)
                    i = "";

                yield return null;
            }
            loadingText.text = start;
            GtionLoading.SetAmountLoading(1);
            GtionLoading.HideLayerLoading();

        }

    }
}
