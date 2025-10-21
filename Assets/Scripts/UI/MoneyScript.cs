using TMPro;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    private TextMeshProUGUI moneyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
        moneyText.text = "$" + 0;
    }

    // Update is called once per frame
    void Update()
    {
        float total = GameManager.GetCurrentLevelMoney();
        moneyText.text = "$" + total.ToString();


    }
}
