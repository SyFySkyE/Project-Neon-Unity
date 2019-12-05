using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    [SerializeField] private int points = 0;
    [SerializeField] private int pointsPerKill = 100;

    public event System.Action<int, int> OnPointsChange;

    public static PlayerPoints instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        OnPointsChange(0, 0);
    }
    public void IncrementPoints()
    {
        points += pointsPerKill;
        OnPointsChange(pointsPerKill, points);
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        OnPointsChange(pointsToAdd, points);
    }

    public int GetPoints()
    {
        return this.points; 
    }

    public void SubtractPoints(int amountToSubtract)
    {
        this.points -= amountToSubtract;
        OnPointsChange(amountToSubtract, points);
    }
}
