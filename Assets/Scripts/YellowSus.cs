using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowSus : MonoBehaviour
{
    private enum RunDirection {
        LEFT,
        RIGHT
    };
    [SerializeField] private RunDirection runDirection = RunDirection.LEFT;
    [SerializeField] private float meanTime = 7f;
    [SerializeField] private float stdTime = 5f;
    [SerializeField] private GameObject dieText;

    private bool startMovement = false, showText = false;

    public Player playerScript;
    public Animator animator;
    public GameObject yellowSus;

    private void Start()
    {
        StartCoroutine(InitAction(0.5f));
        animator = GetComponent<Animator>();
        playerScript = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<Player>();
    }

    private IEnumerator InitAction(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        startMovement = true;

        seconds = Random.Range(meanTime - stdTime, meanTime + stdTime);
        StartCoroutine(InitAction(seconds));
    }

    private void StartYellowSus()
    {
        if (runDirection == RunDirection.RIGHT)
        {
            animator.SetTrigger("YellowSusRightTrigger");
            runDirection = RunDirection.LEFT;
        }
        else if (runDirection == RunDirection.LEFT)
        {
            animator.SetTrigger("YellowSusLeftTrigger");
            runDirection = RunDirection.RIGHT;
        }

        startMovement = false;
    }

    private void Update()
    {
        if (startMovement) {
            StartYellowSus();
        }

        dieText.SetActive(showText);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        showText = true;
        playerScript.Respawn();
        StartCoroutine(DisableTextAfterSeconds(1f));
    }

    private IEnumerator DisableTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        showText = false;
    }
}
