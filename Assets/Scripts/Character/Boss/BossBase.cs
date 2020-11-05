using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : CharacterBase
{    
    Vector3 BossPos = new Vector3(-29.3f, 21.5f, 248.7f);
    public bool AttackJumping { get; set; }
    public bool AttackFiring { get; set; }


    public void AttackJumpAnimEvent() {
        AttackJumping = false;
    }

    public void AttackFireAnimEvent() {
        AttackJumping = true;
    }


}
