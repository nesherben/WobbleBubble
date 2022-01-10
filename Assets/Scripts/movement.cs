using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] public Rigidbody2D character;
    [SerializeField] public GameObject MareoAlert;
    float movX, movY;
    float speed = 5f;
    [SerializeField] public float mareo = 0;
    public Color original;


    // Start is called before the first frame update
    void Start()
    {
        original = GetComponentInParent<SpriteRenderer>().color;
        character = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movX = Input.acceleration.x * speed;
        movY = Input.acceleration.y > 0? Input.acceleration.y * speed*2 : Input.acceleration.y;

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y);
    }

    private void FixedUpdate()
    {
        character.velocity = new Vector2(movX, movY);
        checkMareo();
    }
    private void checkMareo()
    {
        if (Mathf.Abs(character.velocity.x) > 2)
            mareo += 0.01f;
        else
        {
            if (!MareoAlert.activeSelf)
            {
                mareo -= 0.01f;
            }
            else
            {
                mareo -= 0.002f;
            }
        }

        if (mareo < 0)
            mareo = 0;

        if (mareo > 1)
            mareo = 1;

        character.GetComponentInParent<SpriteRenderer>().color = new Color(original.r + mareo * 0.7f, original.g - mareo * 0.7f, original.b - mareo * 0.7f, original.a);

        MareoAlert.SetActive(mareo > 0.5);

        Debug.Log("-------\n" + mareo + "\n--------\n" + "|" + character.velocity.x * 5 + "\n-------");

    }
}
