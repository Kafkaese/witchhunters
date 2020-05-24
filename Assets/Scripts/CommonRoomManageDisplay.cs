using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonRoomManageDisplay : MonoBehaviour
{
    private PC _currentPC;

    public void AssignPC(PC pc)
    {
        _currentPC = pc;
    }

    public PC GetPC()
    {
        return _currentPC;
    }
}
