using System.Collections.Generic;
using Code_Base.Command;
using Code_Base.TicTacToeGrid;

namespace Code_Base.Infrastructure.Services.CommandService
{
  public class CommandService : ICommandService
  {
    private readonly Stack<IMoveCommand> moveCommands = new();
    
    public void MakeMoveCommand(GridCellPresenter gridCellPresenter)
    { 
      IMoveCommand moveCommand = new MoveCommand(gridCellPresenter);
      moveCommands.Push(moveCommand);
      moveCommand.Execute();
    }

    public void UndoLastMove()
    {
      if (moveCommands.TryPop(out IMoveCommand moveCommand))
      {
        moveCommand.Undo();
      }
    }

    public void Dispose() => 
      moveCommands.Clear();
  }
}