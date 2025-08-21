using System.Collections.Generic;
using UnityEngine;

public class UIManage : MonoBehaviour
{
    public GameObject Menu, Setting, Creadit, Sound;
    private Stack<GameObject> _pageStack;
    public GameObject DefaultPage; 

    public void SwitchPage(GameObject page)
    {
        page.SetActive(true);
        _pageStack.Push(page);
    }
    private void Awake()
    {
        _pageStack = new Stack<GameObject>();
    }

    public void Undo()
    {
        if (_pageStack.Count > 0)
        {
            var topPage = _pageStack.Pop();
            topPage.SetActive(false);

            if (_pageStack.Count > 0)
            {
                _pageStack.Peek().SetActive(true);
            }
            else if (DefaultPage != null)
            {
                DefaultPage.SetActive(true);
            }
        }
    }

}
