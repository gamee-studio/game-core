using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActive : MonoBehaviour
{
    [SerializeField] GameObject active;
    [SerializeField] GameObject deactive;

    public void UpdateUI(bool isActive)
    {
        active.gameObject.SetActive(isActive);
        deactive.gameObject.SetActive(!isActive);
    }
    public void Active() 
    {
        active.gameObject.SetActive(true);
        deactive.gameObject.SetActive(false);
    }

    public void Deactive() 
    {
        active.gameObject.SetActive(false);
        deactive.gameObject.SetActive(true);
    }
}
