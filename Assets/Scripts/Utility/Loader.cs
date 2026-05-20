using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public static Loader Instance {get; private set;}
    public event Action<float> OnLoadProgressChanged;
    
    public enum Scenes
    {
        MainMenu = 0,
        GameScene,
        LoadingScene
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Warning: multiple Loader found! Destroying duplicate!");
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public static void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene((int)Scenes.LoadingScene);
        Instance.StartCoroutine(LoadAsync(scene));
    }

    private static IEnumerator LoadAsync(Scenes scene)
    {
        yield return new WaitForSecondsRealtime(1f);
        var loadingOperation = SceneManager.LoadSceneAsync((int)scene);
        if (loadingOperation == null)
        {
            yield break;
        }
        
        while(!loadingOperation.isDone)
        {
            var progress = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            Instance.OnLoadProgressChanged?.Invoke(progress);
            yield return null;
        }
    }
}
