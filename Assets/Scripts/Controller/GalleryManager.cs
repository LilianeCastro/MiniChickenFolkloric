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
    [Header("Buttons")]
    public Color               colorSelected;
    public Text                btnText;
    public Button              btnSelectCharacter;
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

    private int                currentPos;

    public void StartGallery()
    {
        currentPos = 0;
        UpdateSkin(currentPos);

        if(GameManager.Instance.GetSkinID() == -1)
        {
            btnText.color = colorSelected;
            btnText.text = LocalizationManager.Instance.GetLocalizedValue("txt_selected");
        }
        else
        {
            btnText.color = Color.white;
            btnText.text = LocalizationManager.Instance.GetLocalizedValue("txt_radom");
        }
        
        
    }

    public void SelectCharacter()
    {
        if(currentPos >= 0 && currentPos < 7)
        {
            btnText.text = LocalizationManager.Instance.GetLocalizedValue("txt_selected");

            if(currentPos == 0)
            {
                GameManager.Instance.UpdateSkinID(-1);
            }
            else
            {
                GameManager.Instance.UpdateSkinID(currentPos-1); //os personagens são selecionados de acordo com a posição do layer no animator
            }
            btnText.color = colorSelected;
            
        }
    }

    public void UpdateSkin(int pos)
    {
        currentPos = pos;
        btnSelectCharacter.gameObject.SetActive(true);

        if(pos > 6)
        {
            btnSelectCharacter.gameObject.SetActive(false);
        }
        else if(pos==0)
        {
            btnText.text = LocalizationManager.Instance.GetLocalizedValue("txt_radom");
            btnText.color = Color.white;
        }
        else
        {
            btnText.text = LocalizationManager.Instance.GetLocalizedValue("txt_select");
            btnText.color = Color.white;
        }

        for(int i = 0; i < imgProfile.Length; i ++)
        {
            btnChosenProfile[i].image.sprite = imgProfile[i].sprite;
        }

        if(pos < 7 && GameManager.Instance.GetSkinID() + 1 == pos)
        {
            btnText.text = LocalizationManager.Instance.GetLocalizedValue("txt_selected");
            btnText.color = colorSelected;
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
