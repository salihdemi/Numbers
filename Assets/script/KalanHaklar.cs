using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalanHaklar : MonoBehaviour
{
    [SerializeField]
    GameObject kalp1, kalp2, kalp3;
    public void KalanHak(int hak)
    {
        switch (hak)
        {
            case 3:
                kalp1.SetActive(true);
                kalp2.SetActive(true);
                kalp3.SetActive(true);
                break;


            case 2:
                kalp1.SetActive(false);
                kalp2.SetActive(true);
                kalp3.SetActive(true);
                break;

            case 1:
                kalp1.SetActive(false);
                kalp2.SetActive(false);
                kalp3.SetActive(true);
                break;

            case 0:
                kalp1.SetActive(false);
                kalp2.SetActive(false);
                kalp3.SetActive(false);
                break;
        }
    }
}
