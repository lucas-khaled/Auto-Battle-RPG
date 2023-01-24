using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;
using AutoBattle.Abilities;
using AutoBattle.Effects;
using AutoBattle.Characters.Behaviours.AttackBehaviours;
using AutoBattle.Characters.Behaviours.MoveBehaviours;

namespace AutoBattle.Characters
{
    public abstract class Character
    {
        public bool CanAct { get; set; }
        public int BaseDamage { get; set; }
        public bool Visible { get; set; }


        public string Name { get; protected set; }
        public float Health { get; protected set; }
        public bool IsDead { get; protected set; }

        public Action TurnAction { get; protected set; }
        public Character Target { get; protected set; }
        public List<IEffect> Effects { get; protected set; }

        protected ISpecialAbility specialAbility;
        protected IAttackBehaviour attackBehaviour;
        protected IMoveBehaviour moveBehaviour;

        protected GridBox CurrentBox;

        public void AddEffect(IEffect effect) 
        {
            Effects.Add(effect);
        }

        public void ApplyEffects() 
        {
            Effects.ForEach(x => ApplyEffect(x));
        }

        public bool TakeDamage(float amount)
        {
            Health -= amount;
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
            //TODO >> maybe kill him?
        }

        public void Move() 
        {
            moveBehaviour.Move(this);
        }

        public void Attack() 
        {
            attackBehaviour.Attack(this);
        }

        public void DoTurn() 
        {
            ApplyEffects();

            if (CanAct is false) return;

            ChooseAction();
            DoAction();
        }

        public void SetCurrentPlace(GridBox box) 
        {
            CurrentBox.ocupied = false;
            box.ocupied = true;
            CurrentBox = box;
        }

        public abstract void ChooseAction();

        public abstract void DoAction();

        protected void SetCharacterBasis(string name, float health, int baseDamage, ISpecialAbility specialAbility, IMoveBehaviour moveBehaviour, IAttackBehaviour attackBehaviour)
        {
            Name = name;
            Health = health;
            BaseDamage = baseDamage;

            this.specialAbility = specialAbility;
            this.attackBehaviour = attackBehaviour;
            this.moveBehaviour = moveBehaviour;

            IsDead = false;
        }

        private void ApplyEffect(IEffect effect)
        {
            if(effect.ApplyEffect(this) is false) 
                Effects.Remove(effect);
        }


        /*public void StartTurn(Grid battlefield)
        {

            if (CheckCloseTargets(battlefield))
            {
                Attack(Target);


                return;
            }
            else
            {   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if (currentBox.xIndex > Target.currentBox.xIndex)
                {
                    if (battlefield.grids.Exists(x => x.Index == currentBox.Index - 1))
                    {
                        currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        currentBox = battlefield.grids.Find(x => x.Index == currentBox.Index - 1);
                        currentBox.ocupied = true;
                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {Index} walked left\n");
                        battlefield.DrawBattlefield(5, 5);

                        return;
                    }
                }
                else if (currentBox.xIndex < Target.currentBox.xIndex)
                {
                    currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    currentBox = battlefield.grids.Find(x => x.Index == currentBox.Index + 1);
                    currentBox.ocupied = true;
                    return;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {Index} walked right\n");
                    battlefield.DrawBattlefield(5, 5);
                }

                if (currentBox.yIndex > Target.currentBox.yIndex)
                {
                    battlefield.DrawBattlefield(5, 5);
                    currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    currentBox = battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght);
                    currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {Index} walked up\n");
                    return;
                }
                else if (currentBox.yIndex < Target.currentBox.yIndex)
                {
                    currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = currentBox;
                    currentBox = battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght);
                    currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {Index} walked down\n");
                    battlefield.DrawBattlefield(5, 5);

                    return;
                }
            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield)
        {
            bool left = battlefield.grids.Find(x => x.Index == currentBox.Index - 1).ocupied;
            bool right = battlefield.grids.Find(x => x.Index == currentBox.Index + 1).ocupied;
            bool up = battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght).ocupied;
            bool down = battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght).ocupied;

            if (left & right & up & down)
            {
                return true;
            }
            return false;
        }

        public void Attack(Character target)
        {
            var rand = new Random();
            target.TakeDamage(rand.Next(0, (int)_baseDamage));
            Console.WriteLine($"Player {Index} is attacking the player {Target.Index} and did {_baseDamage} damage\n");
        }*/
    }
}
