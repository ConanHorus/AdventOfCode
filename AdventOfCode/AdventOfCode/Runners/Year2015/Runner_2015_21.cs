using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_2015_21.
  /// </summary>
  public class Runner_2015_21
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_21"/> class.
    /// </summary>
    public Runner_2015_21()
      : base(2015, 21)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var boss = new Boss(inputLines);
      var player = new Player();
      var store = GenerateStore();

      int bossHelathCache = boss.Health;

      int part1 = int.MaxValue;
      int part2 = 0;

      for (int buyToken = 0; buyToken <= ushort.MaxValue; buyToken++)
      {
        boss.Health = bossHelathCache;
        player.Clear();
        foreach (var item in store.Buy((ushort)buyToken))
        {
          player.AddItem(item);
        }

        bool playerWon = false;
        do
        {
          if (player.Attack(boss))
          {
            playerWon = true;
            break;
          }

          if (boss.Attack(player))
          {
            break;
          }
        }
        while (true);

        if (playerWon)
        {
          part1 = Math.Min(part1, player.GetPrice());
        }

        if (!playerWon)
        {
          part2 = Math.Max(part2, player.GetPrice());
        }
      }

      return (part1, part2);
    }

    /// <summary>
    /// Generates the store.
    /// </summary>
    /// <returns>A Store.</returns>
    private static Store GenerateStore()
    {
      var store = new Store();

      store.Weapons.AddRange(new Item[]
      {
        new Item("Dagger",        8,     4,       0),
        new Item("Shortsword",   10,     5,       0),
        new Item("Warhammer",    25,     6,       0),
        new Item("Longsword",    40,     7,       0),
        new Item("Greataxe",     74,     8,       0)
      });

      store.Armor.AddRange(new Item[]
      {
        new Item("Leather",      13,     0,       1),
        new Item("Chainmail",    31,     0,       2),
        new Item("Splintmail",   53,     0,       3),
        new Item("Bandedmail",   75,     0,       4),
        new Item("Platemail",   102,     0,       5)
      });

      store.Rings.AddRange(new Item[]
      {
        new Item("Damage +1",    25,     1,       0),
        new Item("Damage +2",    50,     2,       0),
        new Item("Damage +3",   100,     3,       0),
        new Item("Defense +1",   20,     0,       1),
        new Item("Defense +2",   40,     0,       2),
        new Item("Defense +3",   80,     0,       3)
      });

      return store;
    }

    /// <summary>
    /// The store.
    /// </summary>
    private class Store
    {
      /// <summary>
      /// Buying buffer.
      /// </summary>
      private readonly bool[] buyBuffer = new bool[16];

      /// <summary>
      /// Gets the weapons.
      /// </summary>
      public List<Item> Weapons { get; } = new List<Item>();

      /// <summary>
      /// Gets the armor.
      /// </summary>
      public List<Item> Armor { get; } = new List<Item>();

      /// <summary>
      /// Gets rings.
      /// </summary>
      public List<Item> Rings { get; } = new List<Item>();

      /// <summary>
      /// Buys the.
      /// </summary>
      /// <param name="token">The token.</param>
      /// <returns>A list of Items.</returns>
      public IEnumerable<Item> Buy(ushort token)
      {
        for (int i = 0; i < 16; i++)
        {
          this.buyBuffer[i] = ((token >> i) & 1) > 0;
        }

        if (this.buyBuffer.Take(5).Where(x => x).Count() != 1)
        {
          yield break;
        }

        if (this.buyBuffer.Skip(5).Take(5).Where(x => x).Count() > 1)
        {
          yield break;
        }

        if (this.buyBuffer.Skip(10).Where(x => x).Count() > 2)
        {
          yield break;
        }

        for (int i = 0; i < this.buyBuffer.Length; i++)
        {
          if (!this.buyBuffer[i])
          {
            continue;
          }

          if (i < 5)
          {
            yield return this.Weapons[i];
          }

          if (i >= 5 && i < 10)
          {
            yield return this.Armor[i - 5];
          }

          if (i >= 10)
          {
            yield return this.Rings[i - 10];
          }
        }
      }
    }

    /// <summary>
    /// The character.
    /// </summary>
    private abstract class Character
    {
      /// <summary>
      /// Gets or sets the health.
      /// </summary>
      public int Health { get; set; }

      /// <summary>
      /// Gets the damage.
      /// </summary>
      protected int Damage => this.GetDamage();

      /// <summary>
      /// Gets the armor.
      /// </summary>
      protected int Armor => this.GetArmor();

      /// <summary>
      /// Attacks the.
      /// </summary>
      /// <param name="other">The other.</param>
      /// <returns>Whether attack killed the other character.</returns>
      public bool Attack(Character other)
      {
        int damage = Math.Max(this.Damage - other.Armor, 1);
        other.Health -= damage;

        return other.Health <= 0;
      }

      /// <summary>
      /// Gets the damage.
      /// </summary>
      /// <returns>An int.</returns>
      protected abstract int GetDamage();

      /// <summary>
      /// Gets the armor.
      /// </summary>
      /// <returns>An int.</returns>
      protected abstract int GetArmor();
    }

    /// <summary>
    /// The boss.
    /// </summary>
    private class Boss
      : Character
    {
      /// <summary>
      /// Damage.
      /// </summary>
      private readonly int _damage;

      /// <summary>
      /// Armor.
      /// </summary>
      private readonly int _armor;

      /// <summary>
      /// Initializes a new instance of the <see cref="Boss"/> class.
      /// </summary>
      /// <param name="inputLines">The input lines.</param>
      public Boss(string[] inputLines)
      {
        foreach (string line in inputLines)
        {
          string[] parts = line.Split(": ");
          string label = parts[0];
          int value = int.Parse(parts[1]);
          _ = label switch
          {
            "Hit Points" => this.Health = value,
            "Damage" => this._damage = value,
            "Armor" => this._armor = value,
            _ => throw new NotImplementedException(),
          };
        }
      }

      /// <summary>
      /// Gets the armor.
      /// </summary>
      /// <returns>An int.</returns>
      protected override int GetArmor()
      {
        return this._armor;
      }

      /// <summary>
      /// Gets the damage.
      /// </summary>
      /// <returns>An int.</returns>
      protected override int GetDamage()
      {
        return this._damage;
      }
    }

    /// <summary>
    /// The player.
    /// </summary>
    private class Player
      : Character
    {
      /// <summary>
      /// Armor.
      /// </summary>
      private int? _armor;

      /// <summary>
      /// Damage.
      /// </summary>
      private int? _damage;

      /// <summary>
      /// Inventory.
      /// </summary>
      private List<Item> inventory = new List<Item>();

      /// <summary>
      /// Adds item.
      /// </summary>
      /// <param name="item">Item to add.</param>
      public void AddItem([DisallowNull] Item item)
      {
        this.inventory.Add(item);
        this.ResetStats();
      }

      /// <summary>
      /// Clears inventory and stats.
      /// </summary>
      public void Clear()
      {
        this.inventory.Clear();
        this.ResetStats();
      }

      /// <summary>
      /// Gets the price.
      /// </summary>
      /// <returns>An int.</returns>
      public int GetPrice()
      {
        return this.inventory.Select(x => x.Cost).Sum();
      }

      /// <summary>
      /// Gets the armor.
      /// </summary>
      /// <returns>An int.</returns>
      protected override int GetArmor()
      {
        if (this._armor is null)
        {
          this.CalculateArmor();
        }

        return (int)this._armor;
      }

      /// <summary>
      /// Gets the damage.
      /// </summary>
      /// <returns>An int.</returns>
      protected override int GetDamage()
      {
        if (this._damage is null)
        {
          this.CalculateDamage();
        }

        return (int)this._damage;
      }

      /// <summary>
      /// Resets stats.
      /// </summary>
      private void ResetStats()
      {
        this.Health = 100;
        this._armor = null;
        this._damage = null;
      }

      /// <summary>
      /// Calculates armor.
      /// </summary>
      [MemberNotNull(nameof(_armor))]
      private void CalculateArmor()
      {
        this._armor = this.inventory.Select(x => x.Armor).Sum();
      }

      /// <summary>
      /// Calculates damage.
      /// </summary>
      [MemberNotNull(nameof(_damage))]
      private void CalculateDamage()
      {
        this._damage = this.inventory.Select(x => x.Damage).Sum();
      }
    }

    /// <summary>
    /// Item record.
    /// </summary>
    private record Item(string Name, int Cost, int Damage, int Armor);
  }
}