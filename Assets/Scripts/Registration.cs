using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class Registration : MonoBehaviour
{
    private static Registration _instance;

    public TMP_InputField famlia;
    public TMP_InputField name;
    public TMP_InputField otchestvo;
    public TMP_InputField phone;
    public TMP_InputField city;
    public TMP_InputField email;

    public Button registrationButton;

    public static Registration Instance => _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void Register()
    {
        StartCoroutine(RegisterRoutine());
    }

    private IEnumerator RegisterRoutine()
    {
        System.Random random = new System.Random();
        var randomValue = random.Next(ushort.MinValue, ushort.MaxValue);

        string mailValue = $"{email.text ??= ""}{randomValue}@mail.ru";
        
        WWWForm form = new WWWForm();
        form.AddField("name", name.text);
        form.AddField("phone", phone.text);
        form.AddField("city", city.text);
        form.AddField("email", email.text);
        form.AddField("money", 0.ToString());
        form.AddField("password", $"{randomValue}PassPassPass12");
        form.AddField("re_password", $"{randomValue}PassPassPass12");

        string uri = "http://188.120.233.93/api/v1/auth/users/";

        WWW w = new WWW(uri, form);

        yield return w;

        if (!string.IsNullOrEmpty(w.error))
        {
            Debug.Log(w.error);
        }
        else
        {
            Obstacle.IsWorking = true;
            Hide();
        }

       

    }

}