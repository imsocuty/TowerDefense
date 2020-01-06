using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public GameObject DamageText;
    public GameObject TextPos;
    public GameObject HealthBar;
    public float StartHealth;
    public float Health;
    public void GetDamage(int damage)
    {
        GameObject dmgtext = Instantiate(DamageText,TextPos.transform.position,Quaternion.identity);
        dmgtext.GetComponent<Text>().text = damage.ToString();
        Health -=damage;
        HealthBar.GetComponent<Image>().fillAmount = Health / StartHealth;
        Destroy(dmgtext, 0.5f);

        if(Health <= 0)
        {
            Destroy(gameObject);
            gameManager.instance.AddScore(50);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Splash")
        {   
            GetDamage(col.GetComponent<Splash>().Damage);
        }
    }
}
