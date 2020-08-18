using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] private Door myDoor = null;
    private int EnemiesOnSwitch = 0;

    public enum SwitchType
    {
        StayDown,
        PressSensitive
    }

    public SwitchType myType = SwitchType.StayDown;

    public int enemiesOnSwitch
    {
        get{ return EnemiesOnSwitch; }
        set 
        {
            //Every time an enemy steps on or steps off the switch, it checks if there are enemies on the switch. If there aren't, close the door. 
            EnemiesOnSwitch = value; 
            if (value <= 0)
            {
                switch (myType)
                {
                    case SwitchType.StayDown:
                        break;
                    case SwitchType.PressSensitive:
                        myDoor.Close();
                        break;
                }
            }
            else
            {
                myDoor.Open();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If enemy steps on switch
        if (collision.GetComponent<EnemyParent>() != null)
        {
            enemiesOnSwitch++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //If enemy steps off switch
        if (collision.GetComponent<EnemyParent>() != null)
        {
            enemiesOnSwitch--;
        }
    }
}
