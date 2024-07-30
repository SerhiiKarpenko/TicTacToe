using Code_Base.Enums;
using Code_Base.Infrastructure.Services.DrawWinService;
using Code_Base.TicTacToeGrid;
using NUnit.Framework;
using Zenject;

namespace Code_Base.Tests
{
  [TestFixture]
  public class DrawTest : ZenjectUnitTestFixture
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
    public void TestDraw()
    {
      IDrawWinService drawWinService = Container.Resolve<IDrawWinService>();
      
      IGrid grid = new Grid(3,3);
      grid.InitializeGrid();
      
      drawWinService.SetupGrid(grid);
      
      grid.GridCells[0,0].OccupyBy(PlayerType.FirstPlayer);
      grid.GridCells[0,1].OccupyBy(PlayerType.SecondPlayer);
      grid.GridCells[0,2].OccupyBy(PlayerType.FirstPlayer);
      grid.GridCells[1,0].OccupyBy(PlayerType.SecondPlayer);
      grid.GridCells[1,1].OccupyBy(PlayerType.FirstPlayer);
      grid.GridCells[1,2].OccupyBy(PlayerType.SecondPlayer);
      grid.GridCells[2,0].OccupyBy(PlayerType.SecondPlayer);
      grid.GridCells[2,1].OccupyBy(PlayerType.FirstPlayer);
      grid.GridCells[2,2].OccupyBy(PlayerType.SecondPlayer);
      
      bool win = drawWinService.CheckDraw();
      Assert.IsTrue(win, "Row Win");
    }
    
    [TearDown]
    public void TearDown()
    {
      Container.UnbindAll();
    }
  }
}