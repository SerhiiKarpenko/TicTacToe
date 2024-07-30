using Code_Base.Enums;
using Code_Base.Infrastructure.Services.DrawWinService;
using Code_Base.TicTacToeGrid;
using NUnit.Framework;
using Zenject;

namespace Code_Base.Tests
{
  [TestFixture]
  public class WinTest : ZenjectUnitTestFixture
  {

    [SetUp]
    public void BindInterfaces()
    {
      WinDrawLoseTestInstaller.Install(Container);
    }
    
    [Test]
    public void TestResolveIDrawWinService()
    {
      IDrawWinService drawWinService = Container.Resolve<IDrawWinService>();
      Assert.NotNull(drawWinService, "drawWinService != null");
    }

    [Test]
    public void TestLeftDiagonalWin()
    {
      IDrawWinService drawWinService = Container.Resolve<IDrawWinService>();
      
      IGrid grid = new Grid(3,3);
      grid.InitializeGrid();
      
      drawWinService.SetupGrid(grid);
      
      grid.GridCells[0,0].OccupyBy(PlayerType.FirstPlayer);
      grid.GridCells[1,1].OccupyBy(PlayerType.FirstPlayer);
      grid.GridCells[2,2].OccupyBy(PlayerType.FirstPlayer);

      bool win = drawWinService.CheckWin();
      Assert.IsTrue(win, "left Diagonal Win");
    }
    
    [Test]
    public void TestRightDiagonalWin()
    {
      IDrawWinService drawWinService = Container.Resolve<IDrawWinService>();
      
      IGrid grid = new Grid(3,3);
      grid.InitializeGrid();
      
      drawWinService.SetupGrid(grid);
      
      grid.GridCells[2,0].OccupyBy(PlayerType.FirstPlayer);
      grid.GridCells[1,1].OccupyBy(PlayerType.FirstPlayer);
      grid.GridCells[0,2].OccupyBy(PlayerType.FirstPlayer);

      bool win = drawWinService.CheckWin();
      Assert.IsTrue(win, "Right diagonal win");
    }
    
    [Test]
    public void TestColumnWin()
    {
      IDrawWinService drawWinService = Container.Resolve<IDrawWinService>();
      
      IGrid grid = new Grid(3,3);
      grid.InitializeGrid();
      
      drawWinService.SetupGrid(grid);
      
      grid.GridCells[1,0].OccupyBy(PlayerType.FirstPlayer);
      grid.GridCells[1,0].OccupyBy(PlayerType.FirstPlayer);
      grid.GridCells[2,0].OccupyBy(PlayerType.FirstPlayer);

      bool win = drawWinService.CheckWin();
      Assert.IsTrue(win, "Column Win");
    }
    
    [Test]
    public void TestRowWin()
    {
      IDrawWinService drawWinService = Container.Resolve<IDrawWinService>();
      
      IGrid grid = new Grid(3,3);
      grid.InitializeGrid();
      
      drawWinService.SetupGrid(grid);
      
      grid.GridCells[1,0].OccupyBy(PlayerType.FirstPlayer);
      grid.GridCells[0,1].OccupyBy(PlayerType.FirstPlayer);
      grid.GridCells[0,2].OccupyBy(PlayerType.FirstPlayer);

      bool win = drawWinService.CheckWin();
      Assert.IsTrue(win, "Row Win");
    }
    
    [TearDown]
    public void TearDown()
    {
      Container.UnbindAll();
    }
  }
}