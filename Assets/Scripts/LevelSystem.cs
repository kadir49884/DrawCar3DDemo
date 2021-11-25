using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelSystem : MonoBehaviour
{

    private int levelCount = 1;

    [SerializeField]
    private List<GameObject> levels =  new List<GameObject>();

    private static LevelSystem instance = null;
    public static LevelSystem Instance { get => instance; set => instance = value; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        levelCount = PlayerPrefs.GetInt(Statics.PREF_LEVELINFO, 1);

        Debug.Log(levelCount);

        if (levelCount == 1)
        {
            levels[levelCount-1].gameObject.SetActive(true);
        }
        else if(levelCount == 3)
        {
            levels[levelCount - 2].gameObject.SetActive(true);
        }
    }



    public void LevelUp()
    {
        levelCount++;
        if(levelCount > 3)
        {
            levelCount = 1;
        }
        PlayerPrefs.SetInt(Statics.PREF_LEVELINFO,levelCount);
    }


}