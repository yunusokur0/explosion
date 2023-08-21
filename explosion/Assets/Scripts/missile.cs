using UnityEngine;

public class missile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject[] _target;
    [SerializeField] private ParticleSystem _explosionPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _maxDistancePredict;
    [SerializeField] private float _minDistancePredict;
    [SerializeField] private float _maxTimePrediction;
    public int value;
    [SerializeField] private GameObject particle;
    private ParticleSystem jetPackParticleSystem;
    private ParticleSystem jetPackParticleSystem2;
    private void Start()
    {
        jetPackParticleSystem = particle.GetComponent<ParticleSystem>();
        jetPackParticleSystem2 = _explosionPrefab.GetComponent<ParticleSystem>();
    }
    private void FixedUpdate()
    {
        jetPackParticleSystem.Play();
        rb.velocity = transform.forward * _speed;

        var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, Vector3.Distance(transform.position, _target[value].transform.position));
        PredictMovement(leadTimePercentage);

        RotateRocket();
    }

    private void PredictMovement(float leadTimePercentage)
    {
        var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);
    }
    private void RotateRocket()
    {
        var heading = _target[value].transform.position - transform.position;
        var rotation = Quaternion.LookRotation(new Vector3(heading.x, heading.y, heading.z));
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hedef"))
        {
            value++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("hedef1"))
        {

            jetPackParticleSystem2.Play();
            gameObject.SetActive(false);
        }
    }
}
