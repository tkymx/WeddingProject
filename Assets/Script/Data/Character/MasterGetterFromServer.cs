using UnityEngine;
using System.Collections;
using System;

public class MasterGetterFromServer : IMasterGetter
{

    string storySheedId = "1eUWoL7j4vm_GgBFSmIz4OVmdi0_2phmX4CPfL1s9qVc";
    string storySheetName = "シート1";

    string systemSheedId = "1b2noKWwvNkMXhN0w6mQZHoLimn-H8KXsHTief0g1py8";
    string systemSheetName = "シート1";

    public IEnumerator GetMaster(Action completed, GameObject parentErrorWindow)
    {
        yield return SpreadSheetGetter.GetSheetAsync(
            storySheedId,
            storySheetName,
            PlayerCharacterRepository.InitializeFromArray,
            parentErrorWindow
        );

        yield return SpreadSheetGetter.GetSheetAsync(
            systemSheedId,
            systemSheetName,
            SystemRepository.InitializeFromArray,
            parentErrorWindow
        );

        completed();

        yield return null;
    }
}
