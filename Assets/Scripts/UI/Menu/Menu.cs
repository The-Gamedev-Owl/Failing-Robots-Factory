using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Button[] buttonsToDeactivate;

    private Animator animator;

    private void Start()
    {
        if (buttonsToDeactivate == null)
            buttonsToDeactivate = new Button[0];
        animator = GetComponent<Animator>();
    }

    public void Play()
    {
        DeactivateButtons();
        animator.SetTrigger("Play");
        StartCoroutine(PassAnimation());
    }

    public void ShowHighScore()
    {
        animator.SetTrigger("HighScore");
    }

    private IEnumerator PassAnimation()
    {
        yield return new WaitForSeconds(1f);
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Play"))
        {
            if (Input.touchCount > 0)
               animator.CrossFade("EndPlay", 0.25f);
            yield return null;
        }
    }

    private void DeactivateButtons()
    {
        foreach (Button button in buttonsToDeactivate)
            button.interactable = false;
    }
}
