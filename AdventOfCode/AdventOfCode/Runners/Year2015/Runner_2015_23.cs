using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_2015_23.
  /// </summary>
  public class Runner_2015_23
    : RunnerBase
  {
    /// <summary>
    /// Parser.
    /// </summary>
    private static InstructionParser parser = new HalfParser
    {
      Next = new TrippleParser
      {
        Next = new IncrementParser
        {
          Next = new JumpParser
          {
            Next = new JumpIfEvenParser
            {
              Next = new JumpIfOneParser()
            }
          }
        }
      }
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_23"/> class.
    /// </summary>
    public Runner_2015_23()
      : base(2015, 23)
    {
    }

    /// <summary>
    /// The mnemonic.
    /// </summary>
    private enum Mnemonic
    {
      /// <summary>
      /// Half.
      /// </summary>
      HLF,

      /// <summary>
      /// Tripple.
      /// </summary>
      TPL,

      /// <summary>
      /// Incrment.
      /// </summary>
      INC,

      /// <summary>
      /// Jump.
      /// </summary>
      JMP,

      /// <summary>
      /// Jump if even.
      /// </summary>
      JIE,

      /// <summary>
      /// Jump if one.
      /// </summary>
      JIO
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var instructions = inputLines.Select(x => (x, default(Instruction))).ToArray();

      ulong part1 = ProcessInstructions(instructions, 0, 0).B;
      ulong part2 = ProcessInstructions(instructions, 1, 0).B;

      return (part1, part2);
    }

    /// <summary>
    /// Processes the instructions.
    /// </summary>
    /// <param name="instructions">The instructions.</param>
    /// <param name="a">Register a initial value.</param>
    /// <param name="b">Register b initial value.</param>
    /// <returns>A ComputerState.</returns>
    private static ComputerState ProcessInstructions((string raw, Instruction? parsed)[] instructions, ulong a, ulong b)
    {
      var computerState = new ComputerState
      {
        A = a,
        B = b
      };

      while (computerState.InstructionPointer >= 0 && computerState.InstructionPointer < instructions.Length)
      {
        ProcessInstruction(ref instructions[computerState.InstructionPointer], computerState);
      }

      return computerState;
    }

    /// <summary>
    /// Processes the instruction.
    /// </summary>
    /// <param name="instruction">The instruction.</param>
    /// <param name="state">The state.</param>
    private static void ProcessInstruction(ref (string raw, Instruction? parsed) instruction, ComputerState state)
    {
      if (instruction.parsed is null)
      {
        instruction.parsed = ParseInstruction(instruction.raw);
      }

      ProcessInstruction(instruction.parsed, state);
    }

    /// <summary>
    /// Parses the instruction.
    /// </summary>
    /// <param name="raw">The raw.</param>
    /// <returns>An Instruction.</returns>
    private static Instruction ParseInstruction(string raw)
    {
      return parser.Parse(raw.Split(' '));
    }

    /// <summary>
    /// Processes the instruction.
    /// </summary>
    /// <param name="instruction">The instruction.</param>
    /// <param name="state">The state.</param>
    private static void ProcessInstruction(Instruction instruction, ComputerState state)
    {
      switch (instruction.Mnemonic)
      {
        case Mnemonic.HLF:
          state.PerformAction(instruction.Register, v => v / 2);
          state.InstructionPointer++;
          break;

        case Mnemonic.TPL:
          state.PerformAction(instruction.Register, v => v * 3);
          state.InstructionPointer++;
          break;

        case Mnemonic.INC:
          state.PerformAction(instruction.Register, v => v + 1);
          state.InstructionPointer++;
          break;

        case Mnemonic.JMP:
          state.InstructionPointer += instruction.Offset;
          break;

        case Mnemonic.JIE:
          if ((state.ReadRegister(instruction.Register) % 2) == 0)
          {
            state.InstructionPointer += instruction.Offset;
            break;
          }

          state.InstructionPointer++;
          break;

        case Mnemonic.JIO:
          if (state.ReadRegister(instruction.Register) == 1)
          {
            state.InstructionPointer += instruction.Offset;
            break;
          }

          state.InstructionPointer++;
          break;
      }
    }

    /// <summary>
    /// The instruction parser.
    /// </summary>
    private abstract class InstructionParser
    {
      /// <summary>
      /// Gets or sets the next.
      /// </summary>
      public InstructionParser Next { get; set; } = null!;

      /// <summary>
      /// Gets the parse.
      /// </summary>
      public Instruction Parse(string[] parts)
      {
        if (this.CanParse(parts))
        {
          return this.ParseHelper(parts);
        }

        return this.Next.Parse(parts);
      }

      /// <summary>
      /// Cans the parse.
      /// </summary>
      /// <param name="parts">The parts.</param>
      /// <returns>A bool.</returns>
      protected abstract bool CanParse(string[] parts);

      /// <summary>
      /// Parses the helper.
      /// </summary>
      /// <param name="parts">The parts.</param>
      /// <returns>An Instruction.</returns>
      protected abstract Instruction ParseHelper(string[] parts);
    }

    /// <summary>
    /// The half parser.
    /// </summary>
    private class HalfParser
      : InstructionParser
    {
      /// <inheritdoc/>
      protected override bool CanParse(string[] parts)
      {
        return parts[0] == "hlf";
      }

      /// <inheritdoc/>
      protected override Instruction ParseHelper(string[] parts)
      {
        return new Instruction
        {
          Mnemonic = Mnemonic.HLF,
          Register = parts[1][0]
        };
      }
    }

    /// <summary>
    /// The tripple parser.
    /// </summary>
    private class TrippleParser
      : InstructionParser
    {
      /// <inheritdoc/>
      protected override bool CanParse(string[] parts)
      {
        return parts[0] == "tpl";
      }

      /// <inheritdoc/>
      protected override Instruction ParseHelper(string[] parts)
      {
        return new Instruction
        {
          Mnemonic = Mnemonic.TPL,
          Register = parts[1][0]
        };
      }
    }

    /// <summary>
    /// The increment parser.
    /// </summary>
    private class IncrementParser
      : InstructionParser
    {
      /// <inheritdoc/>
      protected override bool CanParse(string[] parts)
      {
        return parts[0] == "inc";
      }

      /// <inheritdoc/>
      protected override Instruction ParseHelper(string[] parts)
      {
        return new Instruction
        {
          Mnemonic = Mnemonic.INC,
          Register = parts[1][0]
        };
      }
    }

    /// <summary>
    /// The jump parser.
    /// </summary>
    private class JumpParser
      : InstructionParser
    {
      /// <inheritdoc/>
      protected override bool CanParse(string[] parts)
      {
        return parts[0] == "jmp";
      }

      /// <inheritdoc/>
      protected override Instruction ParseHelper(string[] parts)
      {
        return new Instruction
        {
          Mnemonic = Mnemonic.JMP,
          Offset = int.Parse(parts[1])
        };
      }
    }

    /// <summary>
    /// The jump if even parser.
    /// </summary>
    private class JumpIfEvenParser
      : InstructionParser
    {
      /// <inheritdoc/>
      protected override bool CanParse(string[] parts)
      {
        return parts[0] == "jie";
      }

      /// <inheritdoc/>
      protected override Instruction ParseHelper(string[] parts)
      {
        return new Instruction
        {
          Mnemonic = Mnemonic.JIE,
          Register = parts[1][0],
          Offset = int.Parse(parts[2])
        };
      }
    }

    /// <summary>
    /// The jump if one parser.
    /// </summary>
    private class JumpIfOneParser
      : InstructionParser
    {
      /// <inheritdoc/>
      protected override bool CanParse(string[] parts)
      {
        return parts[0] == "jio";
      }

      /// <inheritdoc/>
      protected override Instruction ParseHelper(string[] parts)
      {
        return new Instruction
        {
          Mnemonic = Mnemonic.JIO,
          Register = parts[1][0],
          Offset = int.Parse(parts[2])
        };
      }
    }

    /// <summary>
    /// The instruction.
    /// </summary>
    private class Instruction
    {
      /// <summary>
      /// Gets or sets the mnemonic.
      /// </summary>
      public Mnemonic Mnemonic { get; set; }

      /// <summary>
      /// Gets or sets the register.
      /// </summary>
      public char Register { get; set; }

      /// <summary>
      /// Gets or sets the offset.
      /// </summary>
      public int Offset { get; set; }
    }

    /// <summary>
    /// The computer state.
    /// </summary>
    private class ComputerState
    {
      /// <summary>
      /// Gets or sets the a.
      /// </summary>
      public ulong A { get; set; }

      /// <summary>
      /// Gets or sets the b.
      /// </summary>
      public ulong B { get; set; }

      /// <summary>
      /// Gets or sets the instruction pointer.
      /// </summary>
      public int InstructionPointer { get; set; }

      /// <summary>
      /// Performs the action.
      /// </summary>
      /// <param name="r">The r.</param>
      /// <param name="action">The action.</param>
      public void PerformAction(char r, Func<ulong, ulong> action)
      {
        ulong value = action(this.ReadRegister(r));
        this.SetRegister(r, value);
      }

      /// <summary>
      /// Reads the register.
      /// </summary>
      /// <param name="r">The r.</param>
      /// <returns>An ulong.</returns>
      public ulong ReadRegister(char r)
      {
        if (r == 'a')
        {
          return this.A;
        }

        return this.B;
      }

      /// <summary>
      /// Sets the register.
      /// </summary>
      /// <param name="r">The r.</param>
      /// <param name="value">The value.</param>
      public void SetRegister(char r, ulong value)
      {
        if (r == 'a')
        {
          this.A = value;
          return;
        }

        this.B = value;
      }
    }
  }
}