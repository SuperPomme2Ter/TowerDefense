using System;
using System.Collections.Generic;
using UnityEngine;

public class S_CanonCharacteristics : MonoBehaviour
{
     public GameObject canon;
     public Transform offsetFiringPosition;
     internal List<Func<Vector3,int,bool>> FiringFunctions =new ();

     private void Awake()
     {
         FiringFunctions.Add(AlignToFire);
         FiringFunctions.Add(LaunchAnim);
         gameObject.SetActive(false);
     }
     
     
     internal bool LaunchAnim(Vector3 position,int childIndex=0)
     {
         canon.GetComponent<Animator>().SetTrigger("Fire");
         canon.transform.parent.parent.GetComponent<S_TowerCooldown>().enabled = false;
         return false;
     }
     internal bool AlignToFire(Vector3 target,int childIndex=0)
    {
        canon.transform.right = target-canon.transform.position;
        return true;
    }
}
