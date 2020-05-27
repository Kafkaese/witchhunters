using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterSlot : MonoBehaviour
{
    private NotificationManager _notificationManager;


    private Letter _refLetter;


    // Refto icon
    [SerializeField]
    private Image _letterheadIcon;

    [SerializeField]
    private Text _letterMessageText;

    [SerializeField]
    private Text _importantMarker;

    public Letter RefLetter { get => _refLetter; set => _refLetter = value; }

    public void SetLetterHeadIcon(Sprite icon)
    {
        _letterheadIcon.sprite = icon;
    }

    public void SetMessageText(string text)
    {
        _letterMessageText.text = text;
    }

    public void SetImportant(bool important)
    {
        _importantMarker.gameObject.SetActive(important);
    }

    public void IsClicked()
    {
        _notificationManager.DisplayMessage(this);
    }

    public void SetManager(NotificationManager man)
    {
        _notificationManager = man;
    }
}
