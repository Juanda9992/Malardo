using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagGenerator : MonoBehaviour
{
    public static TagGenerator instance;
    [SerializeField] private TagBehaviour[] tagBehaviour;
    [SerializeField] private GameObject tagPrefab;
    [SerializeField] private Transform tagParent;

    [SerializeField] private List<TagBehaviour> currentTags = new List<TagBehaviour>();
    [SerializeField] private TagData doubleTag;
    [SerializeField] private TagData testingTag;
    public TagData lastTag;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentTags = new List<TagBehaviour>();
    }
    public void CreateTag(TagData tagData)
    {
        GameObject go = Instantiate(tagPrefab, tagParent);

        if (tagData.useHandType)
        {
            go.GetComponent<TagBehaviour>().SetTagData(tagData, tagBehaviour[BlindManager.instance.currentBlindProgress].handType);
        }
        else
        {
            go.GetComponent<TagBehaviour>().SetTagData(tagData);
        }

        lastTag = tagData;

        if (TagData.ReferenceEquals(lastTag, doubleTag))
        {
            lastTag = null;
        }
        currentTags.Add(go.GetComponent<TagBehaviour>());
        go.transform.SetAsFirstSibling();
    }

    public TagData GetRoundTag(int round)
    {
        return tagBehaviour[round].GetCurrentTag();
    }
    [ContextMenu("Generate Tags")]
    public void GenerateRoundTags()
    {
        for (int i = 0; i < tagBehaviour.Length; i++)
        {
            tagBehaviour[i].SetTagData(DatabaseManager.instance.tagDatabase.GetRandomTag());
        }
    }

    public void GenerateLastTag()
    {
        if (lastTag != null)
        {
            GameObject go = Instantiate(tagPrefab, tagParent);
            go.GetComponent<TagBehaviour>().SetTagData(lastTag);

            currentTags.Add(go.GetComponent<TagBehaviour>());
            go.transform.SetAsFirstSibling();
        }
    }

    public IEnumerator ConsumeTags(TagExchangeMoment tagExchangeMoment)
    {
        for (int i = 0; i < currentTags.Count; i++)
        {
            if (currentTags[i].GetCurrentTag().tagExchangeMoment == tagExchangeMoment)
            {
                if (TagData.ReferenceEquals(currentTags[i].GetCurrentTag(), doubleTag) && lastTag == null)
                {
                    break;
                }
                yield return new WaitForSeconds(0.3f);
                currentTags[i].ApplyEffect();
                yield return new WaitUntil(() => currentTags[i].GetCurrentTag().tagEffect.EffectReady());
                Destroy(currentTags[i].gameObject);
                currentTags[i] = null;

            }
            else
            {
                break;
            }
        }

        currentTags.RemoveAll(x => x == null);
    }

    [ContextMenu("Create Testing Tag")]
    private void CreateTestingTag()
    {
        CreateTag(testingTag);
    }
}
