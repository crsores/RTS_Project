using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.Player;

namespace Units.Player
{
    public class ScineVessel : MonoBehaviour
    {
        public GameObject scinevessel;
        void Start()
        {
            if (playerManager.instance.ScineVesselMpCheck == false)
            {
                scinevessel.GetComponent<PlayerUnit>().baseStats.maxmp = 200;
                scinevessel.GetComponent<PlayerUnit>().baseStats.mp = 50;

            }
            else
            {
                scinevessel.GetComponent<PlayerUnit>().baseStats.maxmp = 250;
                scinevessel.GetComponent<PlayerUnit>().baseStats.mp = 62.5f;
            }


        }

        // Update is called once per frame
        void Update()
        {

        }
        public void mpup() {

            playerManager.instance.ScineVesselMpCheck = true;

            scinevessel.GetComponent<PlayerUnit>().baseStats.maxmp = 250;
            
        }

    }
}