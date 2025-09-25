using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public void Show()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
    }
}
