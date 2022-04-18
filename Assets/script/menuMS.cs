using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class menuMS : MonoBehaviour
{
    [SerializeField]
    private GameObject startB, exitB;
    void Start()
    {
        FadeOut();
    }
    void FadeOut()
    {
        startB.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
        exitB.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameLevel");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
