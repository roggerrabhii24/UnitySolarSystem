using UnityEngine;

public class Starfield : MonoBehaviour
{
    [Tooltip("Max number of particles at once")]
    public int StarsMax = 2000;
    [Tooltip("Size of dust particles")]
    public float StarSize = 0.5f;
    [Tooltip("Max render distance")]
    public float StarDistance = 500;
    [Tooltip("Close clip distance")]
    public float StarClipDistance = 1;

    private ParticleSystem.Particle[] _points;
    private ParticleSystem _ps;
    private float _starDistanceSqr;
    private float _starClipDistanceSqr;

    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        _starDistanceSqr = StarDistance * StarDistance;
        _starClipDistanceSqr = StarClipDistance * StarClipDistance;
    }

    private void CreateStars()
    {
        _points = new ParticleSystem.Particle[StarsMax];

        for (int i = 0; i < StarsMax; i++)
        {
            _points[i].position = Random.insideUnitSphere * StarDistance + transform.position;
            _points[i].startColor = new Color(1, 1, 1, 1);
            _points[i].startSize = StarSize;
        }
    }

    void Update()
    {
        if (_points == null) CreateStars();

        for (int i = 0; i < StarsMax; i++)
        {

            if ((_points[i].position - transform.position).sqrMagnitude > _starDistanceSqr)
            {
                _points[i].position = Random.insideUnitSphere.normalized * StarDistance + transform.position;
            }

            if ((_points[i].position - transform.position).sqrMagnitude <= _starClipDistanceSqr)
            {
                float percent = (_points[i].position - transform.position).sqrMagnitude / _starClipDistanceSqr;
                _points[i].startColor = new Color(1, 1, 1, percent);
                _points[i].startSize = percent * StarSize;
            }


        }

        _ps.SetParticles(_points, _points.Length);
    }
}