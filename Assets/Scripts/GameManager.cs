using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
  [FormerlySerializedAs("Player")] [SerializeField] private Player player;
  [SerializeField] private List<BattleZone> battleZones;
  private Player _currentPlayer;
  private Player _computerPlayer;
  private Stage _currentStage;
  private Round _currentRound;
  public bool _isSetup = false;
  [FormerlySerializedAs("_battleManager")] [SerializeField] private BattleManager battleManager;
  
  public event Action<Round> RoundUpdated;
  public event Action<Stage> StageUpdated;
  

  public enum Stage
  {
    Setup,
    Preseason,
    Semi,
    Finals
  }

  public enum Round
  { 
    AssignHero,
    SelectBattle,
    Battle
  }
  
  private void Start()

  {
    if (!_isSetup)
    {
      Setup();
    }

    RoundUpdated += player.OnRoundUpdated;
    StageUpdated += player.onStageUpdated;

    ChangeStage(Stage.Setup);
    ChangeRound(Round.AssignHero);
    Turn();
  }



  private void Turn()
  {
    var calculating = true;
    while (calculating)
    {
      calculating = false;
      switch (_currentStage)
      {
        case Stage.Setup:
          switch (_currentRound)
          {
            case Round.AssignHero:
              //Fill spots
              Debug.Log("Starting Assign Heroes");
              if (IsPreSeasonFull())
              {
                ChangeStage(Stage.Preseason);
                ChangeRound(Round.SelectBattle);
              }
              calculating = true;
              break;
            default:
              Debug.Log("Bad Round fisrt");
              break;
          }

          break;
        case Stage.Preseason:
          //select fight
          switch (_currentRound)
          {
            case Round.SelectBattle:
              //Select Battle to fight
              Debug.Log("Starting Select Battle Round");
              ChangeRound(Round.Battle);
              //set _currentBattle 
              calculating = true;
              break;
            case Round.Battle:
              
              //We are here doing the damn thing
              if (!battleManager.IsStarted())
              {
                Debug.Log("Starting Battle Round");
                battleManager.StartBattle(Turn);  
              }
              else if (battleManager.IsWinner())
              {
                //BattleMap.GetNextManager().FillZone(Hero);
              }else
              {
                return;
              }

              if (battleManager.AllPassed())
                battleManager.CalculateBattle();
              break;
            default:
              Debug.Log("BadRound");
              throw new ArgumentOutOfRangeException();
          }

          //animation
          //display battle
          //resolve battle
          //check for loop
          // change season
          break;
        case Stage.Semi:
          //select fight
          //animation
          //display battle
          //check for loop
          // change season
          break;
        case Stage.Finals:
          //select fight
          //animation
          //display battle
          //resolve battle
          //check for loop
          // change season
          break;
        default:
          Debug.Log("Bad Stage");
          throw new ArgumentOutOfRangeException();
      }
    }
  }

  private bool IsBattleSelected()
  {
    return true;
  }

  private bool IsPreSeasonFull()
  {
    //return true if all the spots are picked
    return true;
  }

  private bool IsBattle()
  {
    return true;
  }

  private void Setup()
  {
    Debug.Log("Setting up the game");
    _isSetup = true;
    player.Setup();
  }

  public void Assign()
  {
    switch (_currentStage)
    {
      case Stage.Setup:
        Debug.Log("Soon");
        break;
      case Stage.Preseason:
        switch (_currentRound)
        {
          case Round.AssignHero:
            break;
          case Round.SelectBattle:
            break;
          case Round.Battle:
            if (player.CanAssign())
            {
              player.AssignDieToSelectedHero(battleManager.FillNextZone);
              
            }
            else
            {
              Debug.Log("Nope can doet");
              break;
            }

            if (battleManager.IsFull())
            {
              if (player.CanAssign())
              {
                player.AddSelectedDie();
              }
            }
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }

        break;
      case Stage.Semi:
        break;
      case Stage.Finals:
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  public void Pass()
  {
    if (_currentPlayer != player)
    {
      return;
    }

    _currentPlayer = NextPlayer();
  }

  private Player NextPlayer()
  {
    var next = _currentPlayer.nextPlayer();
    return !next ? _computerPlayer : next;
  }

  private void ChangeRound(Round round)
  {
    Debug.Log("Changinging the round: " + RoundUpdated);
    _currentRound = round;
    RoundUpdated?.Invoke(_currentRound);
  }

  private void ChangeStage(Stage stage)
  {
    _currentStage = stage;
    StageUpdated?.Invoke(stage);
  }
}
