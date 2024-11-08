using UnityEngine;
using UnityEngine.Rendering;


public class TimeAnamoly : MonoBehaviour
{
    [SerializeField] private float timeScaleInAnomalySlow=0.25f;
    [SerializeField] private float timeScaleInAnomalyFast=2f;
    [SerializeField] private VolumeProfile defaultVolumeProfile;
    [SerializeField] private VolumeProfile anomalyVolumeProfile;
    [SerializeField] private Volume mainVolume;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("AnomalySlow"))
        {
            Debug.Log("In Anomaly");
            Time.timeScale = timeScaleInAnomalySlow;
            mainVolume.profile = anomalyVolumeProfile;
        } 
        if (other.gameObject.CompareTag("AnomalyFast"))
        {
            Debug.Log("In Anomaly");
            Time.timeScale = timeScaleInAnomalyFast;
            mainVolume.profile = anomalyVolumeProfile;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AnomalySlow")||other.gameObject.CompareTag("AnomalyFast"))
        {
            Debug.Log("Out Anomaly");
            Time.timeScale = 1;
            mainVolume.profile = defaultVolumeProfile;
        }
    }
}
