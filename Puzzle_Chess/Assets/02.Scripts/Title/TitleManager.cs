using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public void StartGame()
    {
        Loading.LoadScene("Game");
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void LoadGame()
    {
        PlayerInfo.instance.LoadGame();
        Loading.LoadScene("Game");
    }
    public void DeletSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
