using UnityEngine;

public abstract class UtilBehaviour : MonoBehaviour {

  protected Vector3 ThisZ(Vector3 vector) {
    return new Vector3(vector.x, vector.y, transform.position.z);
  }

  protected Vector3 IgnoreZ(Vector3 vector) {
    return new Vector3(vector.x, vector.y, 0);
  }

}
