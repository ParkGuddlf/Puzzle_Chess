using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
}
