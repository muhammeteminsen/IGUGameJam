using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    [Header("Mana Variables")] [SerializeField]
    private Image manaBarImage;

    [SerializeField] private float manaBarAcceleration = 1.5f;
    [SerializeField] private float manaBarDeclaration = 1.5f;
    [SerializeField] private float onDestroyMana = 20f;

    [Header("FreezeSkill Variables")] [SerializeField]
    private float freezeSkillBarAcceleration = 1.5f;

    [SerializeField] private float freezeSkillBarDeclaration = 1.5f;
    [SerializeField] private Image freezeSkillImage;
    public bool isFreeze;

    [Header("Player Speed Skill Variables")] [SerializeField]
    private float speedSkillBarAcceleration = 1.5f;

    [SerializeField] private float speedSkillBarDeclaration = 1.5f;
    [SerializeField] private Image speedSkillImage;
    public bool isSpeedSkill;

    [Header("Enemy Slow Skill Variables")] [SerializeField]
    private float slowSkillBarAcceleration = 1.5f;

    [SerializeField] private float slowSkillBarDeclaration = 1.5f;
    [SerializeField] private Image slowSkillImage;
    public bool isSlowSkill;

    void Start()
    {
        manaBarImage.fillAmount = 0f;
    }


    void Update()
    {
        if (!isFreeze || !isSpeedSkill || !isSlowSkill)
        {
            manaBarImage.fillAmount += manaBarAcceleration * Time.deltaTime;
        }

        ManageSkill(KeyCode.E, ref isFreeze, freezeSkillImage, freezeSkillBarAcceleration, freezeSkillBarDeclaration);
        ManageSkill(KeyCode.Q, ref isSpeedSkill, speedSkillImage, speedSkillBarAcceleration, speedSkillBarDeclaration);
        ManageSkill(KeyCode.T, ref isSlowSkill, slowSkillImage, slowSkillBarAcceleration, slowSkillBarDeclaration);
    }

    public void SetMana()
    {
        manaBarImage.fillAmount += onDestroyMana / 100f;
    }

    private void ManageSkill(KeyCode key, ref bool skillActive, Image skillImage, float skillAcceleration,
        float skillDeclaration)
    {
        if (Input.GetKeyDown(key) && manaBarImage.fillAmount > 0)
        {
            skillActive = !skillActive;
            if (skillActive)
            {
                manaBarImage.fillAmount -= manaBarDeclaration / 100f;
            }
        }

        if (skillActive)
        {
            if (manaBarImage.fillAmount > 0)
            {
                skillImage.fillAmount -= skillDeclaration * Time.deltaTime;
                if (skillImage.fillAmount <= 0)
                {
                    skillActive = false;
                }
            }
            else
            {
                skillActive = false;
            }
        }
        else
        {
            skillImage.fillAmount += skillAcceleration * Time.deltaTime;
        }
    }
}