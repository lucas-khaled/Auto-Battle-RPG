using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;
using AutoBattle.Abilities;
using AutoBattle.Effects;

namespace AutoBattle.Characters
{
    public abstract class Character
    {
        public string Name { get; protected set; }
        public float Health { get; protected set; }
        public float BaseDamage { get; protected set; }
        public bool IsDead { get; protected set; }

        public Action TurnAction { get; protected set; }
        public GridBox CurrentBox { get; protected set; }
        public Character Target { get; protected set; }
        public ISpecialAbility SpecialAbility { get; protected set; }
        public List<IEffect> Effects { get; protected set; }


        public Character(string name, float health, float baseDamage, ISpecialAbility specialAbility)
        {
            Name = name;
            Health = health;
            BaseDamage = baseDamage;
            SpecialAbility = specialAbility;
            IsDead = false;
        }

        public void AddEffect(IEffect effect) 
        {
            Effects.Add(effect);
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

        public abstract void Move();

        public abstract void Attack();

        public abstract void ChooseAction();

        public abstract void DoAction();


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
