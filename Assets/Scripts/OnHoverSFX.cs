using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverSFX : MonoBehaviour
{

    public AudioSource source;

   void OnMouseOver() {
       source.Play();
   }
}
