using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private GameObject GameManager;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject StartUI;
    [SerializeField] private GameObject mainUI;
    public void Restart(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
    public void Quit(){
        Application.Quit();
    }
    public void StartButton(){
        GamePanel.SetActive(true);
        GameManager.SetActive(true);
        StartUI.SetActive(false);
        mainUI.SetActive(true);
    }
}
