using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Body Type Data", menuName = "Scriptable Objects/Employee/Body Type")]
public class BodyTypeDataSO : ScriptableObject
{
    [SerializeField] private BodyType _bodyType;
    [SerializeField] private int _minWeight;
    [SerializeField] private int _maxWeight;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Sprite _uiSprite;
    [SerializeField] private float _baseAppearanceChance;

    public BodyType BodyType => _bodyType;
    public int MinWeight => _minWeight;
    public int MaxWeight => _maxWeight;
    public Sprite Sprite => _sprite;
    public Sprite IconSprite => _uiSprite;
    public float BaseAppearanceChance => _baseAppearanceChance;
}
