﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsDelete : MonoBehaviour
{

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
    }
}
