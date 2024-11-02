using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLControl : MonoBehaviour
{
   private Rigidbody RB;
   [SerializeField] private float moveSpeed;
   [HideInInspector] public Vector3 moveInput; //инфа для скилов, например деша

   void Start() 
   {
      RB = GetComponent<Rigidbody>();
   }
   
   void FixedUpdate()
   {
      moveInput = new Vector3(x:Input.GetAxis("Horizontal"), y:0, z:Input.GetAxis("Vertical")).normalized; //из-за нормализации двигается ещё какое то время когда останавливаешся
      RB.velocity = moveInput * moveSpeed;

      if (moveInput.magnitude > 0.1f)
      {
         Quaternion rotation = Quaternion.LookRotation(moveInput);
         rotation.x = 0;
         rotation.z = 0; // поворот по оси Y, остальные блочим
         transform.rotation = Quaternion.Lerp(a:transform.rotation, b:rotation, 13 * Time.deltaTime); // поворот, от А до Б
      }
   }
}
