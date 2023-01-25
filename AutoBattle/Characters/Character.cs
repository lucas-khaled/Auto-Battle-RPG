using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;
using AutoBattle.Abilities;
using AutoBattle.Effects;
using AutoBattle.Characters.Behaviours.AttackBehaviours;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;

namespace AutoBattle.Characters
{
    public abstract class Character : GridObject
    {
        public bool CanAct { get; set; } = true;
        public int BaseDamage { get; set; }
        public bool Visible { get; set; }

        public Character Target { get; set; }

        public float Health { get; protected set; }
        public bool IsDead { get; protected set; }

        public Action TurnAction { get; protected set; }

        public List<IEffect> Effects { get; protected set; } = new List<IEffect>();

        protected ISpecialAbility specialAbility;
        protected IAttackBehaviour attackBehaviour;
        protected IMoveBehaviour moveBehaviour;
        protected ITargetFindBehaviour targetFindBehaviour;

        protected Character(string name) : base(name)
        {
        }

        public void AddEffect(IEffect effect) 
        {
            Effects.Add(effect);
        }

        public void FindTarget() 
        {
            targetFindBehaviour?.FindTarget(this);
        }

        public bool TakeDamage(float amount)
        {
            Health = Math.Clamp(Health -amount, 0, float.MaxValue);
            Console.WriteLine($"    - {Name} took damage. Health is {Health}");

            if (Health <= 0)
            {
                Die();
                return true;
            }
            return false;
        }

        public void Die()
        {
            IsDead = true;
            Console.WriteLine($"{Name} HAS DIED!!");
            //TODO >> maybe kill him?
        }

        public virtual void Move() 
        {
            moveBehaviour?.Move(this);
            Console.WriteLine($"{Name} moved to {currentBox.ToString()}");
        }

        public virtual void Attack() 
        {
            attackBehaviour?.Attack(this);
        }

        public virtual void DoSpecial() 
        {
            specialAbility?.DoSpecial(this);
        }

        public void DoTurn() 
        {
            if (IsDead) return;

            HandleEffects();

            if (CanAct is false) return;

            FindTarget();
            ChooseAction();
            DoAction();

            Console.WriteLine($"{Name} ended Turn.\n");
        }

        public abstract void ChooseAction();

        public abstract void DoAction();

        protected void SetCharacterBasis(float health, int baseDamage, ISpecialAbility specialAbility, 
            IMoveBehaviour moveBehaviour, IAttackBehaviour attackBehaviour, ITargetFindBehaviour targetFindBehaviour)
        {
            Health = health;
            BaseDamage = baseDamage;

            this.specialAbility = specialAbility;
            this.attackBehaviour = attackBehaviour;
            this.moveBehaviour = moveBehaviour;
            this.targetFindBehaviour = targetFindBehaviour;

            IsDead = false;
        }

        private void HandleEffects()
        {
            if (Effects.Count > 0)
                Effects.ForEach(x => HandleEffect(x));

            Effects.RemoveAll(x => x.Passed());
        }

        private void HandleEffect(IEffect effect)
        {
            if(effect.Passed())
            {
                effect.ResetEffect(this);
                return;
            }

            effect.ApplyEffect(this);
        }
    }
}
