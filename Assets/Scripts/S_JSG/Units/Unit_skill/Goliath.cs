using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.Player;

namespace Units.Player
{

    public class Goliath : MonoBehaviour
    {
        public GameObject goliath;
        void Start()
        {
            if (playerManager.instance.GoliathRangCheck == false)
            {
                goliath.GetComponent<PlayerUnit>().baseStats.airattackrange = 5;

            }
            else
            {
                goliath.GetComponent<PlayerUnit>().baseStats.airattackrange = 8;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void airrangeup()
        {
            playerManager.instance.GoliathRangCheck = true;
            goliath.GetComponent<PlayerUnit>().baseStats.airattackrange = 8;
        }
    }
}
