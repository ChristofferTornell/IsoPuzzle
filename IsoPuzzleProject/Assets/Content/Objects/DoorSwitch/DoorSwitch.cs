using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] private Door myDoor;
    private int EnemiesOnSwitch = 0;

    public int enemiesOnSwitch
    {
        get{ return EnemiesOnSwitch; }
        set 
        {
            //Every time an enemy steps on or steps off the switch, it checks if there are enemies on the switch. If there aren't, close the door. 
            EnemiesOnSwitch = value; 
            if (value <= 0)
            {
                myDoor.Close();
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
