using UnityEngine;
using System.Collections;
using System;

public interface IMasterGetter
{
    IEnumerator GetMaster(Action completed, GameObject parentErrorWindow);
}
