using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DieHandler: MonoBehaviour {
    [SerializeField] private List<int> _faces;
    public int half;
    public bool isIgnus;
    public bool isAssigned = false;

    private const string D4 = "D4";
    private const string D6 = "D6";
    private const string D8 = "D8";
    private const string D10 = "D10";
    private const string D12 = "D12";  
    private const string D20 = "D20";

    [SerializeField] public int DieCost;

    [SerializeField] private  Sprite D4IMAGE;
    [SerializeField] private  Sprite  D6IMAGE;
    [SerializeField] private  Sprite D8IMAGE;
    [SerializeField] private  Sprite  D10IMAGE;
    [SerializeField] private  Sprite  D12IMAGE;  
    [SerializeField] private  Sprite  D20IMAGE;

    public List<int> Faces { get => _faces; set => _faces = value; }

    public int Roll()
    {
        var sel = Faces[UnityEngine.Random.Range(0, Faces.Count)];
        if (isIgnus)
            return sel;
        if (sel < half)
        {
            sel += Roll();
        }
        return sel;
    }

    public void CreateDieCustom(List<int> newFaces, bool is_ignus, int dieCost)
    {
        Debug.LogError("This is a shell of a thing. Not ready. You should ready it now I guess");
        isIgnus = is_ignus;
        Faces = new List<int>(newFaces.Count);
        for(int i = 0; i < newFaces.Count; i++)
        {
            Faces.Add(newFaces[i]);
        }
    }
    
    public DieHandler CreateD4(bool is_ignus = false, Transform spawnZone = null)
    {
        name = "d4";
        isIgnus = is_ignus;
        DieCost = 2;
        if (spawnZone){
            transform.SetParent(spawnZone);    
        }
        gameObject.GetComponent<Image>().sprite = D4IMAGE;
        isIgnus = is_ignus;
        Faces = new List<int>(4) { 1, 2, 3, 4 };
        return this;
    }
    
    public DieHandler CreateD6(bool is_ignus = false , Transform spawnZone = null)
    {
        name = "d6";
        isIgnus = is_ignus;
        DieCost = 3;
        if (spawnZone){
            transform.SetParent(spawnZone);    
        }
        gameObject.GetComponent<Image>().sprite = D6IMAGE;
        Faces = new List<int>(6) { 1, 2, 3, 4, 5, 6 };
        return this;
    }

    public DieHandler CreateD8(bool is_ignus = false, Transform spawnZone = null)
    {
        name = "D8";
        isIgnus = is_ignus;
        DieCost = 4;
        if (spawnZone){
            transform.SetParent(spawnZone);    
        }
        gameObject.GetComponent<Image>().sprite = D8IMAGE;
        Faces = new List<int>(8) { 1, 2, 3, 4, 5, 6, 7, 8 };
        if (spawnZone){
            transform.SetParent(spawnZone);    
        }
        return this;
    }
    
    public DieHandler CreateD10(bool is_ignus = false, Transform spawnZone = null)
    {
        name = "D10";
        isIgnus = is_ignus;
        DieCost = 5;
        if (spawnZone){
            transform.SetParent(spawnZone);    
        }
        gameObject.GetComponent<Image>().sprite = D10IMAGE;
        Faces = new List<int>(10) { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        return this;
    }

    public DieHandler CreateD12(bool is_ignus = false, Transform spawnZone = null)
    {
        name = "D12";
        isIgnus = is_ignus;
        DieCost = 6;
        if (spawnZone){
            transform.SetParent(spawnZone);    
        }
        gameObject.GetComponent<Image>().sprite = D12IMAGE;
        Faces = new List<int>(12) { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        return this;
    }

    public DieHandler CreateD20(bool is_ignus = false, Transform spawnZone = null)
    {
        name = "D20";
        isIgnus = is_ignus;
        DieCost = 10;
        if (spawnZone){
            transform.SetParent(spawnZone);    
        }
        gameObject.GetComponent<Image>().sprite = D20IMAGE;
        Faces = new List<int>(20) { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        return this;
    }
}