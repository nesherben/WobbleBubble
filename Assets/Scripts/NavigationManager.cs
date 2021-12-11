using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    [SerializeField] public Stack<GameObject> NavigationStack = new Stack<GameObject>();

    public void Awake()
    {
        NavigationStack.Clear();
        NavigationStack.Push(GameObject.Find("MainScreen"));
    }
    public void CanvasNavigation(GameObject target)
    {
        NavigationStack.Peek().SetActive(false);
        target.gameObject.SetActive(true);
        NavigationStack.Push(target);
    }
    public void BackGestureNavigation()
    {
        if (NavigationStack.Count > 0)
        {
            NavigationStack.Pop().SetActive(false);
            NavigationStack.Peek().SetActive(true);
        }
        else
        {
            CloseApp();
        }
    }
    public void SceneNavigation(string target)
    {
        NavigationStack.Clear();

        SceneManager.LoadScene(target);
        //Find new main screen
        NavigationStack.Push(GameObject.FindGameObjectWithTag("MainScreen"));
    }
    public void ActiveByTagNavigation(string tag)
    {
        GameObject activate = GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).gameObject;

        NavigationStack.Peek().SetActive(false);
        activate.SetActive(true);
        NavigationStack.Push(activate);
    }

    public void CloseApp()
    {
        //should make a confirmation dialog but xd
        Application.Quit();
    }
}
