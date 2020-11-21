using System.Collections;
using UnityEngine;

public enum CharacterState {
    Idle = 0,
    Walk = 1,
    Attack = 2,
    Trace = 3,  //몬스터가 플레이어를 쫓아가는 상태
    Die = 4,

    //보스
    Attack_Jump = 5,

    Attack_Fire = 6,
    Dizzy = 7,
    Victory = 8,
}

public class CharacterFSM : MonoBehaviour {
    protected CharacterBase characterBase;
    protected PlayerBase playerBase;
    public CharacterState state { get; set; }

    //캐릭터의 상태가 바꼈는지 체크하는 변수.
    protected bool IsNewState { get; set; }

    protected virtual void Awake() {
        characterBase = GetComponent<CharacterBase>();
        playerBase = GameManager.Instance.player;
    }

    //모든 캐릭터는 처음에 Idle 상태이며, FSMMain 코루틴을 실행
    protected virtual void OnEnable() {
        state = CharacterState.Idle;
        StartCoroutine(FSMMain());
    }

    private IEnumerator FSMMain() {
        //상태가 바뀌면 해당 상태 코루틴 메소드를 실행
        while(true) {
            IsNewState = false;
            yield return StartCoroutine(state.ToString());
        }
    }

    //캐릭터 상태가 바뀔때마다 메소드 실행
    public void SetState(CharacterState newState) {
        if(state != newState) {
            IsNewState = true;
            state = newState;

            //해당 캐릭터의 애니메이터에 상태 값을 전달
            characterBase.Anim.SetInteger("state", (int)state);
        }
    }

    protected virtual IEnumerator Die() {
        do {
            yield return null;
        } while(!IsNewState);
    }
}