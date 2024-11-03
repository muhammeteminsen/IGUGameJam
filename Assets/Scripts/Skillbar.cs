using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    public Slider skillSlider;
    public float skillValue = 0;
    public float maxSkillValue = 100;
    public float skillIncrement = 1;
    public float killIncrement = 5;
    public float timeToIncrease = 1f; // Her 1 saniyede bir artýþ

    private void Start()
    {
        skillSlider.maxValue = maxSkillValue;
        skillSlider.value = skillValue;
        StartCoroutine(IncreaseSkillValue());
    }

    private IEnumerator IncreaseSkillValue()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToIncrease);
            skillValue += skillIncrement;
            if (skillValue > maxSkillValue)
                skillValue = maxSkillValue;

            skillSlider.value = skillValue;
        }
    }

    public void EnemyKilled()
    {
        skillValue += killIncrement;
        if (skillValue > maxSkillValue)
            skillValue = maxSkillValue;

        skillSlider.value = skillValue;
    }
}


