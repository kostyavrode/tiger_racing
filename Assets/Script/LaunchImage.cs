using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchImage : MonoBehaviour
{
    public GameObject onj;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Launch"))
        {
            onj.SetActive(true);
            PlayerPrefs.SetString("Launch", "fg");
        }
    }
}
