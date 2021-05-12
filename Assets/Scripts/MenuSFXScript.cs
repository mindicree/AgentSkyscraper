using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSFXScript : MonoBehaviour
{
    public AudioSource hover;
    // Start is called before the first frame update
    public void PlayHoverSFX() {
        hover.Play();
    }
}
