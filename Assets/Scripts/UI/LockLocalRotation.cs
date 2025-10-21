using UnityEngine;

public class LockLocalRotation : MonoBehaviour
{
    private Quaternion _initialLocalRotation;
    private Quaternion _initialWorldRotation;


    private void Awake()
    {
        _initialLocalRotation = transform.localRotation;
        _initialWorldRotation = transform.localRotation;
    }

    private void LateUpdate()
    {
        transform.localRotation = _initialLocalRotation;
        transform.rotation = _initialWorldRotation;
    }

}
