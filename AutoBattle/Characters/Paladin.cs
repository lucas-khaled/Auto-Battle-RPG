﻿using AutoBattle.Abilities;
using AutoBattle.Characters.Behaviours.AttackBehaviours;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using System;

namespace AutoBattle.Characters
{
    internal class Paladin : CharacterWithSpecial
    {
        public Paladin(string name) : base(name)
        {
            SetCharacterBasis(health: 130, baseDamage: 35, new KnockDownAbility(), new MoveTowardsTarget(1), new RowAttackBehaviour(2,1, 3), new FindClosestEnemyBehaviour());
        }

        protected override bool CanDoSpecial() 
        {
            if (SpecialAbility == null) return false;

            int chance = new Random().Next(1, 101);
            return SpecialAbility.CanDoSpecial() && chance < 40;
        }
    }
}
