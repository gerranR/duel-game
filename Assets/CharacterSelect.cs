using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterSelect : MonoBehaviour
{
    public Animator playerAnim, armAnim;
    public int ChosenPlayer;
    public PlayerVariant[] playerVariants;
    private GameObject armPos1Object, armPos2Object, armPivotObject;
    public Vector3 armPos1, armPos2, foxArmPos1, foxArmPos2;
    // Start is called before the first frame update

    [System.Serializable]
    public class PlayerVariant
    {
        public RuntimeAnimatorController playerAnimControl, armAnimControl;
        public bool foxType;
    }

    void Start()
    {
        ChosenPlayer = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ArrowRight()
    {
        if (ChosenPlayer < playerVariants.Length - 1)
        {
            ChosenPlayer += 1;
        }
        else
        {
            ChosenPlayer = 0;
        }
        ChoosePlayer();
    }
    public void ArrowLeft()
    {
        if (ChosenPlayer > 0)
        {
            ChosenPlayer -= 1;
        }
        else
        {
            ChosenPlayer = playerVariants.Length -1;
        }
        ChoosePlayer();
    }

    public void ChoosePlayer()
    {
        armPivotObject = playerAnim.gameObject.transform.Find("ArmPivot").gameObject;
        armPos1Object = playerAnim.gameObject.transform.Find("ArmPos1").gameObject;
        armPos2Object = playerAnim.gameObject.transform.Find("ArmPos2").gameObject;

        playerAnim.runtimeAnimatorController = playerVariants[ChosenPlayer].playerAnimControl;
        armAnim.runtimeAnimatorController = playerVariants[ChosenPlayer].armAnimControl;
        //if (playerVariants[ChosenPlayer].foxType)
        //{
        //    armPivotObject.transform.localPosition = foxArmPos1;
        //    armPos1Object.transform.localPosition = foxArmPos1;
        //    armPos2Object.transform.localPosition = foxArmPos2;
        //}
        //else
        //{
        //    armPivotObject.transform.localPosition = armPos1;
        //    armPos1Object.transform.localPosition = armPos1;
        //    armPos2Object.transform.localPosition = armPos2;
        //}
    }

}
