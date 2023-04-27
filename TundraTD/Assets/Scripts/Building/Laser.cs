using Spells;
using Spells.SpellClasses;
using UnityEngine;


namespace Building
{
    /// <summary>
    /// Laser kills Meteor and is being disabled with lightning
    /// </summary>
    public class Laser : EnemyTower
    {
        [SerializeField] private MeshRenderer laserMesh;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private float maxCooldownTime;
        [SerializeField] private float InteractionSize;
        
        private float _cooldownTime;

        protected override void HandleSpellCast(object sender, MagicSpell.SpellCastInfo e)
        {
            var spell = (MagicSpell)sender;
            if (spell.Element == BasicElement.Lightning && (e.HitInfo.point - transform.position).sqrMagnitude < InteractionSize * InteractionSize)
                _cooldownTime = maxCooldownTime;

            if (spell.Element == (BasicElement.Fire | BasicElement.Earth) && _cooldownTime <= 0)
            {
                (spell as MeteorSpell)?.Explode();
                _cooldownTime = maxCooldownTime;
            }
        }

        private void FixedUpdate()
        {
            if (_cooldownTime > 0)
                _cooldownTime -= Time.deltaTime;
        }
    }
}
