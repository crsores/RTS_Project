using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Building {
    public class MissileTurret : MonoBehaviour
    {
        public GameObject missileturret;

        private float attack = 20f;
        private float atkSpeed = 1.5f;
        private float atkRange = 7f;
        private float atkCooldown;


        

        public float distance; //private

        public Collider[] rangeColliders;//private

        public enum atk_type
        {
            normal,
            vibration,
            blast
        }

        public atk_type air_type;
        //public Transform aggerTarget; //private
        private Units.Enemy.enemyUnit atkUnit;


        void Start()
        {
            

        }

        // Update is called once per frame
        void Update()
        {
            if (atkCooldown>=-1)
            atkCooldown -= Time.deltaTime;

            checkForEnemyTargets();

            if (atkUnit == true)
            {
                RangeCheck();
                turretAttack();
            }

            

        }

        private void checkForEnemyTargets() //범위안 타겟찾음
        {
            rangeColliders = Physics.OverlapSphere(transform.position, missileturret.GetComponent <Player.PlayerBuilding>().baseStats.eyesight, Units.UnitHandler.instance.eUnitLayer);

            for (int i = 0; i < rangeColliders.Length;)
            {
                if (rangeColliders[i].gameObject.GetComponent<Units.Enemy.enemyUnit>().baseStats.air == true)
                {


                    atkUnit = rangeColliders[i].gameObject.GetComponent<Units.Enemy.enemyUnit>();


                    break;

                }
            }
        }
        private void turretAttack()
        {
           
            if (distance<=atkRange&&atkCooldown<=0)
            {

                atkUnit.GetComponentInChildren<Units.UnitStatDisplay>().TakeDamage(attack);
                atkCooldown = atkSpeed;
                Debug.Log("터렛 공격");
            }


        }

        private void RangeCheck()
        {
            distance = Vector3.Distance(atkUnit.transform.position, transform.position);


            if (distance >= 15)
                atkUnit = null;
        }

    }
}
