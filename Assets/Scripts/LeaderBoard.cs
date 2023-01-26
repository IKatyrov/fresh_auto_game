using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public sealed class LeaderBoard : MonoBehaviour
{
    private static LeaderBoard _instance;

    public TextMeshProUGUI _score;
    public Transform transofrmLabels;
    public List<TextMeshProUGUI> labels;

    public static LeaderBoard Instance
    {
        get { return _instance; }
    }

    public bool IsRegisterStep = false;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        if (labels == null || labels.Count == 0)
        {
            labels = new List<TextMeshProUGUI>();
        }

        for (int i = 0; i < transofrmLabels.childCount; i++)
        {
            Transform child = transofrmLabels.GetChild(i);
            TextMeshProUGUI textMeshProUGUI = child.GetComponent<TextMeshProUGUI>();
            labels.Add(textMeshProUGUI);
        }
        
        Hide();
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadLeaderboard());
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator LoadLeaderboard()
    {
        string uri = "http://188.120.233.93/api/v1/auth/top/";
        
        using(var www = UnityWebRequest.Get(uri))
        {
            CustomCertificateHandler  certHandler = new CustomCertificateHandler();
            www.certificateHandler = certHandler;

            yield return www.SendWebRequest();

            Result[] results = JsonConvert.DeserializeObject<Result[]>(www.downloadHandler.text);

            for (int i = 0; i < labels.Count; i++)
            {
                labels[i].text = $"{results[i].name} - {results[i].money}";
            }

            _score.text = $"Счёт {MoneyController.Instance.Coins}";
        }
       

    }
    
    [Serializable]
    public class Result
    {
        public string name;
        public int money;
    }

}

public class BypassCertificate : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        //Simply return true no matter what
        return true;
    }
} 

public class CustomCertificateHandler : CertificateHandler
{
    // Encoded RSAPublicKey
    private static readonly string PUB_KEY = "";
 
 
    /// <summary>
    /// Validate the Certificate Against the Amazon public Cert
    /// </summary>
    /// <param name="certificateData">Certifcate to validate</param>
    /// <returns></returns>
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}