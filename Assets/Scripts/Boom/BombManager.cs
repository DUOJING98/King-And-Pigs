using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    private GameObject currentBomb;
    // Start is called before the first frame update
    public void RemoveBomb()
    {
        Destroy(currentBomb);
        currentBomb = null;
    }
}
