using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    public Image BlackBar;

    bool startGame = true;
    float alpha;

    private void Awake()
    {
        StartGame();
    }

    public void StartGame()
    {
        BlackBar.color = Color.black;
        alpha = 1f;
    }


    public void EndGame()
    {
        startGame = false;
    }

    private void Update()
    {
        if(startGame && alpha != 0)
        {
            alpha -= Time.deltaTime * 2f;
            if(alpha < 0)
            {
                alpha = 0;
            }
            BlackBar.color = new Color(1, 1, 1, alpha);
        }
        else if(!startGame && alpha != 1)
        {
            alpha += Time.deltaTime * 2f;
            if (alpha > 1)
            {
                alpha = 1;

                GameLoader.Reload();
            }
            BlackBar.color = new Color(1, 1, 1, alpha);
        }
    }
}
