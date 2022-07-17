namespace smarthotel.ui.Store.Covids.Actions.FetchSimilarCovids
{
  public class FetchCovidSimilarListAction
  {
    public string Nfc { get; }

    public FetchCovidSimilarListAction(string nfc)
    {
      Nfc = nfc;
    }
  }
}