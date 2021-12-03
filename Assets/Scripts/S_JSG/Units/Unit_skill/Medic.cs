using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.Player;

namespace Units.Player
{
    public class Medic : MonoBehaviour
    {

        public GameObject medic;
        // Start is called before the first frame update
        void Start()
        {
            if (playerManager.instance.MedicMpCheck == false)
            {
                medic.GetComponent<PlayerUnit>().baseStats.maxmp = 200;
                medic.GetComponent<PlayerUnit>().baseStats.mp = 50;


            }
            else if (playerManager.instance.MedicMpCheck == true)
            {
                medic.GetComponent<PlayerUnit>().baseStats.maxmp = 250;
                medic.GetComponent<PlayerUnit>().baseStats.mp = 62.5f;
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
                mpup();
        }

       public void mpup()
        {
            playerManager.instance.MedicMpCheck = true;

            medic.GetComponent<PlayerUnit>().baseStats.maxmp = 250;
            //medic.GetComponent<PlayerUnit>().baseStats.mp = 62.5f;
            //medic.GetComponent<PlayerUnit>().statDisplay.maxmp = 250;
        }
    }
}
