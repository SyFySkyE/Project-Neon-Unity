using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboSystem : MonoBehaviour
{
    [SerializeField] private float timeDecayInSeconds = 5f;
    [SerializeField] private int comboMultiplier = 3;

    private int combo = 0;

    public event System.Action<int> OnComboChange;

    private void Start()
    {
        OnComboChange(combo);
    }

    public void AddToCombo()
    {
        StopAllCoroutines();
        combo++;
        OnComboChange(combo);        
        StartCoroutine(ComboDecay());
    }

    private IEnumerator ComboDecay()
    {
        yield return new WaitForSeconds(timeDecayInSeconds);
        GetComponent<PlayerPoints>().AddPoints(combo * comboMultiplier);
        Debug.Log("Last combo: " + combo);
        combo = 0;
        OnComboChange(combo);        
    }
}
