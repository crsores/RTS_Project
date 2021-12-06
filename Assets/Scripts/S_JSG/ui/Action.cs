using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UI.HUD
{
    public class Action : MonoBehaviour
    {
        public void OnClick()
        {
             //ActionFrame.instance.StartSpawnTimer(name);
            InputManager.InputHandler.instance.UnitSpawn(name);
        }
        
    }
}
