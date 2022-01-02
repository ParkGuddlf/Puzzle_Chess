using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] GameObject MenuPanel;

    public void OpenMenu()
    {
        MenuPanel.SetActive(true);
    }
    public void GoTitle()
    {
        Loading.LoadScene("Title");
    }
    public void GameOver()
    {
        PlayerInfo.Money += (StageManager.instance.stageNum * 50);
        PlayerInfo.Dia += StageManager.instance.stageNum;
        PlayerInfo.instance.EndGame();
        PlayerInfo.instance.SaveINGameMoney();
        Loading.LoadScene("Title");
    }
    public void Cancle()
    {
        MenuPanel.SetActive(false);
    }
}
