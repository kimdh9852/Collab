using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : Photon.MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        if (photonView.isMine)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Vector3 scale = transform.localScale;
                transform.localScale = scale;
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 scale = transform.localScale;
                transform.localScale = scale;
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                Vector3 scale = transform.localScale;
                transform.localScale = scale;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 scale = transform.localScale;
                transform.localScale = scale;
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }
}
