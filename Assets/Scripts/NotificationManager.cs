using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    //Inbox
    
    [SerializeField]
    private List<Letter> _inbox = new List<Letter>();
    
    [SerializeField]
    private Queue<GameObject> _inboxSlots = new Queue<GameObject>();
    
    [SerializeField]
    private SimpleObjectPool _inboxSlotPool;


    //


    [SerializeField]
    private IconFlash _newMessageIcon_TTB;

    [SerializeField]
    private Transform _inboxContentPanel;

    [SerializeField]
    private GameObject _messagePrefab;

    // Message Display
    [SerializeField]
    private Text _messageDisplay_Title;
    [SerializeField]
    private Text _messageDisplay_Body;
    private Letter _currentLetter;

    [SerializeField]
    private Sprite _openLetter;
    [SerializeField]
    private Sprite _closedLetter;

    [SerializeField]
    private TimeKeeper _timeKeeper;

    [SerializeField]
    private UIManager _uiManager;

    public void OnEnable()
    {
        PopulateInbox();
    }

    public void SpawnMessage(string message, string source, string type, bool important)
    {
        // Instantiate prefab
        Letter newLetter = new Letter();
            //Instantiate(_messagePrefab).GetComponent<Letter>();
        
        // Populate letter instance attached to prefab instance
        newLetter.Message = message;
        newLetter.Source = source;
        newLetter.Type = type;
        newLetter.Important = important;
        newLetter.Timestamp = _timeKeeper.GetTimeStamp();

        // Add Letter to Inbox;
        _inbox.Add(newLetter);

        if (this.gameObject.activeSelf)
        {
            PopulateInbox();
        }

        _newMessageIcon_TTB.NewMessage = true;

    }

    public void DisplayMessage(LetterSlot letterSlot)
    {
        Letter letter = letterSlot.RefLetter;

        // First time opened change icon and set seen true
        if(letter.Seen == false)
        {
            letterSlot.SetLetterHeadIcon(_openLetter);
            letter.Seen = true;
        }

        _messageDisplay_Title.text = letter.Type;

        _messageDisplay_Body.text = letter.Message;

        _currentLetter = letter;


        // Check if there are any more new letters
        bool _newOne = false;

        foreach (Letter iletter in _inbox)
        {
            if (!iletter.Seen)
            {
                _newOne = true;
            }
        }

        _newMessageIcon_TTB.NewMessage = _newOne;
    }

    private void PopulateInbox()
    {
        

        int rsc = _inboxSlots.Count;

        for (int i = 0; i < rsc; i++)
        {
            GameObject slot = _inboxSlots.Dequeue();
            //Debug.Log("Slot returned");
            _inboxSlotPool.ReturnObject(slot);
        }

        for (int i = 0; i < _inbox.Count; i++)
        {
            //Debug.Log("i 0 " + i + ". Getting new slot");
            Letter letter = _inbox[i];
            GameObject newSlot;

            

            newSlot = _inboxSlotPool.GetGameObject();

            //= GameObject.Instantiate(_slotPrefab, _contentPanel);
            LetterSlot letterSlot = newSlot.GetComponent<LetterSlot>();
            letterSlot.RefLetter = letter;
            newSlot.transform.SetParent(_inboxContentPanel);
            letterSlot.SetMessageText(letter.Type);
            if(letter.Seen)
            {
                letterSlot.SetLetterHeadIcon(_openLetter);
            }
            else
            {
                letterSlot.SetLetterHeadIcon(_closedLetter);
            }

            letterSlot.SetImportant(letter.Important);

            letterSlot.SetManager(this);

            _inboxSlots.Enqueue(newSlot);

        }


    }

    public void GoToLetterSource()
    {
        string letterSource = _currentLetter.Source;

        _uiManager.CallMethodWithString(letterSource);
    }
}
