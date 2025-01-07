using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class S_ClickableObject : MonoBehaviour
{
    public abstract void Selected();
    public abstract void Unselected();

}
