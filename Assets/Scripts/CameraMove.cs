using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float Speed = 0.1f;
    private bool _run = false;
    private Vector3 startTransform;

    private void Start()
    {
        startTransform = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) _run = !_run;

        if (Input.GetKeyDown(KeyCode.UpArrow)) Speed += 0.05f;
        if (Input.GetKeyDown(KeyCode.DownArrow)) Speed -= 0.05f;

        if (Input.GetKeyDown(KeyCode.KeypadEnter)) transform.position = startTransform;

        if (_run)
            transform.position = new Vector3(transform.position.x + (Speed * Time.deltaTime), transform.position.y, transform.position.z);
    }
}