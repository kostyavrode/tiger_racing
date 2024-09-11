using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Gate : MonoBehaviour
{
    private LevelManager levelManager;
    [SerializeField] private GameObject effect;
    public void InitGate(LevelManager manager)
    {
        levelManager = manager;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            Debug.Log("Gate Reached");
            //effect.SetActive(true);
            transform.DOScale(0.1f, 0.3f).OnComplete(DisableGate);
            GameObject.FindGameObjectWithTag("EffectSound").GetComponent<AudioSource>().Play();

            ServiceLocator.GetService<VibrationManager>().MakeVibration(0.4f);
        }
    }
    private void DisableGate()
    {
        levelManager.DisableGate(this);
    }
    private IEnumerator WaitToDeactivateGate()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
