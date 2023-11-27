using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public BGMusic bgMusic;

    private void Awake()
    {
        if (bgMusic == null)
        {
            bgMusic = this;
        }

        else
        {
            Destroy(this.gameObject);
        }
    }
}
