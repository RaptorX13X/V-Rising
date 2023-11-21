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
    [SerializeField] private DOTweenPath path;
    private bool night;

    private float timer;
    private void Start()
    {
        path.DOPlay();
        night = false;
    }

    private void Update()
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
    }

    public void Restart()
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
    }
}
