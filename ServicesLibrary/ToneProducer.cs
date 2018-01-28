using System;

namespace ServicesLibrary
{
  /// <summary>
  /// </summary>
  public class ToneProducer
  {
    /// <summary>
    /// Calculates the hertz value of a given music key
    /// </summary>
    /// <param name="key">Index of a key</param>
    /// <returns>Hertz value</returns>
    public static double GetHertz(double key)
    {
      return 440 * Math.Pow(2.0, (key - 49.0) / 12.0);
    }
  }
}