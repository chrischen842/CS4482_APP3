using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkinManager : MonoBehaviour
{
    public SpriteRenderer _SpriteRenderer;
    public List<Sprite> skins = new List<Sprite>();
    private int selectedSkin = 0;
    public GameObject playerSkin;
    public List<RuntimeAnimatorController> animators;

    public void NextSkin()
    {
        selectedSkin = selectedSkin + 1;
        if (selectedSkin == skins.Count)
        {
            selectedSkin = 0;
        }

        _SpriteRenderer.sprite = skins[selectedSkin];
        UpdateAnimatorController();
    }

    public void LastSkin()
    {
        selectedSkin = selectedSkin - 1;
        if (selectedSkin < 0)
        {
            selectedSkin = skins.Count - 1;
        }

        _SpriteRenderer.sprite = skins[selectedSkin];
        UpdateAnimatorController();
    }

    private void UpdateAnimatorController()
    {
        var playerAnimator = playerSkin.GetComponent<Animator>();
        if (playerAnimator != null)
        {
            playerAnimator.runtimeAnimatorController = animators[selectedSkin];
        }
    }

    public void PlayGame()
    {
        PrefabUtility.SaveAsPrefabAsset(playerSkin, "Assets/SelectedCharacter.prefab");
    }
}
