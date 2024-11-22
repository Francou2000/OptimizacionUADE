using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
   [SerializeField] Slider slider;
   
   public void UpdateHP(int HP)  
   { 
      //Debug.Log("hp: " + HP);
      slider.value = HP; 
   }
}
