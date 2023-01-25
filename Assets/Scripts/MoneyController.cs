using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public int Coins { get => _coins;  }

    private int _coins;
    [SerializeField] private TextMeshProUGUI coinsCount;

    public static MoneyController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCoin()
    {
        _coins++;

        coinsCount.text = $"x {_coins}";
    }
}
