using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class S_ClickHandler : MonoBehaviour
{
    private S_ClickableObject selectedObject;
    public void OnClick(InputAction.CallbackContext context)
    {
        
        if (context.started)
        {
            Debug.Log("mmmmmmm");
            RaycastHit2D mouseRay= Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (mouseRay && mouseRay.transform.TryGetComponent<S_ClickableObject>(out S_ClickableObject _object))
            {
                if (_object != selectedObject)
                {
                    if (selectedObject != null)
                    {
                        selectedObject.Unselected();
                    }

                    _object.Selected();
                    selectedObject = _object;
                }
            }
            else if (selectedObject != null)
            {
                selectedObject.Unselected();
                selectedObject = null;
            }
        }
    }
}
