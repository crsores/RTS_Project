using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Units.Player
{

    public class Marine : MonoBehaviour
    {
        public GameObject marine;


        void Start()
        {
            if (RTS.Player.playerManager.instance.MarineatkRangCheck == false)
            {
                marine.GetComponent<PlayerUnit>().baseStats.atkRange = 4;
                marine.GetComponent<PlayerUnit>().baseStats.airattackrange = 4;
            }
            else
            {
                marine.GetComponent<PlayerUnit>().baseStats.atkRange = 5;
                marine.GetComponent<PlayerUnit>().baseStats.airattackrange = 5;
            }


        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
                stempack();

            if (Input.GetKeyDown(KeyCode.Z))
                atkrangeUp();
        }

        public void stempack()
        {
            if (marine.GetComponent<PlayerUnit>().statDisplay.currentHealth <= 10)
            {
                Debug.Log("Ã¼·ÂÀÌ ³·½À´Ï´Ù");
                return;

            }

            else if (marine.GetComponent<PlayerUnit>().statDisplay.currentHealth > 10)
            {
                marine.GetComponent<PlayerUnit>().atkspeed -= marine.GetComponent<PlayerUnit>().atkspeed * 0.5f;
                marine.GetComponent<PlayerUnit>().speed += marine.GetComponent<PlayerUnit>().speed * 0.5f;
                marine.GetComponent<PlayerUnit>().statDisplay.currentHealth -= 10;
                Debug.Log("½ºÆÀÆÑ");

                Invoke("returnstem", 12.33f);
            }


        }
        public void returnstem()
        {
            marine.GetComponent<PlayerUnit>().atkspeed += marine.GetComponent<PlayerUnit>().atkspeed * 0.5f;
            marine.GetComponent<PlayerUnit>().speed -= marine.GetComponent<PlayerUnit>().speed * 0.5f;
        }

        public void atkrangeUp()
        {

            RTS.Player.playerManager.instance.MarineatkRangCheck = true;
            marine.GetComponent<PlayerUnit>().baseStats.atkRange = 5;
            marine.GetComponent<PlayerUnit>().baseStats.airattackrange = 5;
        }
    }
}