using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogBox : MonoBehaviour
{
    public TextMeshProUGUI dialogTextMesh;
    public Button YesButton;
    public Button NoButton;
    

    public void PushYesButton()
    {

    }

    public void printDialog(string dialog)
    {

        dialogTextMesh.text = dialog;
    }
}
