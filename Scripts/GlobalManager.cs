using System;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    [SerializeField] private GameObject level;

    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject deadUI;
    
    private GameObject _currentLevel;
    private void Start()
    {
        _currentLevel = Instantiate(level);
        GlobalEventsManager.OnPlayerDead.AddListener(DieHandler);
    }

    private void DieHandler(DieType type)
    {
        Time.timeScale = 0f;
        GlobalEventsManager.RemoveAllListeners();
    }
   
    private void LevelRestartListener()
    {
        Destroy(_currentLevel);
        _currentLevel = Instantiate(level);
        GlobalEventsManager.OnPlayerDead.AddListener(DieHandler);
        Time.timeScale = 1f;
        FindObjectOfType<Joystick>().ResetHeadPosition();
    }

    private void GoToMenuListener()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
}