using UnityEngine;

public class MoveController {

    public static void MoveControl(Transform objTransform, Vector3 moveDir, float speed) {
        objTransform.rotation = Quaternion.LookRotation(moveDir.x * Vector3.right + moveDir.z * Vector3.forward);
        //objTransform.Translate(Vector3.forward * 10 * Time.fixedDeltaTime);
        RigidMovePos(objTransform, moveDir, speed);
    }

    public static void LookTarget(Transform objTransform, Transform targetTransform, float speed) {
        Vector3 dir = new Vector3(objTransform.position.x - targetTransform.transform.position.x, 0, objTransform.position.z - targetTransform.transform.position.z);
        objTransform.rotation = Quaternion.Lerp(objTransform.rotation, Quaternion.LookRotation(-dir), speed * Time.fixedDeltaTime);
    }

    public static void RigidMovePos(Transform objTransform, Vector3 dir, float speed) {
        objTransform.gameObject.GetComponent<Rigidbody>().MovePosition(objTransform.position + new Vector3(dir.x, 0, dir.z).normalized * speed * Time.fixedDeltaTime);
    }

}