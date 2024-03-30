using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageActive : MonoBehaviour
{
    public GameObject objectToManage;

    void Start()
    {
        if (objectToManage == null)
        {
            objectToManage = gameObject;
        }
    }

    public void TurnOn()
    {
        objectToManage.SetActive(true);
    }

    public void TurnOff()
    {
        objectToManage.SetActive(false);
    }
}
