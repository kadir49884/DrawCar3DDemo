using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<GameManager>();
				if (instance == null)
				{
					GameObject obj = new GameObject();
					obj.hideFlags = HideFlags.HideInHierarchy;
					obj.name = typeof(GameManager).Name;
					instance = obj.AddComponent<GameManager>();
					instance.WakeUp();
				}
			}
			return instance;
		}
	}

	private bool IsGameFinish { get; set; }
	private bool IsGameStarted { get; set; }

	public Action GameStart { get; set; }
	public Action GameWin { get; set; }
	public Action GameFail { get; set; }

	public bool ExecuteGame { get => IsGameStarted && !IsGameFinish; }


	private void WakeUp()
	{

		GameStart += Initialize;
		GameWin += Game_Win;
		GameFail += Game_Fail;
	}


	public void Initialize()
	{
		IsGameStarted = true;
	}

	private void Game_Win()
	{
		IsGameFinish = true;
	}
	private void Game_Fail()
	{
		IsGameFinish = true;
	}


}



