using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Hero : MonoBehaviour
{
  
    //public Zone currentLocation;
    private int _ignus  = 0;
    private int _payout = 0; 
    private int _power =0;
    private int _base =0;
    private int _greed = 0;
    private List<DieHandler> _assignDie;
    private List<DieHandler> _greedDie;
    private List<DieHandler> _powerDie;


    public enum DieType
    {
        Greed,
        Power,
        Assign
    }

    private void Awake()
    {
        _assignDie = new List<DieHandler>();
    }

    public Hero CreateHero(string heroName, Transform zone = null)
    {
        name = heroName;
        if (!zone)
        {
            return this;
        }
        transform.SetParent(zone);
        return this;
    }

    public void CalculatePower()
    {
        _base = 0;
        _payout = 0;
        _power = 0;
        foreach (var handler in _assignDie)
        {
            _base += handler.Roll();
        }

        _payout = _base / 2;

        var temp = 0;
        foreach (var handler in _powerDie)
        {
            temp += handler.Roll();
        }

        _power = temp + _base + _ignus;
        _ignus += _payout;
    }

    public void CalculateGreed()
    {
        _greed = 0;
        foreach (var handler in _greedDie)
        {
            _greed += handler.Roll();
        }
    }

    public int Greed()
    {
        return _greed;
    }

    public void SetAssignDie(DieHandler selectedDieHandler)
    {
        Debug.Log("Adding assign die");
        _assignDie.Add(selectedDieHandler);
        
    }

    public void MoveAssignDie(Transform transform)
    {
        Debug.Log("Count of assign die : " + _assignDie.Count);
        foreach (var dieHandler in _assignDie)
        {
            dieHandler.transform.SetParent(transform);
        }
    }

    public void AddGreedDie(DieHandler d)
    {
        _greedDie.Add(d);
    }

    public void AddPowerDie(DieHandler d)
    {
         _powerDie.Add(d);
    }
}
