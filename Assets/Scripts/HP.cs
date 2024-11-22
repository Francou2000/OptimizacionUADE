using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
   [SerializeField] Slider slider;

   public void updateHP(int HP)  {
    slider.value = HP; 
   }
}
