using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fadescreen: MonoBehaviour
{
    public Image BlackFade;
    void Start()
    {
        BlackFade.canvasRenderer.SetAlpha(1f);
        Fadein();

    }

    void Fadein()
    {
        BlackFade.CrossFadeAlpha(0f, 2, false);
    }
}
