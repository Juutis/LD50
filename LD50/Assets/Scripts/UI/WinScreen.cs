using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScreen : MonoBehaviour
{
    public int Deliveries, SpoiledDeliveries, MoneyMade, MoneyLost;

    public TextMeshProUGUI DeliveriesText, SpoiledDeliveriesText, MoneyMadeText, MoneyLostText, TotalMoneyMadeText;

    public Color positiveColor, negativeColor, neutralColor;

    public bool UpdateTexts = false;

    // Start is called before the first frame update
    void Start()
    {
        SetTexts();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            SceneManager.LoadScene("Menu");
        }
        if (UpdateTexts) {
            SetTexts();
            UpdateTexts = false;
        }
    }

    void SetTexts() {
        DeliveriesText.text = "" + Deliveries;
        SpoiledDeliveriesText.text = "" + SpoiledDeliveries;
        MoneyMadeText.text = moneyText(MoneyMade);
        MoneyLostText.text = moneyText(-MoneyLost, "-");
        SpoiledDeliveriesText.color = SpoiledDeliveries > 0 ? negativeColor : neutralColor;
        MoneyLostText.color = MoneyLost > 0 ? negativeColor : neutralColor;

        var total = MoneyMade - MoneyLost;
        TotalMoneyMadeText.text = moneyText(total);
        TotalMoneyMadeText.color = total > 0 ? positiveColor : total < 0 ? negativeColor : neutralColor;
    }

    string moneyText(int value, string defaultSign = "+") {
        var sign = value > 0 ? "+" : value < 0 ? "-" : defaultSign;
        return sign + "$" + Mathf.Abs(value);
    }
}
