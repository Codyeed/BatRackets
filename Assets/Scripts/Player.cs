using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int ignus;
    [SerializeField] public List<DieHandler> _fateDie;
    
    [SerializeField] private GameObject _diePrefab;
    [SerializeField] private GameObject _heroPrefab;
    [SerializeField] public GameObject FateDieZone;
    [SerializeField] public GameObject HeroZone;
    [SerializeField] private List<DieHandler> _ignusDie;
    
    
    //UI
    [SerializeField] private IgnusPoolUI IgnusPoolUI;
    public event Action<int> IgnusUpdated;

    
    
    private bool _hasPassed = false;

    private List<Hero> _heroes;
    [SerializeField] private DieHandler _selectedDieHandler;
    [SerializeField] private Hero _selectedHero;
    [SerializeField] private BattleZone _selectedBattleZone;
    [SerializeField] private GreedPanel _selectedGreedPanel;
    private List<Hero> _deadHeroes;
    private Player _nextPlayer;

    private GameManager.Round _currentRound;
    private GameManager.Stage _currentStage;


    private void Update()
    {
        var selectedGameObject = EventSystem.current.currentSelectedGameObject;
        var hero = selectedGameObject?.GetComponent<Hero>();
        if (hero)
        {
            _selectedHero = hero;
        }
        var die = selectedGameObject?.GetComponent<DieHandler>();
        if (die)
        {
            _selectedDieHandler = die;
        }

        var greedPanel = selectedGameObject?.GetComponent<GreedPanel>();
        if (greedPanel)
        {
            _selectedGreedPanel = greedPanel;
        }
    }

    public void Setup()
    {
        Debug.Log("Setting Up Player");
        IgnusUpdated += IgnusPoolUI.UpdateIgnusUI;
        _fateDie = new List<DieHandler>()
        {
            Instantiate(_diePrefab).GetComponent<DieHandler>().CreateD4(false, FateDieZone.transform),
            Instantiate(_diePrefab).GetComponent<DieHandler>().CreateD6(false, FateDieZone.transform),
            Instantiate(_diePrefab).GetComponent<DieHandler>().CreateD8(false, FateDieZone.transform),
            Instantiate(_diePrefab).GetComponent<DieHandler>().CreateD10(false, FateDieZone.transform),
            Instantiate(_diePrefab).GetComponent<DieHandler>().CreateD12(false, FateDieZone.transform),
            Instantiate(_diePrefab).GetComponent<DieHandler>().CreateD20(false, FateDieZone.transform),
            
        };

        _heroes = new List<Hero>()
        {
            Instantiate(_heroPrefab).GetComponent<Hero>().CreateHero("Bill", HeroZone.transform),
            Instantiate(_heroPrefab).GetComponent<Hero>().CreateHero("Bob", HeroZone.transform),
            Instantiate(_heroPrefab).GetComponent<Hero>().CreateHero("Boe", HeroZone.transform),
            Instantiate(_heroPrefab).GetComponent<Hero>().CreateHero("Other One", HeroZone.transform),
        };

        ignus = 30;
        IgnusUpdated?.Invoke(ignus);
    }

    public void AssignDieToSelectedHero(Action<Hero> assignHeroToBattle)
    {
        _selectedHero.SetAssignDie(_selectedDieHandler);
        assignHeroToBattle?.Invoke(_selectedHero);
        _selectedDieHandler.isAssigned = true;
        
        if (_selectedDieHandler.isIgnus)
        {
            _ignusDie.Remove(_selectedDieHandler);
        }
        else
        {
            _fateDie.Remove(_selectedDieHandler);
        }
        
        DecreaseIgnus(_selectedDieHandler.DieCost);
        _selectedDieHandler = null;
        _selectedHero = null;
    }

    public void Pass()
    {
        _hasPassed = true;
    }

    public bool HasPassed()
    {
        return _hasPassed;
    }

    public Player nextPlayer()
    {
        return _nextPlayer;
    }

    public void OnRoundUpdated(GameManager.Round obj)
    {
        _currentRound = obj;
    }

    public void onStageUpdated(GameManager.Stage obj)
    {
        _currentStage = obj;
    }

    private void IncreaseIgnus(int i)
    {
        ignus += i;
        IgnusUpdated?.Invoke(ignus);
    }
    
    private void DecreaseIgnus(int i)
    {
        ignus -= i;
        IgnusUpdated?.Invoke(ignus);
    }


    private void OnDestroy()
    {
        IgnusUpdated -= IgnusPoolUI.UpdateIgnusUI;
    }

    public bool CanAssign()
    {
        if (_selectedDieHandler.isAssigned)
        {
            Debug.Log("Trying to assign a die that is allready assigned");
            return false;
        }

        return _currentRound switch
        {
            GameManager.Round.AssignHero => _selectedHero && _selectedDieHandler &&
                                            _selectedDieHandler.DieCost <= ignus,
            GameManager.Round.Battle => _selectedGreedPanel && _selectedDieHandler &&
                                        _selectedDieHandler.DieCost <= ignus,
            _ => false
        };
    }

    public void AddSelectedDie()
    {
        
    }
}


