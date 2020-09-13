using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NameChar{   
    name_chick,
    name_boto,
    name_boi,
    name_curupira,
    name_iara,
    name_saci,
    name_vitoria,
    name_cuca,
    name_bicho_papao,
    name_boiuna,
    name_lobisomem
}

public enum NameFont{   
    font_chick,
    font_boto,
    font_boi,
    font_curupira,
    font_iara,
    font_saci,
    font_vitoria,
    font_cuca,
    font_bicho_papao,
    font_boiuna,
    font_lobisomem
}

public class GalleryManager : MonoBehaviour
{
    public Button[]            btnChosenProfile;

    [Header("Tab")]
    public Image[]             imgProfile;
    public Image[]             imgProfileCheck;

    [Header("Board")]
    public Text                txtNameChar;
    public Text                txtDescriptionOnBoard;
    public Text                txtFontOnBoard;

    [Header("Description")]
    public string[]            charName;

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

        txtNameChar.text = charName[pos];
        txtDescriptionOnBoard.text = LocalizationManager.Instance.GetLocalizedValueGallery(((NameChar)pos).ToString());
        txtFontOnBoard.text = LocalizationManager.Instance.GetLocalizedValueGallery(((NameFont)pos).ToString());;
    }
}
