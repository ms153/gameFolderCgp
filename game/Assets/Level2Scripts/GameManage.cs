using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    private int takedowns = 0;
    private int level = 1;

    private void Update()
    {
        if (takedowns == 20)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public void ScoreUpdate()
    {
        takedowns++;
    }

    public void OnGUI()
    {
        GUI.contentColor = Color.yellow;
        GUI.Box(new Rect(Screen.width - 200, Screen.height - 35, 200, 80), "Points: " + takedowns);
    }
}
