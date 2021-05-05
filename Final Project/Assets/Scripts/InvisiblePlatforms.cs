using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePlatforms : MonoBehaviour
{
    [SerializeField] GameObject invisiblePlats;

    public void makeVisible()
    {
        invisiblePlats.SetActive(true);
    }
}
