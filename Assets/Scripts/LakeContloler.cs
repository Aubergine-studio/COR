using UnityEngine;

public class LakeContloler : MonoBehaviour
{
    public float CameraSpeed = 2.0f;
    public float SeckendSpeed = 0.8f;
    public float ThierdSpeed = 0.5f;

    public CameraMove camera;
    public CameraMove seckend_a;
    public CameraMove seckend_b;
    public CameraMove thierd_a;
    public CameraMove thierd_b;
    public CameraMove thierd_c;
    public CameraMove thierd_d;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        camera.Speed = CameraSpeed;
        seckend_a.Speed = seckend_b.Speed = SeckendSpeed *-1;
        thierd_a.Speed = thierd_b.Speed = thierd_c.Speed = thierd_d.Speed = ThierdSpeed * -1;
    }
}