using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateParent
{
    protected PlayerMovement player;

    public virtual void OnValidate(PlayerMovement _player)
    {
        this.player = _player;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {

    }
}
