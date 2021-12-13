using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    static string nextScene;

    [SerializeField] Image prograssBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }
    void Start()
    {
        StartCoroutine(fakeLoad(Random.Range(0.2f,0.7f)));
        StartCoroutine(LoadSceneProcess());
    }
    IEnumerator fakeLoad(float i)
    {
        while (prograssBar.fillAmount < i)
        {
            yield return null;
            prograssBar.fillAmount = Mathf.Lerp(prograssBar.fillAmount, 0.9f, 0.1f);            
        }

    }
    IEnumerator LoadSceneProcess()
    {

        yield return new WaitForSeconds(0.2f);
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float time = 0f;

        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                prograssBar.fillAmount = op.progress;
            }
            else
            {
                time += Time.unscaledDeltaTime;
                prograssBar.fillAmount = Mathf.Lerp(0.9f, 1f, time);
                if (prograssBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

}
