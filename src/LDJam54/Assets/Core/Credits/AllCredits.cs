using UnityEngine;

[CreateAssetMenu(menuName = "Credits/All Credits")]
public class AllCredits : ScriptableObject
{
    [SerializeField] private RoleCredit[] credits;

    public RoleCredit[] Credits => credits;
}
