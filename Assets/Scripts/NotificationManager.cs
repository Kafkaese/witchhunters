using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    private List<Letter> _inbox;

    [SerializeField]
    private GameObject _inboxConentPanel;

    [SerializeField]
    private GameObject _messagePrefab;

    [SerializeField]
    private Text _messageDisplay_Title;

    [SerializeField]
    private Text _messageDisplay_Body;

    [SerializeField]
    private Sprite _openLetter;

    //[SerializeField]
    //private Sprite _closedLetter;

    public void SendMessage(string message, Room source, string type, bool important)
    {
        // Instantiate prefab
        // Populate letter instance attached to prefab instance
    }

    public void DisplayMessage(Letter letter)
    {
        // First time opened change icon and set seen true
        if(letter.Seen == false)
        {
            letter.SetLetterHeadIcon(_openLetter);
            letter.Seen = true;
        }

        _messageDisplay_Title.text = letter.Type;

        _messageDisplay_Body.text = letter.Message;

        // Make Button go to source
    }
}
