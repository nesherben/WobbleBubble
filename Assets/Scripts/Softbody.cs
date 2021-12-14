using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Softbody : MonoBehaviour
{
    [SerializeField] public GameObject wall1, wall2, rooft, floor;
    [SerializeField] public float mareo = 0;
    [SerializeField] public GameObject MareoAlert;

    [SerializeField] public Rigidbody2D character;

    float movX, movY;
    Color original;
    public float speed = 0.005f;

    private const float splineOffset = 0.5f;
    [SerializeField] public SpriteShapeController spriteShape;
    [SerializeField] public Transform[] points;

    private void Awake()
    {
        character = GetComponent<Rigidbody2D>();
        original = spriteShape.spriteShapeRenderer.color;
        UpdateVerticies();
    }

    private void Update()
    {
        movX = Input.acceleration.x * speed;
        movY = Input.acceleration.y * speed;

        UpdateVerticies();
    }
    private void FixedUpdate()
    {
        checkMareo();
    }
    private void checkMareo()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, 0, 0), Mathf.Clamp(transform.position.y, 0, 0));
        character.velocity = new Vector2(movX, movY);

        if (Mathf.Abs(character.velocity.x) > 0.5 || Mathf.Abs(character.velocity.y) > 0.5)
            mareo += 0.02f;
        else
            mareo -= 0.01f;

        if (mareo < 0)
            mareo = 0;

        if (mareo > 1)
            mareo = 1;

        spriteShape.spriteShapeRenderer.color = new Color(original.r + mareo * 1.3f, original.g - mareo * 1.3f, original.b - mareo * 1.3f, original.a);

        MareoAlert.SetActive(mareo > 0.5);

        Debug.Log("-------\n" + mareo + "\n--------\n" + "|" + character.velocity.x * 5 + "\n-------");

    }
    private void UpdateVerticies()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {           
            Vector2 _vertex = points[i].localPosition;
            Vector2 _towardsCenter = (Vector2.zero - _vertex).normalized;

            float _colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;
            try
            {
                spriteShape.spline.SetPosition(i, (_vertex - _towardsCenter * _colliderRadius));
            }
            catch
            {
                Debug.Log("Spline points are too close");
                spriteShape.spline.SetPosition(i, (_vertex - _towardsCenter * (_colliderRadius + splineOffset)));
            }            
            
            Vector2 _lt = spriteShape.spline.GetLeftTangent(i);
            Vector2 _newRt = Vector2.Perpendicular(_towardsCenter) * _lt.magnitude;
            Vector2 _newLt = Vector2.zero - (_newRt);

            spriteShape.spline.SetRightTangent(i, _newRt);
            spriteShape.spline.SetLeftTangent(i, _newLt);

        }
    }
}
