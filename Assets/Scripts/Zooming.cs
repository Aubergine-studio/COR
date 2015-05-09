using UnityEngine;

public class Zooming : MonoBehaviour
{
    private Camera _camera;
    public float Stop = 6f;
    public float Speed = 0.1f;
    public Animator[] Dark;
    public bool ZoomIn = true;
    private bool Rining = false;
    public bool sqrsped = false;

    // Use this for initialization
    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Rining = !Rining;

        if (Rining)
        {
            if (_camera.orthographicSize > Stop || _camera.orthographicSize < Stop)
            {
                if (ZoomIn)
                    _camera.orthographicSize -= Speed * Time.deltaTime;
                else
                    _camera.orthographicSize += Speed * Time.deltaTime;
            }
            if (_camera.orthographicSize < Stop)
                foreach (var a in Dark)
                {
                    a.SetTrigger("Flood");
                }
        }
    }
}