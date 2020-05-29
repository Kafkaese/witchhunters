using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kitchen : MonoBehaviour
{
    // Production Queue
    private Queue<ProductionQueueSlot> _productionQueue = new Queue<ProductionQueueSlot>();

    // Refs to Description Panel: Title, Description
    [SerializeField]
    private Text _titleText;
    [SerializeField]
    private Text _descrptionText;


}
