using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeWeightChart : MonoBehaviour
{
    [SerializeField] private List<BodyTypeDataSO> _employeeTypes;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < _employeeTypes.Count; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out EmployeeWeightIcon employeeWeightIcon))
            {
                employeeWeightIcon.SetData(_employeeTypes[i]);
            }
        }
    }
}
