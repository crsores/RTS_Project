using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.Player;

namespace Units.Player
{
    public class BattleCruiser : MonoBehaviour
    {
        public GameObject battlecruiser;
        void Start()
        {
            if (playerManager.instance.BattleCruiserMpCheck == false)
            {
                battlecruiser.GetComponent<PlayerUnit>().statDisplay.maxmp = 200;
                battlecruiser.GetComponent<PlayerUnit>().statDisplay.mp = 50;
            }
            else
            {
                battlecruiser.GetComponent<PlayerUnit>().statDisplay.maxmp = 250;
                battlecruiser.GetComponent<PlayerUnit>().statDisplay.mp = 62.5f;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void mpup()
        {

            playerManager.instance.BattleCruiserMpCheck = true;
            battlecruiser.GetComponent<PlayerUnit>().statDisplay.maxmp = 250;
            

        }

        public void yamato()
        {



        }



    }
}