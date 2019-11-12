using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour
{
    public List<Button> selections;
    float waitTime = 0.5f;
    int curr = 0;
    // Start is called before the first frame update
    void Start()
    {
        selections[curr].Select();
        //selections[curr].OnSelect(null);
    }

    // Update is called once per frame
    void Update()
    {
        //
        float r = Input.GetAxis("Vertical");
        if (Mathf.Abs(r) > 0.5 && waitTime < 0)
        {
            if (r < 0)
            {
                selectDown();
            } else {
                selectUp();
            }
            waitTime = 0.5f;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            selections[curr].onClick.Invoke();
        }
        waitTime -= Time.deltaTime;
    }

    void selectDown()
    {
        curr = (curr + 1) % selections.Capacity;
        if (selections[curr].interactable)
        {
            selections[curr].Select();
        } else
        {
            selectDown();
        }
    }

    void selectUp()
    {
        curr = curr - 1;
        if (curr < 0)
        {
            curr = selections.Capacity - 1;
        }
        if (selections[curr].interactable)
        {
            selections[curr].Select();
        }
        else
        {
            selectUp();
        }
    }
}
