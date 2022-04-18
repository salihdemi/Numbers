using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class levelMS : MonoBehaviour
{
    [SerializeField]
    private GameObject karePrefab,puanPaneli,sonPanel;
    [SerializeField]
    private Transform karePanel, soruPaneli;
    [SerializeField]
    KalanHaklar kh;
    private int bolunen, bolen, soruSýra, secilenSayý, hak, puan, zorluk;
    private List<int> bolumDegerleri = new List<int>(25);
    private GameObject[] kareler = new GameObject[25];
    bool týklanabilir;
    private GameObject seciliKare;
    private void Awake()
    {
        hak = 3;
        kh.KalanHak(hak);
    }
    void Start()
    {
        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        kareleriOlustur();
        Invoke("SoruPaneliAc", 2);
    }
    void OyunBitti()
    {
        týklanabilir = false;
        sonPanel.SetActive(true);
    }
    public void YenidenBaþla()
    {
        SceneManager.LoadScene(1);
    }
    public void AnaMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Cikis()
    {
        Application.Quit();
    }
    void SoruPaneliAc()
    {
        soruPaneli.GetComponent<RectTransform>().DOScale(1, 0.5f);
        týklanabilir = true;
    }

    public void kareleriOlustur()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject kare = Instantiate(karePrefab, karePanel);
            kare.GetComponent<Button>().onClick.AddListener(() => Týklandý());
            kareler[i] = kare;
        }

        StartCoroutine(DoFadeRoutine());
        KarelereSayiVer();
        SoruSor();
    }

    void Týklandý()
    {
        if (týklanabilir)
        {
            secilenSayý = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponentInChildren<Text>().text);
            seciliKare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (secilenSayý == bolunen / bolen)
            {
                seciliKare.GetComponent<Button>().interactable = false;
                seciliKare.transform.GetChild(1).GetComponent<Image>().enabled = true;
                puan += ZorlukSec() * 5;
                bolumDegerleri.RemoveAt(soruSýra);
                puanPaneli.GetComponentInChildren<Text>().text = puan.ToString();
                if (bolumDegerleri.Count > 0)
                {
                    SoruSor();
                }
                else
                {
                    OyunBitti();
                }
            }
            else
            {
                hak--;
                kh.KalanHak(hak);
            }
            if (hak <= 0)
            {
                OyunBitti();
            }
        }
    }

    void KarelereSayiVer()
    {
        foreach (var kare in kareler)
        {
            int rastDeger = Random.Range(1, 13);
            bolumDegerleri.Add(rastDeger);
            kare.GetComponentInChildren<Text>().text = rastDeger.ToString();
        }
    }

    void SoruSor()
    {
        bolen = Random.Range(2, 11);
        soruSýra = Random.Range(0, bolumDegerleri.Count);
        bolunen = bolen * bolumDegerleri[soruSýra];
        soruPaneli.GetComponentInChildren<Text>().text = (bolunen+":"+bolen);
    }

    int ZorlukSec()
    {
        if (bolunen >= 40)
        {
            zorluk = 3;
        }
        else if (bolunen >= 20)
        {
            zorluk = 2;
        }
        else
        {
            zorluk = 1;
        }
        return zorluk;
    }

    IEnumerator DoFadeRoutine()
    {
        foreach (var kare in kareler)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.2f);

            yield return new WaitForSeconds(0.1f);
        }
    }

}