using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalaceGateController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator[] anims;
    void Awake()
    {
        anims = GetComponentsInChildren<Animator>();
    }

    public void RaiseSegments()
    {
        foreach (Animator anim in anims)
        {
            anim.SetTrigger("Raise");
        }
    }
    public void LowerSegments()
    {
        foreach (Animator anim in anims)
        {
            anim.SetTrigger("Lower");
        }
        Invoke("DelayedIncrement", 0.7f);
    }

    private void DelayedIncrement()
    {
        GameObject.Find("PalaceStateManager").GetComponent<PalaceProgressionManager>().IncrementState();
    }
}
