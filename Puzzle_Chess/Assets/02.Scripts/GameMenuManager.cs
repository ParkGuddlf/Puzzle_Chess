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
    public void Cancle()
    {
        MenuPanel.SetActive(false);
    }
}
