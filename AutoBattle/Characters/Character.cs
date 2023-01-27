using System;
using System.Collections.Generic;
using AutoBattle.Abilities;
using AutoBattle.Effects;
using AutoBattle.Characters.Behaviours.AttackBehaviours;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using System.Threading;
using AutoBattle.GameManagement;
using AutoBattle.Grids;

namespace AutoBattle.Characters
{
    /// <summary>
    /// Base class for characters. They are a <c>GridObject</c>.
    /// </summary>
    public abstract class Character : GridObject
    {
        /// <summary>
        /// Indicates if the character is able to do an action.
        /// </summary>
        public bool CanAct { get; set; } = true;

        /// <summary>
        /// Indicates if the character is visible for others and can be targetable.
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// The average damage this character does.
        /// </summary>
        public int BaseDamage { get; set; }

        /// <summary>
        /// The team this character is inserted.
        /// </summary>
        public Team Team { get; set; }

        /// <summary>
        /// The actual target of the character.
        /// </summary>
        public Character Target { get; set; }

        /// <summary>
        /// The actual health of the character.
        /// </summary>
        public float Health { get; protected set; }

        /// <summary>
        /// Indicates if the character is dead.
        /// </summary>
        public bool IsDead { get; protected set; }

        /// <summary>
        /// The actual action that the character will execute or executed this turn.
        /// </summary>
        public Action TurnAction { get; protected set; }

        /// <summary>
        /// List of active effects on this character.
        /// </summary>
        public List<IEffect> Effects { get; protected set; } = new List<IEffect>();

        public ISpecialAbility SpecialAbility { get; protected set; }
        public IAttackBehaviour AttackBehaviour { get; protected set; }
        public IMoveBehaviour MoveBehaviour { get; protected set; }
        public ITargetFindBehaviour TargetFindBehaviour { get; protected set; }

        protected Character(string name) : base(name)
        {
        }

        /// <summary>
        /// Add a new effect to effect list on this character.
        /// </summary>
        /// <param name="effect">The effect that will be applied</param>
        public void AddEffect(IEffect effect) 
        {
            Effects.Add(effect);
        }

        public void FindTarget() 
        {
            TargetFindBehaviour?.FindTarget(this);
        }

        /// <summary>
        /// Do damage (or heal) this character. If health becomes 0, automatically kills the caracter.
        /// </summary>
        /// <param name="amount">The amount to be reduced from the character health. If negative, it will increase it</param>
        public void TakeDamage(float amount)
        {
            Health = Math.Clamp(Health -amount, 0, float.MaxValue);
            Console.WriteLine($"    - {Name} health is {Health}");

            if (Health <= 0)
                Die();
        }

        /// <summary>
        /// Kills the character
        /// </summary>
        public void Die()
        {
            IsDead = true;
            Health = 0;
            Console.WriteLine($" {Name} HAS DIED!!");
            GameEvents.onCharacterDeath?.Invoke(this);
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

        /// <summary>
        /// Do character's Turn... unless his dead.
        /// </summary>
        public void DoTurn() 
        {
            if (IsDead) 
            {
                Console.WriteLine($"\n {Name} is Dead X(");
                return;
            }

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
