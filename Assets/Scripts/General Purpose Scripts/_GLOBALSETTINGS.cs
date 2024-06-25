using UnityEngine;

/// <summary>
/// stores some essential information such as font size, accessibility options, and such
/// also the place to call functions that will update said info
/// written by Andy (@rmfz)
/// </summary>
public class _GLOBALSETTINGS : MonoBehaviour
{
    public static int fontSize;

    public static int languageMode;
    //0 = regular, patient (adults & patient)
    //1 = skibidi speak (gen-z lingo & impatient asf)

    public static int deviceMode;
    //force the app to display crap in the style of a certain device
    //0 - phone without notches (i.e iphone SE, iphone 6, iphone 7, iphone 8, etc)
    //1 - phone with notch (j.e iphone X+)
    //2 - tablet, landscape (who tf uses portrait)

    public static bool displayHasNotch;
    //is the app running on an iPhone that has a notch? aka is it an iPhone X or above?

}
