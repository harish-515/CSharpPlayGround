using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayGrond.DesignPatterns.Behavioral
{
    // a chain of componenns who all get a change to process 
    // a command or a query,optionally having default processing 
    // implementation and an ability to terminate the processing
    // chain


    #region " example 1 -- Typical CQS implementation "
    // Command : asking for an action or change
    // Query : asking for information
    // CQS(Command & Query Seperatation) : having seperate means of sending commands & queries

    public class GameCreature
    {
        public string Name;
        public int Attack, Defense;

        public GameCreature(string name,int atk,int def)
        {
            this.Name = name;
            this.Attack = atk;
            this.Defense = def;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name},{nameof(Attack)}: {Attack},{nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier
    {
        protected GameCreature creature;
        protected CreatureModifier next; //linked list
        public CreatureModifier(GameCreature creature)
        {
            this.creature = creature ?? throw new ArgumentNullException(paramName: nameof(creature));
        }

        public void Add(CreatureModifier cm)
        {
            if (next != null) next.Add(cm);
            else next = cm;
        }

        public virtual void Handle() => next?.Handle(); 
    }


    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(GameCreature creature) : base(creature)
        {
        }
        public override void Handle()
        {
            Console.WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreasedDefenceModifier : CreatureModifier
    {
        public IncreasedDefenceModifier(GameCreature creature) : base(creature)
        {
        }
        public override void Handle()
        {
            Console.WriteLine($"Increasing {creature.Name}'s defense");
            creature.Defense += 3;
            base.Handle();
        }
    }

    public class NoBonusModifier : CreatureModifier
    {
        public NoBonusModifier(GameCreature creature) : base(creature)
        {
        }
        public override void Handle()
        {
            // do nothing
        }
    }

    public static class CQSDemo
    {
        public static void Demo()
        {
            var goblin = new GameCreature("Goblin", 2, 2);
            Console.WriteLine(goblin);
            var root = new CreatureModifier(goblin);
            Console.WriteLine("adding attack modifier");
            root.Add(new DoubleAttackModifier(goblin));
            // adding a modifier no bonus
            root.Add(new NoBonusModifier(goblin));
            Console.WriteLine("increase defense modifier");
            root.Add(new IncreasedDefenceModifier(goblin));
            root.Handle();

            Console.WriteLine(goblin);
        }
    }

    #endregion


    #region " example 2 -- CQS with a Mediator"
    
    // Mediator Object 
    public class Game
    {
        public event EventHandler<Query> Queries;

        public void PerformQuery(object sender,Query q)
        {
            Queries?.Invoke(sender, q); 
        }
    }

    public class GameCreature1
    {
        public Game game;
        private int attack, defense;     
        public string Name;

        public GameCreature1(Game game,string name,int attack,int defense)
        {
            this.game = game;
            this.Name = name;
            this.attack = attack;
            this.defense = defense;
        }

        public int Attack
        {
            get
            {
                var q = new Query(Name, Query.Argument.Attack, attack);
                game.PerformQuery(this, q);
                return q.Value;
            }
        }

        public int Defense
        {
            get
            {
                var q = new Query(Name, Query.Argument.Defense, defense);
                game.PerformQuery(this, q);
                return q.Value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name},{nameof(Attack)}: {Attack},{nameof(Defense)}: {Defense}";
        }
    }

    public abstract class CreatureModifier1 : IDisposable
    {
        protected Game game;
        protected GameCreature1 creature;

        protected CreatureModifier1(Game game, GameCreature1 creature)
        {
            this.game = game;
            this.creature = creature;
            game.Queries += Handle;
        }


        protected abstract void Handle(object sender, Query q);


        public void Dispose()
        {
            game.Queries -= Handle;
        }
    }

    public class DoubleAttackModifier1 : CreatureModifier1
    {
        public DoubleAttackModifier1(Game game, GameCreature1 creature) : base(game, creature)
        {
        }

        protected override void Handle(object sender, Query q)
        {
            if((q.CreatureName == creature.Name) 
                && q.WhatToQuery == Query.Argument.Attack){
                q.Value *= 2;
            }

        }
    }

    public class IncreaseDefenseModifier1 : CreatureModifier1
    {
        public IncreaseDefenseModifier1(Game game, GameCreature1 creature) : base(game, creature)
        {
        }

        protected override void Handle(object sender, Query q)
        {
            if ((q.CreatureName == creature.Name)
                && q.WhatToQuery == Query.Argument.Defense)
            {
                q.Value += 3;
            }

        }
    }


    public class Query
    {
        public string CreatureName;
        public enum Argument
        {
            Attack,
            Defense
        }

        public Argument WhatToQuery;
        public int Value;

        public Query(string name,Argument whatToQuery,int value)
        {
            CreatureName = name;
            WhatToQuery = whatToQuery;
            Value = value;
        }

    }

    public static class CQSEnhancedDemo
    {
        public static void Demo()
        {
            var game = new Game();
            var goblin = new GameCreature1(game, "Strong Goblin", 3, 3);
            Console.WriteLine(goblin);

            using(new DoubleAttackModifier1(game, goblin))
            {
                Console.WriteLine(goblin); 
            }

            Console.WriteLine(goblin);
        }
    }



    #endregion


  namespace Coding.Exercise
    {
        public enum StatType
        {
            Attack,
            Defense
        }

        public class Query
        {
            public StatType stattype;
            public int Value;
        }

        public abstract class Creature
        {
            protected Game game;
            protected readonly int baseAttack;
            protected readonly int baseDefense;

            protected Creature(Game game,int attack,int defense)
            {
                this.game = game;
                this.baseAttack = attack;
                this.baseDefense = defense;
            } 

            public virtual int Attack { get; }
            public virtual int Defense { get; }
            public abstract void PerformQuery(object sender, Query  q);

        }

        public class Goblin : Creature
        {
            public Goblin(Game game) : base(game, 1, 1)
            {
            }

            public Goblin(Game game,int attack,int defense) : base(game, attack, defense)
            {
            }
            public override int Defense
            {
                get
                {
                    var q = new Query { stattype = StatType.Defense };
                    foreach (var c in game.Creatures)
                        c.PerformQuery(this, q);
                    return q.Value;
                }
            }

            public override int Attack
            {
                get
                {
                    var q = new Query { stattype = StatType.Attack };
                    foreach (var c in game.Creatures)
                        c.PerformQuery(this, q);
                    return q.Value;
                }
            }
            public override void PerformQuery(object sender, Query q)
            {
                if (ReferenceEquals(this, sender))
                {
                    switch (q.stattype)
                    {
                        case StatType.Attack:
                            q.Value += baseAttack;
                            break;
                        case StatType.Defense:
                            q.Value += baseDefense;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    if (q.stattype == StatType.Defense)
                    {
                        q.Value++;
                    }
                }
            }

        }

        public class GoblinKing : Goblin
        {
            public GoblinKing(Game game) : base(game, 3, 3)
            {

            }
            public override int Attack => base.Attack;

            public override int Defense => base.Defense;

            public override void PerformQuery(object sender, Query q)
            {
                if (!ReferenceEquals(sender, this) && q.stattype == StatType.Attack)
                {
                    q.Value++; // every goblin gets +1 attack
                }
                else base.PerformQuery(sender, q);
            }
        }

        public class Game
        {
            public IList<Creature> Creatures;
        }
    }

}
