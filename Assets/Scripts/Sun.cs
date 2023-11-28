using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;
public class Sun : MonoBehaviour
{
    [SerializeField] private PlayerDamage player;
    [SerializeField] private Collider playerCollider;
    //[SerializeField] private DOTweenPath path;
    [SerializeField] private float lightAngle;
    [SerializeField] private GameObject sunlight;
    private bool night;

    private float timer;
    private void Start()
    {
        //path.DOPlay();
        //night = false;
        StartCoroutine(DayNightCycle());
    }

    private void Update()
    {
        Vector3 sunAngle = new Vector3(lightAngle, 0, 0);
        Ray ray = new Ray(player.transform.position, sunAngle);
        RaycastHit hit;
        //sunlight.transform.Rotate(lightAngle, 0, 0); //robi dyskoteke, ale poprawne cyferki
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) // to też nie działa
        {
            if (hit.collider || night)
            {
                player.exposed = false;
                Debug.DrawRay(player.transform.position, -sunAngle * 100, Color.green);
            }
            else if (hit.collider == null && !night) 
            {
                player.exposed = true;
                Debug.DrawRay(player.transform.position, -sunAngle * 100, Color.red);
            }
        }
    }
    /*private void Update()
    {
        Vector3 playerDirection = -transform.position + (player.transform.position + new Vector3(0, 1, 0));
        Ray ray = new Ray(transform.position, playerDirection * 100);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider == playerCollider && !night)
            {
                player.exposed = true;
                Debug.DrawRay(transform.position, playerDirection * 100, Color.red);
            }
            else if (hit.collider != playerCollider || night)
            {
                player.exposed = false;
                Debug.DrawRay(transform.position, playerDirection * 100, Color.green);
            }
        }
    }*/

    IEnumerator DayNightCycle()
    {
        while (true)
        {
            night = false;
            sunlight.SetActive(true);
            while (timer < 15)
            {
                timer += 1;
                Debug.Log(timer);
                var t = timer / 15;
                lightAngle = Mathf.LerpAngle(45.0f, 135.0f, t); //nie
                // lightAngle += 6; też robi syf
                yield return new WaitForSeconds(1f);
                //sunlight.transform.Rotate(lightAngle, 0, 0); //niepoprawne cyferki, skacze wszędzie ale nie tam gdzie trzeba
            }

            night = true;
            sunlight.SetActive(false);
            yield return new WaitForSeconds(15f);
            timer = 0;
        }
    }
    
    /*public void Restart()
    {
        StartCoroutine(Finished());
    }
    IEnumerator Finished()
    {
        path.DORestart();
        path.DOPause();
        timer = 0;
        night = true;
        while (timer < 15)
        {
            timer += 1;
            Debug.Log(timer);
            yield return new WaitForSeconds(1f);
        }
        night = false;
        path.DOPlay();
    }*/
}
