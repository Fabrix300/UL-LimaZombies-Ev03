using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUIComponent : MonoBehaviour
{
    public void ToggleActive()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
