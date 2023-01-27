using AutoBattle.Abilities;
using AutoBattle.Characters.Behaviours.AttackBehaviours;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using AutoBattle.GameManagement;
using System;

namespace AutoBattle.Characters
{
    internal class Archer : CharacterWithSpecial
    {
        public Archer(string name) : base(name)
        {
            SetCharacterBasis(100, 12, new InvisibilityAbility(), new MoveTowardsTarget(2), new SimpleAttackBehaviour(6,2), new FindClosestEnemyBehaviour());
        }

        protected override bool CanDoSpecial()
        {
            if (SpecialAbility == null) return false;

            int chance = new Random().Next(1, 101);
            return SpecialAbility.CanDoSpecial() && Health < 50 || chance < 30;
        }
    }
}
