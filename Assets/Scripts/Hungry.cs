using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hungry : MonoBehaviour
{
    [SerializeField] public GameObject wobble;
    [SerializeField] public GameObject stacksNumber;
    [SerializeField] public GameObject HungryAlert;
    public float hambre = 1.0f;
    public int stacks = 0;
    float contador = 10;
    void FixedUpdate()
    {
        if (hambre > 0)
        {
            hambre -= 0.05f * Time.deltaTime;
        }
        contador -= 1 * Time.deltaTime;

        if (contador < 0 && stacks < 10)
        {
            contador = 10;
            stacks++;
        }
        stacksNumber.GetComponent<TextMeshProUGUI>().text = stacks.ToString();
        HungryAlert.SetActive(hambre < 0.3f);
    }
    public void eat()
    {
        if (hambre < 0.8 && stacks > 0)
        {
            hambre += 0.5f;
            stacks -= 1;
            if (hambre > 1.2f)
            {
                for (int i = 0; i < 9; i++)
                {
                    wobble.GetComponentInChildren<Transform>().GetChild(i).GetComponent<Transform>().localScale *= 1.1f;
                }
            }
        }

    }
}
