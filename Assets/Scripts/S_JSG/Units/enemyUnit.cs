using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Units.Enemy
{

    public class enemyUnit : MonoBehaviour
    {
        public BasicUnit unitType;

        public UnitStatType.Base baseStats;

        public UnitStatDisplay statDisplay;

        public Collider[] rangeColliders;//private

        public Transform aggerTarget; //private

        private UnitStatDisplay aggroUnit;

        

       // public GameObject unitstatDisplay;

     //   public Image healthbarAmount;

        private bool hasAggero = false;

        private float atkCooldown;

        public float distance; //private

       // public float currentHealth;
        
        private void Start()
        {
            baseStats = unitType.baseStats;
            statDisplay.SetStatatDisplayUnit(baseStats, false);

        }

        private void Update()
        {

            if (atkCooldown>=-1)
            atkCooldown -= Time.deltaTime;
            
            if (!hasAggero)
            {
                checkForEnemyTargets();
            }
            else
            {
                //Attack();
                MoveToAggroTarget();
            }
            
        }

        

        
        
        private void checkForEnemyTargets() //범위안 타겟찾음
        {
            rangeColliders = Physics.OverlapSphere(transform.position, baseStats.eyesight, UnitHandler.instance.pUnitLayer);

            for (int i =0; i < rangeColliders.Length;)
            {
                aggerTarget = rangeColliders[i].gameObject.transform;
                aggroUnit = aggerTarget.gameObject.GetComponentInChildren<UnitStatDisplay>();
                

                hasAggero = true;
                break;
            }
        }

        private void Attack()
        {
            
            
            if (atkCooldown <= 0&&distance<=baseStats.atkRange)
            {
                aggroUnit.TakeDamage(baseStats.attack);
                atkCooldown = baseStats.atkspeed;
            }
        }
       
        private void MoveToAggroTarget() //타겟을 찾으면 따라감
        {
            if (aggerTarget == null)
            {
                hasAggero = false;
            }
            else
            {
                distance = Vector3.Distance(aggerTarget.position, transform.position);

                if (distance <= baseStats.eyesight)
                {
                    //Debug.Log("이동");
                    transform.position = Vector3.MoveTowards(transform.position, aggerTarget.position, 3f * Time.deltaTime);

                }
            }

        }

        
    }
}
