using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollTable : MonoBehaviour
{
    int isSpinning = 1;
    [HideInInspector] public int diceNumber;
    private Animator animator;
    [SerializeField] private GameObject textObject;
    private TextMeshProUGUI diceText;

    private Vector2 startPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        diceText = textObject.GetComponent<TextMeshProUGUI>();
    }

    public void Spin() {
        isSpinning = 1;
        textObject.SetActive(false);
        transform.position = new Vector2(startPosition.x, startPosition.y - 2);

        animator.SetInteger("spinning", isSpinning);
    }

    public void StopSpinning() {
        isSpinning = 0;
        animator.SetInteger("spinning", isSpinning);

        diceNumber = Random.Range(0, 6);
        int tempNumber = diceNumber + 1;
        diceText.SetText(tempNumber.ToString());

        textObject.SetActive(true);
        transform.position = startPosition;
    }
}
