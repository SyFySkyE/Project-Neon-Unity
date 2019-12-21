using System.Collections;
using UnityEngine;

public class PlayerComboSystem : MonoBehaviour
{
    [SerializeField] private float timeDecayInSeconds = 5f;
    [SerializeField] private int comboMultiplier = 3;

    private int combo = 0;

    public event System.Action<int> OnComboChange;

    private PlayerPoints playerPoints;

    private void Start()
    {
        playerPoints = GetComponent<PlayerPoints>();
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
        playerPoints.AddPoints(combo * comboMultiplier);
        combo = 0;
        OnComboChange(combo);
    }
}
