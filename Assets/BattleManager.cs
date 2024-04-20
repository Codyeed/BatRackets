using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private bool _isWinner = false;
    private bool _started;
    private bool _allPassed = false;

    [SerializeField] private List<BattleZone> _battleZones;

    private Action _turn;

    public bool IsWinner()
    {
        return _isWinner;
    }

    public bool IsStarted()
    {
        return _started;
    }

    public void StartBattle(Action turn)
    {
        //This should take in heros and assign them
        _turn = turn;
        _started = true;
        _turn?.Invoke();
    }

    public bool AllPassed()
    {
        return _allPassed;
    }

    public void CalculateBattle()
    {
        _turn?.Invoke();
    }

    public void FillNextZone(Hero hero)
    {
        foreach (var battleZone in _battleZones)
        {
            if (!battleZone.HasHero())
            {
                battleZone.SetHero(hero);
                battleZone.MoveAssignDie();
                return;
            }
            
        }
        Debug.LogError("We should only call this when we have room for a zone in the manager");
    }

    public bool IsFull()
    {
        var full = false;
        foreach (var battleZone in _battleZones.Where(battleZone => !full))
        {
            full = battleZone.HasHero();
        }

        return full;
    }
}
