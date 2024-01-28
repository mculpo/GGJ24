using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject card1;
    public GameObject card2;

    private Vector3 originPosition;

    void Awake()
    {
        originPosition = card1.GetComponent<Transform>().position;
    }

    public void Default()
    {
        card1.transform.position = originPosition;
        card2.transform.position = originPosition;
    }
}
