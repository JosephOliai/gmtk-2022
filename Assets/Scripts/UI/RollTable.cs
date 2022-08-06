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

    public void SetNumber(int number) {
        diceNumber = number;
        diceText.SetText(diceNumber.ToString());
    }

    public void StopSpinning() {
        isSpinning = 0;
        animator.SetInteger("spinning", isSpinning);

        textObject.SetActive(true);
        transform.position = startPosition;
    }
}
