using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindTarget : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private List<PieceViewRange> views;
    [SerializeField] private ParticleSystem system;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private PieceDetail detail;

    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public int damageDealt;

    private bool canPlay = false;
    private bool attackTarget = false;

    private float attackTime = 0f;
    private float waitTime = 0f;
    private float elapsed = 1f;

    private float health;
    private PreparePhase currPhase;

    public void PlayUnit(float order, float downTime, PreparePhase phase)
    {
        currPhase = phase;
        health = detail.health;
        canPlay = true;
        waitTime -= order;
        elapsed = downTime;
    }

    private void OnMouseEnter()
    {
        if(!canPlay)
            system.Play();
    }

    private void OnMouseExit()
    {
        if(system.isPlaying)
            system.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlay && target != null)
        {
            waitTime += Time.deltaTime;
            if(waitTime > elapsed)
            {
                if (!attackTarget)
                {
                    Vector3 newPos = GetPath(target.position);
                    transform.position = newPos;
                }
                waitTime = 0f;
            }
            model.LookAt(target);

            if (attackTarget)
            {
                attackTime += Time.deltaTime;
                if(attackTime > detail.atkSpeed)
                {
                    attackTime = 0f;
                    DealDamage(detail.damage);
                }
            }
        }
    }

    private void DealDamage(float dmg)
    {
        target.GetComponent<FindTarget>().TakeDamage(dmg);
        manaBar.fillAmount += (dmg * 0.2f) * 0.01f;

        if(manaBar.fillAmount == 1)
        {
            //GetComponent<UltimateCast>().Cast();
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        healthBar.fillAmount = health / detail.health;
        manaBar.fillAmount += (dmg * 0.2f) * 0.01f;
        if(health <= 0)
        {
            currPhase.PieceDefeated(transform, tag.Equals("enemyPiece"));
            Destroy(this.gameObject);
        }

        if (manaBar.fillAmount == 1)
        {
            //GetComponent<UltimateCast>().Cast();
        }
    }



    private Vector3 GetPath(Vector3 pos)
    {
        PieceViewRange bestView = null;
        float dist = 10000f;
        foreach (var view in views)
        {
            if (view.foundTarget)
            {
                attackTarget = true;
                return transform.position;
            }

            if (!view.isOccupied)
            {
                var diff = Vector3.Distance(view.transform.position, pos);
                if(diff < dist)
                {
                    bestView = view;
                    dist = diff;
                }
            }
        }

        if (bestView == null)
        {
            return transform.position;
        }

        return bestView.transform.position;
    }
}
