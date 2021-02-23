using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private int zombieTakedowns = 0;

    private void Update()
    {
        if(zombieTakedowns >= 20)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

    public void ScoreUpdate()
    {
        zombieTakedowns++;
    }

    public void OnGUI()
    {
        GUI.contentColor = Color.yellow;
        GUI.Box(new Rect(Screen.width - 200, Screen.height - 35, 200, 80), "Points: " + zombieTakedowns);
  
    }
}
