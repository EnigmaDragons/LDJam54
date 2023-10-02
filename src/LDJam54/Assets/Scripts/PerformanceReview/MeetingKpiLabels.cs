using TMPro;
using UnityEngine;

public class MeetingKpiLabels : MonoBehaviour
{
    public TextMeshPro nameLabel;
    public TextMeshPro descriptionLabel;

    public void Set(string kpiName, string kpiDescription)
    {
        nameLabel.text = kpiName;
        descriptionLabel.text = kpiDescription;
    }
}
