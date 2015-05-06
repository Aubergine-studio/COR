using UnityEngine;

public class Zooming : MonoBehaviour
{
    private Camera _camera;
    public float Stop = 6f;
    public float Speed = 0.1f;

    // Use this for initialization
    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_camera.orthographicSize > Stop)
            _camera.orthographicSize -= Speed * Time.deltaTime;
    }
}