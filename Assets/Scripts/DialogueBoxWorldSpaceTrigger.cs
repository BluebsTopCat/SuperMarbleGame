using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBoxWorldSpaceTrigger : MonoBehaviour
{
    public bool destroyoncomplete = true;
    // Start is called before the first frame update
    public string[] text;
    public GameObject indicator;
    private Movement player;
    public TextMeshProUGUI textplace;
    public GameObject box;
    public GameObject continuebutton;
    private bool spcpressed;
    private bool indialogue = false;
    private bool intrigger = false;
    private void Start()
    {
        intrigger = false;
        player = GameObject.Find("Player").GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && intrigger)
        {
            Debug.Log("Starting Dialogue");
            player.playerfrozen = true;
            player.circle.simulated = false;
            if (indialogue == false)
                StartCoroutine(dialogue());
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        intrigger = true;
        indicator.SetActive(true);
        indicator.GetComponent<DialogueBox>().overlay = this.gameObject;
        box.GetComponent<DialogueBox>().overlay = this.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        indicator.SetActive(false);
        intrigger = false;
    }

    

    private IEnumerator dialogue()
    {
        textplace.text = null;
        indialogue = true;
        box.SetActive(true);
        for (int i = 0; i < text.Length;i++)
        {
            for (int d = 0; d < text[i].Length; d++)
            {
                textplace.text += text[i][d];
                yield return new WaitForSeconds(.025f);
            }
            continuebutton.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            continuebutton.SetActive(false);
            textplace.text = null;
        }
        box.SetActive(false);
        player.playerfrozen = false;
        indialogue = false;
        player.circle.simulated = true;
        if (destroyoncomplete)
            Destroy(gameObject);
        
    }
}
