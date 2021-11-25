using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{

    
    [SerializeField] private GameObject starterPanel;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject restartButton;

    private GameManager gameManager;

    private void Start()
    {
        gameManager =  GameManager.Instance;
        gameManager.GameWin += ThisGameWin;
        gameManager.GameFail += ThisGameFail;
    }

    

    private void ThisGameWin()
    {
        Invoke(nameof(DelayWin), 2);
    }

    private void ThisGameFail()
    {
        restartButton.SetActive(true);
    }

    private void DelayWin()
    {
        nextLevelButton.SetActive(true);
    }
    public void Starter()
    {
        gameManager.GameStart();
        starterPanel.SetActive(false);
    }
    public void ReLoad()
    {
        SceneManager.LoadScene(Statics.SCENE_GAMESCENE);
    }

}
