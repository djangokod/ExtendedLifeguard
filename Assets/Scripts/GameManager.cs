using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using SupersonicWisdomSDK;

public class GameManager : MonoBehaviour
{
    public UnityEvent winEvent;
    public UnityEvent loseEvent;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
          // Subscribe
       SupersonicWisdom.Api.AddOnReadyListener(OnSupersonicWisdomReady);
       // Then initialize
       SupersonicWisdom.Api.Initialize();
        Instance= this;


    }
    void OnSupersonicWisdomReady()
{
    print("game has started");
    SupersonicWisdom.Api.NotifyLevelStarted(0, null);
   // Start your game from this point
}
    public void LoadNextScene()
    {
        int lastSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if(lastSceneIndex >= sceneCount)
        {
            lastSceneIndex = 0;
        }
        SupersonicWisdom.Api.NotifyLevelCompleted(lastSceneIndex, null);
        SceneManager.LoadScene(lastSceneIndex);
    }
    public void LoadLastScene()
    {
        int lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SupersonicWisdom.Api.NotifyLevelFailed(lastSceneIndex, null);
        SceneManager.LoadScene(lastSceneIndex);
    }
}
