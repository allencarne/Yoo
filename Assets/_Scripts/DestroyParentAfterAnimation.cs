using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentAfterAnimation : MonoBehaviour
{
    public void AE_DestroyParentAfterAnimation()
    {
        Destroy(transform.parent.gameObject);
    }
}
