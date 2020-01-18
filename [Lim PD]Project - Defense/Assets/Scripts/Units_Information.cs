using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units_Information
{
    public enum MonsterType
    {
        Archer,
        Warrior,
        Boss
    }

    private string name;
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    private float power;
    public float Power
    {
        get
        {
            return power;
        }
        set
        {
            power = value;
        }
    }

    private float speed;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    private float hp;
    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }


    public void initialize(int type)
    {
        switch (type)
        {
            case 0:
                name = "Me";
                power = 12;
                speed = 0.05f;
                hp = 200;
                break;
            case 1:
                name = "Hero";
                power = 5;
                speed = 0.03f;
                hp = 200;
                break;
            case 2:
                name = "Tower";
                power = 12;
                speed = 0.05f;
                hp = 200;
                break;
            case 3:
                name = "Archer";
                power = 5;
                speed = 0.07f;
                hp = 50;
                break;
            case 4:
                name = "Warrior";
                power = 10;
                speed = 0.1f;
                hp = 70;
                break;
            case 5:
                name = "Boss";
                power = 30;
                speed = 0.01f;
                hp = 500;
                break;
        }
    }


}
