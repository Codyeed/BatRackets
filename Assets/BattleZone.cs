using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleZone : MonoBehaviour
{

    [SerializeField] private GameObject _heroHolder;
    [SerializeField] private GameObject _AssingPanel;
    private bool _isWinner = false;
    private bool _started = false;
    private Hero _hero;
    private bool _hasPassed = false;


    public void StartBattle(Action turn)
    {
        turn.Invoke();
        _started = true;
    }

    public bool IsStarted()
    {
        return _started;
    }

    public bool HasPassed()
    {
        return _hasPassed;
    }

    public void CalculateBattle(Action turn)
    {
        
        turn.Invoke();
    }

    public bool HasHero()
    {
        return _hero != null;
    }

    public void MoveAssignDie()
    {
        _hero.MoveAssignDie(_AssingPanel.transform);
    }
    
    public void Move

    public void SetHero(Hero hero)
    {
        hero.transform.SetParent(_heroHolder.transform, false);
        hero.GetComponent<RectTransform>().anchorMax = new Vector2(1,1);
        hero.GetComponent<RectTransform>().offsetMax = new Vector2(0,0);
        hero.GetComponent<RectTransform>().anchorMin = new Vector2(0,0);
        hero.GetComponent<RectTransform>().offsetMin = new Vector2(0,0);
        hero.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        _hero = hero;

    }
}
