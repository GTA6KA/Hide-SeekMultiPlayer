using System.ComponentModel;
using UnityEngine;

public class PlayerGameBehaviour : MonoCache
{
    [SerializeField] private Camera _camera;

    private MeshFilter _myMeshFilter;
    private MeshRenderer _myMeshRenderer;

    private Vector2 _cameraRayDirection;
    private void Start()
    {
        _myMeshFilter = GetComponent<MeshFilter>();
        _myMeshRenderer = GetComponent<MeshRenderer>();
        _cameraRayDirection = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    public override void OnTick()
    {
        ChangeModelBehaviour();
    }
    private void ChangeModelBehaviour()
    {
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(_cameraRayDirection);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.GetComponent<TestChangeMesh>())
                    {
                        if (hit.transform.TryGetComponent(out MeshRenderer meshRenderer)) _myMeshRenderer.materials = meshRenderer.materials;
                        if (hit.transform.TryGetComponent(out MeshFilter meshFilter)) _myMeshFilter.mesh = meshFilter.mesh;
                    }
                }
            }
        }
    }
}
