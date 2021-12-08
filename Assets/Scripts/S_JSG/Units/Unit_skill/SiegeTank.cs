using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Units.Player
{

    public class SiegeTank : MonoBehaviour
    {
        public GameObject tank;

        public float N_attack, N_attackSpeed, N_attackRange, N_UpPlus, N_Speed;
        public int N_DropCount,S_DropCount;
        public float S_attack, S_attackSpeed, S_attackRange,S_UpPlus,S_Speed;


        enum mode
        {
            Normal,
            Chang,
            Siege

        }

        mode _Mode = mode.Normal;
        void Start()
        {

            N_attack = tank.GetComponent<PlayerUnit>().attack;
            N_attackRange = tank.GetComponent<PlayerUnit>().atkRange;
            N_attackSpeed = tank.GetComponent<PlayerUnit>().atkspeed;
            N_UpPlus = tank.GetComponent<PlayerUnit>().atkUpPlus;
            N_Speed = tank.GetComponent<PlayerUnit>().speed;
            N_DropCount = tank.GetComponent<PlayerUnit>().DropCount;

            S_attack = 70;
            S_attackRange = 12;
            S_attackSpeed = 7.5f;
            S_UpPlus = 5;
            S_Speed = 0;
            S_DropCount = 0;


        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SiegeMode()
        {
            switch (_Mode)
            {
                case mode.Normal:
                    tank.GetComponent<PlayerUnit>().attack = N_attack;
                    tank.GetComponent<PlayerUnit>().atkRange = N_attackRange;
                    tank.GetComponent<PlayerUnit>().atkspeed = N_attack;
                    tank.GetComponent<PlayerUnit>().atkUpPlus = N_UpPlus;
                    tank.GetComponent<PlayerUnit>().speed = N_Speed;
                    tank.GetComponent<PlayerUnit>().DropCount = N_DropCount;
                    break;

                case mode.Chang:
                    tank.GetComponent<PlayerUnit>().attack = 0;
                    tank.GetComponent<PlayerUnit>().atkRange = 0;
                    tank.GetComponent<PlayerUnit>().atkspeed = 0;
                    tank.GetComponent<PlayerUnit>().atkUpPlus = 0;
                    tank.GetComponent<PlayerUnit>().speed = 0;
                    tank.GetComponent<PlayerUnit>().DropCount = 0;

                    break;
                case mode.Siege:
                    tank.GetComponent<PlayerUnit>().attack = S_attack;
                    tank.GetComponent<PlayerUnit>().atkRange = S_attackRange;
                    tank.GetComponent<PlayerUnit>().atkspeed = S_attack;
                    tank.GetComponent<PlayerUnit>().atkUpPlus = S_UpPlus;
                    tank.GetComponent<PlayerUnit>().speed = S_Speed;
                    tank.GetComponent<PlayerUnit>().DropCount = S_DropCount;
                    break;




            }
           


        }
        public void ChangeMode()
        {
            _Mode = mode.Chang;

            Invoke("changemode", 1.5f);

            

        }
        public void changemode()
        {
            if (_Mode == mode.Normal)
            {
                _Mode = mode.Siege;

            }
            else if (_Mode == mode.Siege)
            {
                _Mode = mode.Normal;
            }


        }



    }
}