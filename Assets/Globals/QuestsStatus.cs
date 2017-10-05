using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsStatus : MonoBehaviour
{
    public static int testQuestStatus;
    //-1 = inactive; 0 = activated trigger event; 10 = left blackwardsmith before getting any info; 20 = left after learning he doesn't smith anymore
    //-10: Threatened him, quest unable to be completed; 30 = left after learning of daughter; 40 = Accepted quest; 50 = found daughter

    public void NewGame()
    {
        Debug.Log("Start gaem");
        testQuestStatus = -1; //Do this for all quest statuses
    }
}
