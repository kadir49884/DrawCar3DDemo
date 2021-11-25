using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private string levelInfo;

    void Awake()
    {

        levelInfo = Path.Combine(Statics.PREF_LEVELDATA, Statics.PREF_LEVELSTABIL + PlayerPrefs.GetInt(Statics.PREF_LEVELINFO,1));

       LevelScriptableScript levelScriptableScript = Resources.Load<LevelScriptableScript>(levelInfo);
        Instantiate(levelScriptableScript.LevelPrefab);
        
    }

}
