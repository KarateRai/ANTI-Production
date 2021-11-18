using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast
{
    public string message;
    public float duration;
    public Toast(string msg, float dur)
    {
        message = msg;
        duration = dur;
    }
}
