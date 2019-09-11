using UnityEngine;
using System.Collections;

public class SkillsController : MonoBehaviour
{
    public ISkill[] Skills;
    // Use this for initialization
    void Start()
    {
    }

    public void UseSkill()
    {
        Skills[0].Activate();
    }
}
