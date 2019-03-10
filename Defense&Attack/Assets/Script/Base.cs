using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public TextMesh HPtext=null;
    static public int HP = 0;
    // Start is called before the first frame update
   
    void Start()
    {
        HP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        HPtext.text = HP.ToString();
    }
}
