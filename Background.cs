using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private Sprite bgColorido;
    [SerializeField] private Sprite bgPB;

    public void BackgroundPB()
    {
        gameObject.GetComponent<Image>().sprite = bgPB;
    }

    public void BackgroundColorido()
    {
        gameObject.GetComponent<Image>().sprite = bgColorido;
    }
}
