using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct KupVerileri
{
    public Color kupRenk;
    public KeyCode keyCode;
}

public class KupUretici : MonoBehaviour
{
    public Transform duvarKonumu;
    public Transform kupBaslangicKonumu;
    public GameObject kupPrefab;
    public KupVerileri[] kupVerileri;
    public float scaleHiz;
    public float kupHareketHizi;
    public float kupUzamaHizi;

    Dictionary<KeyCode, GameObject> atananKupler = new Dictionary<KeyCode, GameObject>();
    void Start()
    {
        for (int i = 0; i < kupVerileri.Length; i++) //kactane kupum varsa o kadar dönecek ve hepisini ayný yaratacak
        {
            KupVerileri cubeData = kupVerileri[i];
            GameObject go = Instantiate(kupPrefab, kupBaslangicKonumu.position + Vector3.right * scaleHiz * i, Quaternion.identity);
            KupHareket kupHareket = go.GetComponent<KupHareket>();
            kupHareket.keyCode = KeyCode.None;
            kupHareket.Hiz = 0;
            kupHareket.KupUzamaHizi = 0;
            go.GetComponentInChildren<Renderer>().material.color = cubeData.kupRenk;
        }
    }

    void Update()
    {
        for (int i = 0; i < kupVerileri.Length; i++)
        {
            KupVerileri kupVeri = kupVerileri[i];
            if (!Input.GetKeyDown(kupVeri.keyCode))
                continue;

            if (atananKupler.TryGetValue(kupVeri.keyCode, out GameObject atananGo))
            {
                if (atananGo!=null) 
                    atananGo.GetComponent<KupHareket>().keyCode = KeyCode.None; //ikisi beraber buyumesýn dýye 
                atananKupler.Remove(kupVeri.keyCode);
            }

            GameObject go = Instantiate(kupPrefab, kupBaslangicKonumu.position + Vector3.right * scaleHiz * i, Quaternion.identity);
            go.transform.localScale = new Vector3(0.75f, 0.1f, 0.75f);//olusan küpün x y z si
            KupHareket kupHareket = go.GetComponent<KupHareket>();
            kupHareket.keyCode = kupVeri.keyCode;
            kupHareket.DuvarKonumu = duvarKonumu.position;
            kupHareket.Hiz = kupHareketHizi;
            kupHareket.KupUzamaHizi = kupUzamaHizi;
            go.GetComponentInChildren<Renderer>().material.color = kupVeri.kupRenk;
            atananKupler.Add(kupVeri.keyCode, go);
        }
    }
}

