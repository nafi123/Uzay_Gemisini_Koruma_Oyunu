using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GemiKontrolu : MonoBehaviour
{
    private float hiz = 20f;
    float xminx;
    float xmax;
    public float inceAyar = 0.8f;
    public GameObject Mermi;
    public float mermininHizi = 5f;
    public float atesEtmeAraligi = 0.2f;
    public float can = 100f;
    public AudioClip AtesSesi;
    public AudioClip OlumSesi;
    // Start is called before the first frame update
    void Start()
    {
        float uzaklik = transform.position.z - Camera.main.transform.position.z;
        Vector3 solUc = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, uzaklik));
        Vector3 sagUc = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, uzaklik));
        
        xminx = solUc.x + inceAyar;
        xmax = sagUc.x - inceAyar;

    }
    void AtesEtme()
    {
        GameObject gemimizinMermisi = Instantiate(Mermi, transform.position + new Vector3(0, 1f, 0), Quaternion.identity) as GameObject;
        gemimizinMermisi.GetComponent<Rigidbody2D>().velocity = new Vector3(0, mermininHizi, 0);
        AudioSource.PlayClipAtPoint(AtesSesi, transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("AtesEtme", 0.01f, atesEtmeAraligi);
        }
        if (Input.GetKeyUp(KeyCode.Space)){
           
            CancelInvoke("AtesEtme");
        }
        
        float yeniX = Mathf.Clamp(transform.position.x, xminx, xmax);
        transform.position = new Vector3(yeniX, transform.position.y, transform.position.z);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += new Vector3(-hiz * Time.deltaTime, 0, 0);
            transform.position += Vector3.left * hiz * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //ransform.position += new Vector3(hiz * Time.deltaTime, 0, 0);
            transform.position += Vector3.right * hiz * Time.deltaTime;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MermiKontrolu carpanMermi = collision.gameObject.GetComponent<MermiKontrolu>();
        if (carpanMermi)
        {
            can -= carpanMermi.ZararVerme();
            carpanMermi.CarptigindaYokOl();
            if (can <= 0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(OlumSesi, transform.position);
            }
        }

    }
}
