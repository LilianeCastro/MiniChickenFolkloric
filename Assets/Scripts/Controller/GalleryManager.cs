using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    public Button[]            btnChosenProfile;

    [Header("Tab")]
    public Image[]             imgProfile;
    public Image[]             imgProfileCheck;
    public Image[]             imgProfileBody;

    [Header("Board")]
    public Image               imgBodyFull;
    public Text                txtNameChar;
    public Text                txtDescriptionOnBoard;

    [Header("Description")]
    public string[]            charName;
    public string[]            txtDescription;

    

    public void updateSkin(int pos)
    {
        for(int i = 0; i < imgProfile.Length; i ++)
        {
            btnChosenProfile[i].image.sprite = imgProfile[i].sprite;
        }

        changeSkinToActiveInGallery(pos);
    }

    private void changeSkinToActiveInGallery(int pos)
    {
        btnChosenProfile[pos].image.sprite = imgProfileCheck[pos].sprite;
        imgBodyFull.sprite = imgProfileBody[pos].sprite;

        txtNameChar.text = charName[pos];
        txtDescriptionOnBoard.text = txtDescription[pos];
    }
}
