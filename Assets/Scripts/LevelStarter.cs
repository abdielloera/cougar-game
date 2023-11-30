using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    public GameObject CountDown3;
    public GameObject CountDown2;
    public GameObject CountDown1;
    public GameObject CountDownGo;
    public GameObject fadein;
    public AudioSource readyFX;
    public AudioSource goFX;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountSequence());
    }
    IEnumerator CountSequence()
    {
        yield return new WaitForSeconds(.5f);
        CountDown3.SetActive(true);
        readyFX.Play();
        yield return new WaitForSeconds(1);
        CountDown2.SetActive(true);
        readyFX.Play();
        yield return new WaitForSeconds(1);
        CountDown1.SetActive(true);
        readyFX.Play();
        yield return new WaitForSeconds(1);
        CountDownGo.SetActive(true);
        goFX.Play();
    }
}
