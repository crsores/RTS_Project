using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveVec = new Vector3(horizontal, 0, vertical);
        moveVec = moveVec.normalized * Speed * Time.deltaTime;

        this.transform.Translate(moveVec);
        
    }
}
