using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tower : MonoBehaviour
{
    public Animator anim;
    public float Range;
    public GameObject Target;

    public GameObject Splash;
    public int Damage;
    public GameObject Effect_obj;
    public GameObject Effect_pos;
    public Transform PartToRotate;
    public GameObject DestroyButton;
    public bool isClicked;
    public GameObject selectedTower = null;
    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget",0f,0.2f);
    }

    public void Update()
    {   
        LookAtTarget();

        if(Input.GetMouseButtonDown(0))
        {
        ClickDetect();
        }
    }
    
    private void onDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
    void UpdateTarget()
    {
        if(Target ==null)
        {
            GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");
            float shortestDistance = Mathf.Infinity;
            GameObject nearestMonster = null;
            foreach (GameObject Monster in Monsters)
            {
                float DistanceToMonsters = Vector3.Distance(transform.position, Monster.transform.position);

                if(DistanceToMonsters < shortestDistance)
                {
                    shortestDistance = DistanceToMonsters;
                    nearestMonster = Monster;
                }
            }
            if (nearestMonster != null && shortestDistance <= Range)
            {
                Target = nearestMonster;
                Attack();
            }
            else
            {
                Idle();
                Target = null;
            }
        }
        else if(Target != null)
        {
            float DistanceToMonsters = Vector3.Distance(transform.position, Target.transform.position);
            if(DistanceToMonsters > Range)
            {
                Idle();
                Target = null; 
            }
        }
    }

    public void Attack()
    {
        anim.SetInteger("Tower12",2);
    }

    public void Idle()
    {
        anim.SetInteger("Tower12",1);
    }

    public void SplashDamage()
    {
        GameObject splash = Instantiate(Splash, new Vector3(transform.position.x, transform.position.y+0.3f, transform.position.z), Quaternion.identity);
        splash.GetComponent<Splash>().Damage = Damage;
        Destroy(splash, 4f);
    }

    public void AttackTarget()
    {   
        if(Target != null)
        {

        GameObject Effect = Instantiate(Effect_obj, Effect_pos.transform.position, PartToRotate.rotation);
        Destroy(Effect, 2f);
        Target.GetComponent<Monster>().GetDamage(Damage);

        }
    }

    void LookAtTarget()
    {
        if(Target == null)
            return;

        Vector3 dir = Target.transform.position - transform.position;
        Quaternion lookRoatation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRoatation.eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);
        
    }
    public void select()
{
    if (isClicked == true)
    {
        isClicked = false;
    }
    else if (isClicked == false)
    {
        isClicked = true;
        DestroyButton.SetActive(true);
    }
}

    public void ClickDetect()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            string HitObj = (hit.transform.gameObject.tag);
            //print(HitObj + "맞음");
            // 맞은애가 타워면
            if (HitObj == "TowerSelect")
            {
                selectedTower = hit.transform.gameObject;
                selectedTower.GetComponent<Tower>().select();
            }
        }
    }
    public void DestroyButtonClick(){
        selectedTower.SetActive(false);
        DestroyButton.SetActive(false);
        gameManager.instance.AddScore(50);
    }

}
