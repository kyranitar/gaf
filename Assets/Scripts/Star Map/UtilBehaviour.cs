using UnityEngine;

public abstract class UtilBehaviour : MonoBehaviour {

  protected Vector3 ThisY(Vector3 vector) {
    return new Vector3(vector.x, transform.position.y, vector.z);
  }

  protected Vector3 IgnoreY(Vector3 vector) {
    return new Vector3(vector.x, 0, vector.z);
  }

}
