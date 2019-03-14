using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBtnManager : MonoBehaviour
{

    //public Transform enemyPrefab;
    //public Transform spawnPoint;
    public GameObject zone;

    public void PurchaseYes()
    {
        zone.gameObject.tag = "User1Area";
        Tile.btn_state = true;
    }
    public void PurchaseNo()
    {
        Tile.btn_state = true;
    }
}
