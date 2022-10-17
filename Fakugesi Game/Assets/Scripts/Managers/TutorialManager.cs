using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Unity Handles")]
    public GameObject[] sequenceObjs;
    public GameObject anim;

    [Header("Integers")]
    public int index = 0;
    public int indexInQuestion;

  
    private void Start()
    {
        for (int i = 0; i < sequenceObjs.Length; i++)
        {
            sequenceObjs[i].SetActive(false);
        }
        if (anim != null)
            anim.SetActive(false);
        
    }
    private void Update()
    {
        if (anim != null)
        {
            if (index == indexInQuestion)
            {
                anim.SetActive(true);
            }
            else
            {
                anim.SetActive(false);
            }
        }
        else
            anim = null;
    }
    void LoopSequence()
    {
        for (int x = 0; x < sequenceObjs.Length; x++)
        {
            if (x == index)
            {
                sequenceObjs[x].SetActive(true);
            }
            else
            {
                sequenceObjs[x].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("sequenceTrigger"))
        {
            LoopSequence();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            index++;
        }
    }
}
