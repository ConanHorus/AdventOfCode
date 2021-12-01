using AdventOfCode.Returns;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_2015_22.
  /// </summary>
  public class Runner_2015_22
    : RunnerBase
  {
    /// <summary>
    /// Active effects.
    /// </summary>
    private static readonly Dictionary<string, Effect> activeEffects = new Dictionary<string, Effect>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_22"/> class.
    /// </summary>
    public Runner_2015_22()
      : base(2015, 22)
    {
    }

    /// <inheritdoc/>
    public override (string? part1, string? part2) Run(string inputString, string[] inputLines)
    {
      var boss = GenerateBoss(inputLines);
      var player = new Player();

      ulong part1 = FindSmallestManaUsage(player, boss);

      return (part1.ToString(), null);
    }

    /// <summary>
    /// Finds the smallest mana usage.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="boss">The boss.</param>
    /// <returns>An int.</returns>
    private static ulong FindSmallestManaUsage(Player player, Boss boss)
    {
      ulong smallestManaUsage = ulong.MaxValue;
      int fightNumber = 0;
      do
      {
        if (fightNumber % 1_000_000 == 0)
        {
          Console.WriteLine(smallestManaUsage);
        }

        fightNumber++;
        var fightReturn = RunFight(player, boss);
        if (fightReturn.Successful)
        {
          smallestManaUsage = Math.Min(smallestManaUsage, fightReturn.Value);
          continue;
        }

        Exception exception = fightReturn.Exception!;
        if (exception is InvalidPatternException)
        {
          continue;
        }
      }
      while (fightNumber < int.MaxValue);

      return smallestManaUsage;
    }

    /// <summary>
    /// Runs the fight.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="boss">The boss.</param>
    /// <returns>A Return.</returns>
    private static Return<ulong> RunFight(Player player, Boss boss)
    {
      player.Reset();
      boss.Reset();
      activeEffects.Clear();

      while (true)
      {
        UpdateEffects();
        var attackReturn = player.Attack(boss);
        if (!attackReturn)
        {
          return attackReturn.Exception!;
        }

        if (boss.Health <= 0)
        {
          break;
        }

        UpdateEffects();
        attackReturn = boss.Attack(player);
        if (!attackReturn)
        {
          return attackReturn.Exception!;
        }

        if (player.Health <= 0)
        {
          return new InvalidPatternException();
        }
      }

      return Return.Success(player.ManaUsed);
    }

    /// <summary>
    /// Updates the effects.
    /// </summary>
    private static void UpdateEffects()
    {
      var delete = new List<string>();

      foreach (var effect in activeEffects.Values)
      {
        effect.Update();
        effect.Timer++;
        if (effect.Timer <= 0)
        {
          effect.End();
          delete.Add(effect.Name);
        }
      }

      foreach (string name in delete)
      {
        activeEffects.Remove(name);
      }
    }

    /// <summary>
    /// Gets the effect.
    /// </summary>
    /// <param name="patternValue">The pattern value.</param>
    /// <param name="player">The player.</param>
    /// <param name="boss">The boss.</param>
    /// <returns>An Effect.</returns>
    private static Effect GetEffect(int patternValue, Player player, Boss boss)
    {
      return patternValue switch
      {
        0 => new Effect_Drain(player, boss),
        1 => new Effect_MagicMissile(player, boss),
        2 => new Effect_Poison(player, boss),
        3 => new Effect_Recharge(player, boss),
        4 => new Effect_Sheild(player, boss),
        _ => throw new NotImplementedException()
      };
    }

    /// <summary>
    /// Adds the effect.
    /// </summary>
    /// <param name="effect">The effect.</param>
    /// <param name="player">The player.</param>
    /// <param name="boss">The boss.</param>
    /// <returns>A bool.</returns>
    private static bool AddEffect(Effect effect, Player player, Boss boss)
    {
      if (activeEffects.ContainsKey(effect.Name))
      {
        return false;
      }

      activeEffects[effect.Name] = effect;
      return true;
    }

    /// <summary>
    /// Generates the boss.
    /// </summary>
    /// <param name="inputLines">The input lines.</param>
    /// <returns>A boss.</returns>
    private Boss GenerateBoss(string[] inputLines)
    {
      int health = 0;
      int damage = 0;

      foreach (string line in inputLines)
      {
        string[] parts = line.Split(": ");
        if (parts[0] == "Hit Points")
        {
          health = int.Parse(parts[1]);
        }

        if (parts[0] == "Damage")
        {
          damage = int.Parse(parts[1]);
        }
      }

      return new Boss(health, damage);
    }

    /// <summary>
    /// The pattern too short exception.
    /// </summary>
    private class PatternTooShortException
      : Exception
    {
    }

    /// <summary>
    /// The invalid pattern exception.
    /// </summary>
    private class InvalidPatternException
      : Exception
    {
    }

    /// <summary>
    /// The effect.
    /// </summary>
    private abstract class Effect
    {
      /// <summary>
      /// Self.
      /// </summary>
      protected readonly Character self;

      /// <summary>
      /// Other.
      /// </summary>
      protected readonly Character other;

      /// <summary>
      /// Gets the cost.
      /// </summary>
      public abstract int Cost { get; }

      /// <summary>
      /// Gets the timer.
      /// </summary>
      public abstract int Timer { get; set; }

      /// <summary>
      /// Gets the name.
      /// </summary>
      public abstract string Name { get; }

      /// <summary>
      /// Initializes a new instance of the <see cref="Effect"/> class.
      /// </summary>
      /// <param name="self">The self.</param>
      /// <param name="other">The other.</param>
      public Effect(Character self, Character other) => (this.self, this.other) = (self, other);

      /// <summary>
      /// Starts the effect.
      /// </summary>
      public abstract void Start();

      /// <summary>
      /// Updates the effect.
      /// </summary>
      public abstract void Update();

      /// <summary>
      /// Ends the effect.
      /// </summary>
      public abstract void End();
    }

    /// <summary>
    /// The effect magic missile.
    /// </summary>
    private class Effect_MagicMissile
      : Effect
    {
      /// <inheritdoc/>
      public override int Cost => 53;

      /// <inheritdoc/>
      public override int Timer { get; set; } = 0;

      /// <inheritdoc/>
      public override string Name => nameof(Effect_MagicMissile);

      /// <summary>
      /// Initializes a new instance of the <see cref="Effect_MagicMissile"/> class.
      /// </summary>
      /// <param name="self">The self.</param>
      /// <param name="other">The other.</param>
      public Effect_MagicMissile(Character self, Character other)
        : base(self, other)
      {
      }

      /// <inheritdoc/>
      public override void End()
      {
      }

      /// <inheritdoc/>
      public override void Start()
      {
        this.other.Health -= 4;
      }

      /// <inheritdoc/>
      public override void Update()
      {
      }
    }

    /// <summary>
    /// The effect drain.
    /// </summary>
    private class Effect_Drain
      : Effect
    {
      /// <inheritdoc/>
      public override int Cost => 73;

      /// <inheritdoc/>
      public override int Timer { get; set; } = 0;

      /// <inheritdoc/>
      public override string Name => nameof(Effect_Drain);

      /// <summary>
      /// Initializes a new instance of the <see cref="Effect_Drain"/> class.
      /// </summary>
      /// <param name="self">The self.</param>
      /// <param name="other">The other.</param>
      public Effect_Drain(Character self, Character other)
        : base(self, other)
      {
      }

      /// <inheritdoc/>
      public override void End()
      {
      }

      /// <inheritdoc/>
      public override void Start()
      {
        this.self.Health += 2;
        this.other.Health -= 2;
      }

      /// <inheritdoc/>
      public override void Update()
      {
      }
    }

    /// <summary>
    /// The effect sheild.
    /// </summary>
    private class Effect_Sheild
      : Effect
    {
      /// <summary>
      /// Armor change.
      /// </summary>
      private int armorChange = 7;

      /// <inheritdoc/>
      public override int Cost => 113;

      /// <inheritdoc/>
      public override int Timer { get; set; } = 6;

      /// <inheritdoc/>
      public override string Name => nameof(Effect_Sheild);

      /// <summary>
      /// Initializes a new instance of the <see cref="Effect_Sheild"/> class.
      /// </summary>
      /// <param name="self">The self.</param>
      /// <param name="other">The other.</param>
      public Effect_Sheild(Character self, Character other)
        : base(self, other)
      {
      }

      /// <inheritdoc/>
      public override void End()
      {
        this.self.Armor -= this.armorChange;
      }

      /// <inheritdoc/>
      public override void Start()
      {
        this.self.Armor += this.armorChange;
      }

      /// <inheritdoc/>
      public override void Update()
      {
      }
    }

    /// <summary>
    /// The effect poison.
    /// </summary>
    private class Effect_Poison
      : Effect
    {
      /// <inheritdoc/>
      public override int Cost => 173;

      /// <inheritdoc/>
      public override int Timer { get; set; } = 6;

      /// <inheritdoc/>
      public override string Name => nameof(Effect_Poison);

      /// <summary>
      /// Initializes a new instance of the <see cref="Effect_Poison"/> class.
      /// </summary>
      /// <param name="self">The self.</param>
      /// <param name="other">The other.</param>
      public Effect_Poison(Character self, Character other)
        : base(self, other)
      {
      }

      /// <inheritdoc/>
      public override void End()
      {
      }

      /// <inheritdoc/>
      public override void Start()
      {
      }

      /// <inheritdoc/>
      public override void Update()
      {
        this.other.Health -= 3;
      }
    }

    /// <summary>
    /// The effect recharge.
    /// </summary>
    private class Effect_Recharge
      : Effect
    {
      /// <inheritdoc/>
      public override int Cost => 229;

      /// <inheritdoc/>
      public override int Timer { get; set; } = 5;

      /// <inheritdoc/>
      public override string Name => nameof(Effect_Recharge);

      /// <summary>
      /// Initializes a new instance of the <see cref="Effect_Recharge"/> class.
      /// </summary>
      /// <param name="self">The self.</param>
      /// <param name="other">The other.</param>
      public Effect_Recharge(Character self, Character other)
        : base(self, other)
      {
      }

      /// <inheritdoc/>
      public override void End()
      {
      }

      /// <inheritdoc/>
      public override void Start()
      {
      }

      /// <inheritdoc/>
      public override void Update()
      {
        var player = this.self as Player;
        if (player is not null)
        {
          player.Mana += 101;
        }
      }
    }

    /// <summary>
    /// The character.
    /// </summary>
    private abstract class Character
    {
      /// <summary>
      /// Health cache.
      /// </summary>
      private int _healthCache;

      /// <summary>
      /// Damage cache.
      /// </summary>
      private int _damageCache;

      /// <summary>
      /// Armor cache.
      /// </summary>
      private int _armorCache;

      /// <summary>
      /// Mana cache.
      /// </summary>
      private int _manaCache;

      /// <summary>
      /// Gets or sets the health.
      /// </summary>
      public int Health { get; set; }

      /// <summary>
      /// Gets or sets the damage.
      /// </summary>
      public int Damage { get; set; }

      /// <summary>
      /// Gets or sets the armor.
      /// </summary>
      public int Armor { get; set; }

      /// <summary>
      /// Gets or sets the mana.
      /// </summary>
      public int Mana { get; set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="Character"/> class.
      /// </summary>
      /// <param name="health">The health.</param>
      /// <param name="damage">The damage.</param>
      /// <param name="mana">The mana.</param>
      public Character(int health, int damage, int mana)
      {
        this._healthCache = this.Health = health;
        this._damageCache = this.Damage = damage;
        this._armorCache = this.Armor = 0;
        this._manaCache = this.Mana = mana;
      }

      /// <summary>
      /// Resets.
      /// </summary>
      public void Reset()
      {
        this.Health = this._healthCache;
        this.Damage = this._damageCache;
        this.Armor = this._armorCache;
        this.Mana = this._manaCache;
      }

      /// <summary>
      /// Attacks the.
      /// </summary>
      /// <param name="other">The other.</param>
      /// <returns>A return.</returns>
      public abstract Return Attack(Character other);
    }

    /// <summary>
    /// The player.
    /// </summary>
    private class Player
      : Character
    {
      /// <summary>
      /// Random number generator.
      /// </summary>
      private readonly Random random = new Random();

      /// <summary>
      /// Gets or sets the mana used.
      /// </summary>
      public ulong ManaUsed { get; set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="Player"/> class.
      /// </summary>
      public Player()
        : base(50, 0, 500)
      {
      }

      /// <inheritdoc/>
      public override Return Attack(Character other)
      {
        var effectReturn = this.ChooseSpell(this, other);
        if (!effectReturn)
        {
          return effectReturn.Exception!;
        }

        var effect = effectReturn.Value!;
        effect.Start();
        if (effect.Timer > 0)
        {
          if (!AddEffect(effect, this, (Boss)other))
          {
            return new InvalidPatternException();
          }
        }

        return Return.Success();
      }

      /// <summary>
      /// Chooses the spell.
      /// </summary>
      /// <param name="self">The self.</param>
      /// <param name="other">The other.</param>
      /// <returns>An Effect.</returns>
      private Return<Effect> ChooseSpell(Character self, Character other)
      {
        var effect = GetEffect(this.random.Next(0, 5), (Player)self, (Boss)other);
        this.Mana -= effect.Cost;
        this.ManaUsed += (uint)effect.Cost;
        if (this.Mana < 0)
        {
          return new InvalidPatternException();
        }

        return Return.Success(effect);
      }
    }

    /// <summary>
    /// The boss.
    /// </summary>
    private class Boss
      : Character
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="Boss"/> class.
      /// </summary>
      /// <param name="health">The health.</param>
      /// <param name="damage">The damage.</param>
      public Boss(int health, int damage)
        : base(health, damage, 0)
      {
      }

      /// <inheritdoc/>
      public override Return Attack(Character other)
      {
        other.Health -= Math.Max(this.Damage - other.Armor, 1);
        return Return.Success();
      }
    }
  }
}