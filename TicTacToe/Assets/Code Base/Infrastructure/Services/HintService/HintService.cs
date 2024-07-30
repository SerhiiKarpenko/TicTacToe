using Code_Base.Infrastructure.Services.GridPresenterProvider;

namespace Code_Base.Infrastructure.Services.HintService
{
  public class HintService : IHintService
  {
    private readonly IGridPresenterProvider gridPresenterProvider;

    public HintService(IGridPresenterProvider gridPresenterProvider)
    {
      this.gridPresenterProvider = gridPresenterProvider;
    }

    public void ShowHint() => 
      gridPresenterProvider.GridPresenter.GetRandomNonOccupiedGridCellPresenter().ShowHintOutline();
  }
}