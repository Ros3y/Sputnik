using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Zigurous.DataStructures;

public class SceneLoader : SingletonBehavior<SceneLoader>
{
    public Image background;

    public Music music;

    public void StartTransition(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    private IEnumerator Transition(string sceneName)
    {
        yield return FadeIn(0.5f);

        AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    
        while(!unloadScene.isDone)
        {
            yield return null;
        }
        GlobalControl.Instance.currentLevelName = sceneName;
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while(!loadScene.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        yield return FadeOut(0.5f);
    }

    private IEnumerator FadeIn(float duration)
    {
        float elapsedTime = 0.0f;
        while(elapsedTime < duration)
        {
            float percentage = elapsedTime/duration;
            background.color = new Color(background.color.r, background.color.g, background.color.b, percentage);
            music.audioSource.volume = 1.0f - percentage;
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOut(float duration)
    {
        float elapsedTime = 0.0f;
        while(elapsedTime < duration)
        {
            float percentage = elapsedTime/duration;
            background.color = new Color(background.color.r, background.color.g, background.color.b, 1.0f - percentage);
            music.audioSource.volume = percentage;
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
