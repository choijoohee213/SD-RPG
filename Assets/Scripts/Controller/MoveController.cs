using UnityEngine;

public class MoveController {

    public static void LookDirection(Transform objTransform, Vector3 moveDir) {
        objTransform.rotation = Quaternion.LookRotation(moveDir.x * Vector3.right + moveDir.z * Vector3.forward);
    }

    public static void LookTarget(Transform objTransform, Transform targetTransform, float speed) {
        Vector3 dir = new Vector3(objTransform.position.x - targetTransform.transform.position.x, 0, objTransform.position.z - targetTransform.transform.position.z);
        objTransform.rotation = Quaternion.Lerp(objTransform.rotation, Quaternion.LookRotation(-dir), speed * Time.fixedDeltaTime);
    }

    public static void RigidMovePos(Transform objTransform, Vector3 dir, float speed) {
        objTransform.gameObject.GetComponent<Rigidbody>().MovePosition(objTransform.position + new Vector3(dir.x, 0, dir.z).normalized * speed * Time.fixedDeltaTime);
    }

    public static void LimitMoveRange(Transform objTransform, Vector3 minRange, Vector3 maxRange) {
        objTransform.position = new Vector3(Mathf.Clamp(objTransform.position.x, minRange.x, maxRange.x), objTransform.position.y, Mathf.Clamp(objTransform.position.z, minRange.z, maxRange.z));
    }
}