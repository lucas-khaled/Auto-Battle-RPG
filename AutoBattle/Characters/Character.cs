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
using System.Threading;
using AutoBattle.GameManagement;

namespace AutoBattle.Characters
{
    public abstract class Character : GridObject
    {
        public bool CanAct { get; set; } = true;
        public bool Visible { get; set; } = true;
        public int BaseDamage { get; set; }

        public Team Team { get; set; }
        public Character Target { get; set; }

        public float Health { get; protected set; }
        public bool IsDead { get; protected set; }

        public Action TurnAction { get; protected set; }

        public List<IEffect> Effects { get; protected set; } = new List<IEffect>();

        public ISpecialAbility SpecialAbility { get; protected set; }
        public IAttackBehaviour AttackBehaviour { get; protected set; }
        public IMoveBehaviour MoveBehaviour { get; protected set; }
        public ITargetFindBehaviour TargetFindBehaviour { get; protected set; }

        protected Character(string name) : base(name)
        {
        }

        public void AddEffect(IEffect effect) 
        {
            Effects.Add(effect);
        }

        public void FindTarget() 
        {
            TargetFindBehaviour?.FindTarget(this);
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
            Console.WriteLine($" {Name} HAS DIED!!");
        }

        public virtual void Move() 
        {
            MoveBehaviour?.Move(this);
        }

        public virtual void Attack() 
        {
            AttackBehaviour?.Attack(this);
        }

        public virtual void DoSpecial() 
        {
            SpecialAbility?.DoSpecial(this);
        }

        public void DoTurn() 
        {
            Console.WriteLine($"\n {Name}'s turn");
            HandleEffects();
            Thread.Sleep(500);

            if (CanAct && IsDead is false)
            {
                FindTarget();
                ChooseAction();
                DoAction();
            }

            Thread.Sleep(500);
            EndTurn();
        }

        public abstract void ChooseAction();

        public abstract void DoAction();

        protected void SetCharacterBasis(float health, int baseDamage, ISpecialAbility specialAbility, 
            IMoveBehaviour moveBehaviour, IAttackBehaviour attackBehaviour, ITargetFindBehaviour targetFindBehaviour)
        {
            Health = health;
            BaseDamage = baseDamage;

            this.SpecialAbility = specialAbility;
            this.AttackBehaviour = attackBehaviour;
            this.MoveBehaviour = moveBehaviour;
            this.TargetFindBehaviour = targetFindBehaviour;

            IsDead = false;
        }

        private void HandleEffects()
        {
            if (Effects.Count < 0)
                return;

            Effects.ForEach(effect => HandleEffect(effect));
            Effects.RemoveAll(effect => effect.Passed());
            Console.Write(Environment.NewLine);
        }

        private void HandleEffect(IEffect effect) 
        {
            if (IsDead) return;

            effect.ApplyEffect(this);
        }

        private void EndTurn() 
        {
            Console.WriteLine($"\n {Name} ended his turn");
            Console.WriteLine(" -------------------\n");

            Thread.Sleep(1000);
        }
    }
}
