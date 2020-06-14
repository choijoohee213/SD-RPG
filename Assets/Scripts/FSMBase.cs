using System.Collections;
using UnityEngine;

public enum CharacterState {
    Idle = 0,
    Walk = 1,
    Attack = 2,
    Trace = 3  //몬스터가 플레이어를 쫓아가는 상태
}

public class FSMBase : MonoBehaviour {
    public Animator anim;
    public Rigidbody rigid;
    public Vector3 movePos;

    //캐릭터(플레이어,몬스터)의 상태변화를 제어하는 변수
    public CharacterState state;

    //캐릭터의 상태가 바꼈는지 체크하는 변수.
    public bool isNewState;

    protected virtual void Awake() {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    //모든 캐릭터는 처음에 Idle 상태이며, FSMMain 코루틴을 실행
    protected virtual void OnEnable() {
        state = CharacterState.Idle;
        StartCoroutine(FSMMain());
    }

    private IEnumerator FSMMain() {
        //상태가 바뀌면 해당 상태 코루틴 메소드를 실행
        while(true) {
            isNewState = false;
            yield return StartCoroutine(state.ToString());
        }
    }

    //캐릭터 상태가 바뀔때마다 메소드 실행
    public void SetState(CharacterState newState) {
        if(state != newState) {
            isNewState = true;
            state = newState;

            //해당 캐릭터의 애니메이터에 상태 값을 전달
            anim.SetInteger("state", (int)state);
        }
    }

}