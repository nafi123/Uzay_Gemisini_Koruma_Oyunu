using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.ComponentModel;

public class DusmanKontrolu : MonoBehaviour
{
   
    // Start is called before the first frame update
    public float can = 400f;
    public GameObject mermi;
    public float mermiHizi = 8f;
    public float saniyeBasinaMermi = 0.6f;
    public int SkorDegeri = 200;
    private SkorKontrolu skorKontrolu;
    public AudioClip AtesSesi;
    public AudioClip OlumSesi;
    void Start()
    {
       skorKontrolu = GameObject.Find("Skor").GetComponent<SkorKontrolu>();
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
                skorKontrolu.SkoruArttir(SkorDegeri);
                AudioSource.PlayClipAtPoint(OlumSesi, transform.position);
            }
        }

    }
    

    // Update is called once per frame
    void Update()
    {
        float atmaOlasiligi = Time.deltaTime * saniyeBasinaMermi;
        if(Random.value < atmaOlasiligi)
        {
            AtesEt();
        }
    }
    void AtesEt()
    {
        Vector3 baslangicPozisyonu = transform.position + new Vector3(0, -0.8f, 0);
        GameObject dusmanMermisi = Instantiate(mermi, baslangicPozisyonu, Quaternion.identity) as GameObject;
        dusmanMermisi.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -mermiHizi);
        AudioSource.PlayClipAtPoint(AtesSesi, transform.position);
    }
}
