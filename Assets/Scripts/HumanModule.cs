/*  Filename:           HumanModule.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        February 25, 2023
 *  Description:        Script that refactored from the test script of the human module assets
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 */

using System.Collections.Generic;
using UnityEngine;

public class HumanModule : MonoBehaviour
{
    [System.Serializable]
    public class HumanModuleData
    {
        public GameObject part;
        public SkinnedMeshRenderer renderer;
        public List<Texture> textures;
    }

    [Header("Body Parts - Skin")]
    [SerializeField] private HumanModuleData head;
    [SerializeField] private HumanModuleData body;

    [Header("Body Parts - Cloth - Common")]
    [SerializeField] private HumanModuleData glasses;
    [SerializeField] private List<HumanModuleData> beards;

    [Header("Body Parts - Cloth - Suits")]
    [SerializeField] private List<HumanModuleData> suits;

    [Header("Body Parts - Cloth - Others")]
    [SerializeField] private List<HumanModuleData> caps;
    [SerializeField] private List<HumanModuleData> chains;
    [SerializeField] private HumanModuleData jacket;
    [SerializeField] private HumanModuleData pullover;
    [SerializeField] private HumanModuleData scarf;
    [SerializeField] private List<HumanModuleData> shoes;
    [SerializeField] private HumanModuleData shortPants;
    [SerializeField] private HumanModuleData tShirt;
    [SerializeField] private HumanModuleData tankTop;
    [SerializeField] private HumanModuleData trousers;

    [Header("Body Parts - Hair")]
    [SerializeField] private List<HumanModuleData> hairs;

    [Header("Body Parts - Props")]
    [SerializeField] private List<HumanModuleData> props;

    public void RandomAppearance()
    {
        // Skin
        if (head.textures.Count > 0)
        {
            Texture texture = head.textures[UnityEngine.Random.Range(0, head.textures.Count)];
            head.renderer.materials[0].mainTexture = texture;
            body.renderer.materials[0].mainTexture = texture;
        }

        // Glasses
        bool wearGlass = Random.value > 0.5f;
        SetBodyPart(glasses, wearGlass);

        // Beard
        bool hasBeard = Random.value > 0.5f;
        SetBodyPartByList(beards, hasBeard);

        // Suit
        bool wearSuit = Random.value > 0.5f;
        SetBodyPartByList(suits, wearSuit);

        // caps
        bool wearCap = !wearSuit && Random.value > 0.5f;
        SetBodyPartByList(caps, !wearCap);

        // chains
        bool wearChain = !wearSuit && Random.value > 0.5f;
        SetBodyPartByList(chains, !wearChain);

        // jacket, pullover, t-shirt or tank top
        int random = Random.Range(0, 4);
        SetBodyPart(jacket, !wearSuit && random == 0);
        SetBodyPart(pullover, !wearSuit && random == 1);
        SetBodyPart(tShirt, !wearSuit && random == 2);
        SetBodyPart(tankTop, !wearSuit && random == 3);

        // scarf
        bool wearScarf = !wearSuit && Random.value > 0.5f;
        SetBodyPart(scarf, wearScarf);

        // shoes
        SetBodyPartByList(shoes, !wearSuit);

        // short pants or trousers
        bool wearTrousers = Random.value > 0.5f;
        SetBodyPart(shortPants, !wearSuit && !wearTrousers);
        SetBodyPart(trousers, !wearSuit && wearTrousers);

        // hair
        bool hasHair = Random.value > 0.8f;
        SetBodyPartByList(hairs, hasHair);

        // props
        bool hasProps = Random.value > 0.8f;
        SetBodyPartByList(props, hasProps);
    }

    private void SetBodyPart(HumanModuleData data, bool active)
    {
        data.part.SetActive(active);
        if (active && data.textures.Count > 0)
        {
            data.renderer.materials[0].mainTexture = data.textures[UnityEngine.Random.Range(0, data.textures.Count)];
        }
    }

    private void SetBodyPartByList(List<HumanModuleData> dataList, bool active)
    {
        int index = UnityEngine.Random.Range(0, dataList.Count);
        if (active)
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                SetBodyPart(dataList[i], index == i);
            }
        }
        else
        {
            foreach (HumanModuleData data in dataList)
            {
                data.part.SetActive(false);
            }
        }
    }

}
