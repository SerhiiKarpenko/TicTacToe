namespace Code_Base.Utilities
{
  public static class ColorUtilities
  {
    public static string GenerateHex(string seed)
      => seed.GetHashCode().ToString("X")[..6];
  }
}