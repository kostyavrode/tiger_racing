using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughScript : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DeactivateCor());
    }
    private IEnumerator DeactivateCor()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
