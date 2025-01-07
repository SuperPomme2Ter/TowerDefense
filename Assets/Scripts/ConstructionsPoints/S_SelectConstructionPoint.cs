using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_SelectConstructionPoint : S_ClickableObject
{
    private List<BoxCollider2D> choiceButtons = new();
    private Animator barAnimator;

    private void Start()
    {
        barAnimator = GetComponent<Animator>();
        GameObject slidingBar = transform.GetChild(2).gameObject;
        for (int i = 1; i < slidingBar.transform.childCount; i++)
        {
            choiceButtons.Add(slidingBar.transform.GetChild(i).GetComponent<BoxCollider2D>());
            choiceButtons[i-1].enabled = false;
        }
    }

    public override void Selected()
    {
        foreach (BoxCollider2D choice in choiceButtons)
        {
            choice.enabled = true;
        }
        barAnimator.SetTrigger("GainFocus");
    }

    public override void Unselected()
    {
        foreach (BoxCollider2D choice in choiceButtons)
        {
            choice.enabled = false;
        }
        barAnimator.SetTrigger("LoseFocus");
    }
}
