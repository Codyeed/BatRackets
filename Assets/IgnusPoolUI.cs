using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IgnusPoolUI : MonoBehaviour
{

    [SerializeField] private Text UI;
    public void UpdateIgnusUI(int obj)
    {
        Debug.Log("+++Updateing text+++++");
        UI.text = obj.ToString();
    }
}
