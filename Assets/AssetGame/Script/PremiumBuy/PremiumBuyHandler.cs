using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PremiumBuyHandler : MonoBehaviour
{
    
    static PremiumBuyHandler _Main = null;
    public static PremiumBuyHandler Main
    {
        get
        {
            return _Main;
        }
        private set
        {
            _Main = value;
        }
    }
    public static void InstantiatePremiumBuyOnScene(OnSuccess successCallback = null)
    {
        if (Main != null)
        {
            Destroy(Main.gameObject);
        }

        PremiumBuyHandler tempMain = Resources.Load<PremiumBuyHandler>("PremiumBuy/PremiumBuy");
        Main = Instantiate(tempMain);
        Main.onComplete = successCallback;

    }

    public delegate void OnSuccess();
    OnSuccess onComplete = null;

    
    [SerializeField] Animation anim = null;

    [SerializeField] GameObject ParentalGate = null;

    [Header("Container")]
    [SerializeField] GameObject container = null;
    [SerializeField] GameObject aktivasi = null;
    [SerializeField] GameObject sukses = null;

    [Header("Activation")]
    [SerializeField] UnityEngine.UI.Text heading = null;
    [SerializeField] UnityEngine.UI.Text description = null;
    [SerializeField]
    UnityEngine.UI.InputField input = null;
    [SerializeField] GameObject failed = null;
    [SerializeField] Color colorTry = new Color();//FD6048
    [SerializeField] Color colorFailed = new Color();

    [Header("BuyPage")]
    [SerializeField] GameObject buyPage = null;

    private void Start()
    {
        if ((PlayerPrefs.GetInt(GlobalKey.IS_PREMIUM, 0) != 1))
            OpenTryReedem();
        else
            OpenSuccessReedem();

        SetupQuest();
    }

    public void CheckVoucher(string value) {
        if (value != "generasialfa2020")
        {
            if (value == "") { 
                setTryReedem();
                return;
            }
            //failed.SetActive(true);
            setFailedReedem();
            GtionProduction.Vibration.Vibrate(200);
        }
        else {
            OpenSuccessReedem();
        }
    }

    public void CloseTab() {
        anim.Play("HidePremiumBuy");
        Destroy(gameObject, 0.6f);
    }

    public void OpenBuy() {
        container.SetActive(false);
        buyPage.SetActive(true);
    }

    public void OpenParentalGate() {
        ParentalGate.SetActive(true);        
    }

    public void OpenSuccessReedem() {
        container.SetActive(true);
        buyPage.SetActive(false);
        aktivasi.SetActive(false);
        sukses.SetActive(true);

        PlayerPrefs.SetInt(GlobalKey.IS_PREMIUM, 1);

        if (Main.onComplete != null)
        {
            Main.onComplete();
        }

    }

    public void OpenTryReedem()
    {
        container.SetActive(true);
        buyPage.SetActive(false);
        aktivasi.SetActive(true);
        sukses.SetActive(false);

        setTryReedem();
    }

    public void ChangeContentType(){
        if(input.contentType == UnityEngine.UI.InputField.ContentType.Standard)
            input.contentType = UnityEngine.UI.InputField.ContentType.Password;
        else
            input.contentType = UnityEngine.UI.InputField.ContentType.Standard;

        input.ForceLabelUpdate ();
    }

    public void setTryReedem() {
        input.text = "";
        failed.SetActive(false);
        heading.color = colorTry;
        heading.text = "Aktivasi dengan Kode Voucher";
        description.text = "Masukan kode voucher yang terlampir dalam kemasan Produk Digiwall";
    }
    public void setFailedReedem()
    {
        failed.SetActive(true);
        heading.color = colorFailed;
        heading.text = "Aktivasi Produk Gagal";
        description.text = "Kode yang anda masukan salah\natau tidak dikenal";
    }

    public void GotoLink()
    {
        Application.OpenURL("https://alfaarena.orderonline.id/App-orderdigiwall");
    }

    //Parental Gate
    [SerializeField] private Text questText = null;
    [SerializeField] private InputField answerIF = null;
    [SerializeField] private GameObject resetQuestButtonObj = null;
    private string[] numberString = new string[]{"Nol", "Satu", "Dua", "Tiga", "Empat", "Lima", "Enam", "Tujuh", "Delapan", "Sembilan"};
    private int[] questInt = new int[4];
    private List<int> answerInt = new List<int>();



    void SetupQuest(){
        questText.text = "";
        answerIF.text = "";
        for (int i = 0; i < questInt.Length; i++)
        {
            questInt[i] = Random.Range(0,numberString.Length);

            if(i < 3)
                questText.text += numberString[questInt[i]] + ", ";
            else
                questText.text += numberString[questInt[i]];
        }
    }

    public void Answer(int number){
        
        if(answerInt.Count < 4){
            answerInt.Add(number);
            answerIF.text += number + "     ";
        }
        if (answerInt.Count == 4) {     
            CheckAnswer();
        }
    }

    void CheckAnswer(){
        int wrongCount = 0;
        for (int i = 0; i < answerInt.Count; i++)
        {
            if(questInt[i] != answerInt[i]){
                resetQuestButtonObj.SetActive(true);                
                wrongCount++;
            }

        }

        if(wrongCount == 0){
            ParentalGate.SetActive(false);
        }
    }

    public void ResetQuest(){
        resetQuestButtonObj.SetActive(false);
        answerInt = new List<int>();
        answerIF.text = "";

    }

}
