using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die 
{
    private List<int> _faces;
    public int half;
    public bool isIgnus;

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

    public void CreateDieCustom(List<int> newFaces, bool is_ignus)
    {
        isIgnus = is_ignus;
        Faces = new List<int>(newFaces.Count);
        for(int i = 0; i < newFaces.Count; i++)
        {
            Faces.Add(newFaces[i]);
        }
    }

    public Die CreateD6(bool is_ignus = false)
    {
        isIgnus = is_ignus;
        Faces = new List<int>(6) { 1, 2, 3, 4, 5, 6 };
        return this;
    }

    public Die CreateD4(bool is_ignus = false)
    {
        isIgnus = is_ignus;
        Faces = new List<int>(4) { 1, 2, 3, 4 };
        return this;
    }

    public Die CreateD8(bool is_ignus = false)
    {
        isIgnus = is_ignus;
        Faces = new List<int>(8) { 1, 2, 3, 4, 5, 6, 7, 8 };
        return this;
    }
    
    public Die CreateD10(bool is_ignus = false)
    {
        isIgnus = is_ignus;
        Faces = new List<int>(10) { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        return this;
    }

    public Die CreateD12(bool is_ignus = false)
    {
        isIgnus = is_ignus;
        Faces = new List<int>(12) { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        return this;
    }

    public Die CreateD20(bool is_ignus = false)
    {
        isIgnus = is_ignus;
        Faces = new List<int>(20) { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        return this;
    }
}
