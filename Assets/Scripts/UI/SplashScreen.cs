using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    private CanvasGroup canvas;
    private AsyncOperation loadInformation;

    private void Start()
    {
        canvas = GetComponentInChildren<CanvasGroup>();
        canvas.alpha = 0.0f;
        canvas.TweenAlpha(1, 1.0f).SetDelay(1.0f).SetEase(Ease.QuadIn);
        canvas.TweenAlpha(0,1.0f).SetDelay(3.0f);
        canvas.transform.TweenScale(Vector3.one * 1.1f, 3.0f).SetReversed(true).SetDelay(1.0f).SetEase(Ease.Linear).OnComplete(transitionScene);
        loadMenu();
    }

    private void loadMenu()
    {
        loadInformation = SceneManager.LoadSceneAsync("Menu Stage", LoadSceneMode.Additive);
        loadInformation.allowSceneActivation = false;
    }

    private void transitionScene()
    {
        loadInformation.allowSceneActivation = true;
        canvas.gameObject.SetActive(false);
        StartCoroutine(SetActiveSceneWhenReady());   
    }

    private IEnumerator SetActiveSceneWhenReady()
    {
        while(!loadInformation.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu Stage"));
        FindObjectOfType<Music>().enabled = true; 
    }
}
