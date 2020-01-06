using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject SelectNode;
    public static BuildManager instance;

    public GameObject Monster;
    private float TimeLeft = 2.0f;
    private float nextTime = 0.0f;
    public GameObject[] tempGO; 
    GameObject tower1;
    public void Start()
    {
        instance = this;
    }

    void Update()
    {
        if(nextTime > TimeLeft)
        {
            Instantiate(Monster, new Vector3(4,0,-13), Quaternion.identity);
            nextTime = 0;
        }else{
                nextTime += Time.deltaTime;
        }
    }  
    public void BuildToTower()
    {   
        bool Color1 = GameObject.Find("Node1").GetComponentInChildren<Node>().Color;
        bool Color2 = GameObject.Find("Node2").GetComponentInChildren<Node>().Color;
        bool Color3 = GameObject.Find("Node3").GetComponentInChildren<Node>().Color;
        bool Color4 = GameObject.Find("Node4").GetComponentInChildren<Node>().Color;
        

        var Money = GameObject.Find("GameManager").GetComponent<gameManager>();
        if(Money.score > 99){
        if(Color1==false){
            tower1 = Instantiate(tempGO[Random.Range(0,2)], SelectNode.transform.position, Quaternion.identity);
            gameManager.instance.AddScore(-100);
            GameObject.Find("Node1").GetComponent<Node>().NodeD();
        }else if(Color2 == false){
            tower1 = Instantiate(tempGO[Random.Range(0,2)], SelectNode.transform.position, Quaternion.identity);
            gameManager.instance.AddScore(-100);
            GameObject.Find("Node2").GetComponent<Node>().NodeD();
        }else if(Color3 == false){
            tower1 = Instantiate(tempGO[Random.Range(0,2)], SelectNode.transform.position, Quaternion.identity);   
            gameManager.instance.AddScore(-100);
            GameObject.Find("Node3").GetComponent<Node>().NodeD();
        }else if(Color4 == false){
            tower1 = Instantiate(tempGO[Random.Range(0,2)], SelectNode.transform.position, Quaternion.identity);
            gameManager.instance.AddScore(-100);
            GameObject.Find("Node4").GetComponent<Node>().NodeD();
    }else{
        Debug.Log("NO Money");
    }
        }
    }
}

