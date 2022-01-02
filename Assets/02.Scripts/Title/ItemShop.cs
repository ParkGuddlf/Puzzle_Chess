using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    [SerializeField] Text money;
    [SerializeField] Text dia;

    private void Update()
    {
        money.text = PlayerInfo.Money.ToString();
        dia.text = PlayerInfo.Dia.ToString();
    }

    public void ChangeDiaMoney(string a)
    {
        switch (a)
        {
            case "Money":
                if (PlayerInfo.Money > 999)
                {
                    PlayerInfo.Money -= 1000;
                    PlayerInfo.Dia += 10;
                }
                break;
            case "Dia":
                if (PlayerInfo.Dia > 9)
                {
                    PlayerInfo.Dia -= 10;
                    PlayerInfo.Money += 1000;
                }
                break;
        }
        PlayerInfo.instance.SaveINGameMoney();
    }
}
