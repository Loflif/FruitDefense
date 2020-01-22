using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostGameText : MonoBehaviour
{
    [SerializeField] private Text Text;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeText(int HungerlingsKilled, int HungerlingsTotal, int CashRecieved)
    {
        Text.text = "You Managed to kill " + HungerlingsKilled.ToString() + "/" + HungerlingsTotal.ToString() + " Hungerlings!\n"
            + "This earned you: " + CashRecieved.ToString() + "$\n" 
            + "Spend it Wisely";
    }
}
