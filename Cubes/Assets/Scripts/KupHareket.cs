using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KupHareket : MonoBehaviour
{
    public Vector3 DuvarKonumu { get; set; }//duvarý geçince silineceði için
    public KeyCode keyCode { get; set; }//A-S-D-F ye baðýmlý kalmak istemedim oyun içinde 
    public float Hiz { get; set; }
    public float KupUzamaHizi { get; set; }//scale


    void Update()
    {

        if (transform.position.y - transform.localScale.y / 2 >= DuvarKonumu.y)
        {
            Destroy(gameObject);
        }

        if (Input.GetKey(keyCode))
        {
            Vector3 scale = transform.localScale; //scaleini aldým 
            float scaleHiz = KupUzamaHizi * Time.deltaTime;
            scale.y += scaleHiz;
            transform.localScale = scale;

            Vector3 pos = transform.position;
            pos.y += scaleHiz / 2;
            transform.position = pos;
            return;
        }
        transform.Translate(Vector3.up * Hiz * Time.deltaTime);
    }
}
