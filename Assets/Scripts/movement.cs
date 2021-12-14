using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] public Rigidbody2D character;
    float movX, movY;
    float speed = 10f;
    [SerializeField] public float mareo = 0;


    // Start is called before the first frame update
    void Start()
    {

        character = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movX = Input.acceleration.x * speed;
        movY = Input.acceleration.y * speed;

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y);
    }

    private void FixedUpdate()
    {
        character.velocity = new Vector2(movX, movY);
       // checkMareo();
    }
    private void checkMareo()
    {

        if (Mathf.Abs(character.velocity.x) > 0.5 || Mathf.Abs(character.velocity.y) > 0.5)
            mareo += 0.02f;
        else
            mareo -= 0.01f;

        if (mareo < 0)
            mareo = 0;

        if (mareo > 1)
            mareo = 1;

        //spriteShape.spriteShapeRenderer.color = new Color(original.r + mareo * 1.3f, original.g - mareo * 1.3f, original.b - mareo * 1.3f, original.a);

        //MareoAlert.SetActive(mareo > 0.5);

        Debug.Log("-------\n" + mareo + "\n--------\n" + "|" + character.velocity.x * 5 + "\n-------");

    }
}
