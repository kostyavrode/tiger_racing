using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VibroState
{
    OFF = 0,
    ON = 1
}
public class VibrationManager : MonoBehaviour
{
    private VibroState state;
    private bool isNeedToVibrate;
    public GameObject vibrationOnButton;
    public GameObject vibrationOffButton;
    public void Awake()
    {
        if (!PlayerPrefs.HasKey("VibroState"))
        {
            PlayerPrefs.SetInt("VibroState", 1);
            ChangeVibroState(VibroState.ON);
        }
        else if (PlayerPrefs.GetInt("VibroState") == 0)
        {
            ChangeVibroState(VibroState.OFF);
            vibrationOffButton.SetActive(true);
            vibrationOnButton.SetActive(false);
        }
        else
        {

            ChangeVibroState(VibroState.ON);
            vibrationOnButton.SetActive(true);
            vibrationOffButton.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if (isNeedToVibrate)
        {
            Handheld.Vibrate();
        }
    }
    public void ChangeVibroState(VibroState state)
    {
        this.state = state;
        if (state==VibroState.OFF)
        {
            PlayerPrefs.SetInt("VibroState", 0);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("VibroState", 1);
            PlayerPrefs.Save();
        }
    }
    public void MakeVibration(float time)
    {
        isNeedToVibrate = true;
        StartCoroutine(WaitToDeActivateVibration(time));
    }
    private IEnumerator WaitToDeActivateVibration(float time)
    {
        yield return new WaitForSeconds(time);
        isNeedToVibrate = false;
    }
}
