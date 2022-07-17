namespace smarthotel.ui.Store.Covids.Actions.FetchCovids
{
  public class FetchCovidListAction
  {
    public string Nfc { get; }

    public FetchCovidListAction(string nfc)
    {
      Nfc = nfc;
    }
  }
}