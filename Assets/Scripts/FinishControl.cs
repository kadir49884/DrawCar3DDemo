using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishControl : MonoBehaviour, IFinishInterface
{

    [SerializeField]
    private bool isPlayer;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }


    public void GameFinished()
    {
        if(isPlayer && gameManager.ExecuteGame)
        {
            LevelSystem.Instance.LevelUp();
            gameManager.GameWin();
        }
        else if(gameManager.ExecuteGame)
        {
            gameManager.GameFail();
        }

    }
}
