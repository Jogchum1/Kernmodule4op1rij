using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetup : MonoBehaviour
{
    public NetworkedPlayer player;
    public Button button;

    
    public void SetClickEvent()
    {
        button.onClick.AddListener(delegate { player.PlaceCoin(button.transform); });
    }
    
}
