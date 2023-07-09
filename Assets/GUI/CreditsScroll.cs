using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    [SerializeField] private float ScrollSpeed;
    [SerializeField] private Transform ScrollTransform;

    private float BaseScrollSpeed;
    private bool Scrolling = true;
    private Input Input;

    // Start is called before the first frame update
    void Start()
    {
        Input = new Input();
        Input.Player1.Enable();

        BaseScrollSpeed = ScrollSpeed;

        StartCoroutine(_Scroll());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.Player1.Jump.IsPressed())
        {
            BaseScrollSpeed = ScrollSpeed * 4;
        }
        else
        {
            BaseScrollSpeed = ScrollSpeed;
        }
    }

    private IEnumerator _Scroll()
    {
        yield return new WaitForSeconds(3f);

        while (Scrolling)
        {
            ScrollTransform.position += Vector3.up * BaseScrollSpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        AudioManager.Instance.FadeOut("Credits", 1);
        LevelManager.Instance.LoadLevel(1, Transition.Heart);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Scrolling = false;
    }
}