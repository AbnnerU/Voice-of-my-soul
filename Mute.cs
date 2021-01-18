using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mute : MonoBehaviour
{
    [SerializeField] private Sprite mute;
    [SerializeField] private Sprite desmute;
    [SerializeField] private Image imagem;
    public void Mutar()
    {
        if (PlayerSave.GetMute() == 1)
        {
            imagem.sprite = mute;
            PlayerSave.SetMute(0);
        }
        else
        {
            imagem.sprite = desmute;
            PlayerSave.SetMute(1);
        }
        AudioListener.volume = PlayerSave.GetMute();
    }
}
