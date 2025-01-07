using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CanonRotation : MonoBehaviour
{
     public Sprite canonSprite;
     public Transform firingPosition;

    public void AlignToFire(Vector3 target)
    {
        transform.right = target-transform.position;
    }
}
