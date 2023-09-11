using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityObj : MonoBehaviour, IGravity
{
    private Vector3 gravityDir = new Vector3(0, -9.81f, 0);
    public Vector3 GravityDir
    {
        get => gravityDir;
        set => gravityDir = value;
    }

    private Rigidbody2D rigid;
    private void Start()
    {
        rigid ??= GetComponentInChildren<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    private void Gravity()
    {
        rigid.AddForce(GravityDir);
    }


}
