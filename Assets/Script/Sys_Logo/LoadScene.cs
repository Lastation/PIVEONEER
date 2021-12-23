using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static string nextScene;

    [SerializeField] private Slider SLIDER_ProgressBar;
    [SerializeField] private Text   TEXT_ProgressBar;

    public void Load_Scene(string _name)
    {
        nextScene = _name;
        SLIDER_ProgressBar.gameObject.SetActive(true);
        StartCoroutine(Load_Scene());
    }

    IEnumerator Load_Scene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;

            SLIDER_ProgressBar.value = op.progress;
            TEXT_ProgressBar.text = ((int)(SLIDER_ProgressBar.value * 100)).ToString() + "%";

            if (op.progress >= 0.9f) SLIDER_ProgressBar.value = 1.0f;
            if (SLIDER_ProgressBar.value == 1.0f) op.allowSceneActivation = true;
        }
    }
}