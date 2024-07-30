namespace Code_Base.Command
{
  public interface IMoveCommand
  {
    public void Execute();
    public void Undo();
  }
}