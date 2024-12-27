using System.Text.RegularExpressions;

public static class StringHelpers
{
    public static void ExtrairDatasLinkedin(string texto, out DateTime dataInicial, out DateTime dataFinal)
    {
        var regex = new Regex(@"(?:De\s(\w{3})\sde\s(\d{4})\saté\s(\w{3}|o momento)(?:\sde\s(\d{4}))?)|(?:(\d{4})\s*-\s*(\d{4}|o momento))", RegexOptions.IgnoreCase);
        var match = regex.Match(texto);

        if (match.Success)
        {
            var meses = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                {"jan", 1}, {"fev", 2}, {"mar", 3}, {"abr", 4},
                {"mai", 5}, {"jun", 6}, {"jul", 7}, {"ago", 8},
                {"set", 9}, {"out", 10}, {"nov", 11}, {"dez", 12}
            };

            if (!string.IsNullOrEmpty(match.Groups[1].Value) && !string.IsNullOrEmpty(match.Groups[2].Value))
            {
                var mesInicial = meses[match.Groups[1].Value.ToLower()];
                var anoInicial = int.Parse(match.Groups[2].Value);
                dataInicial = new DateTime(anoInicial, mesInicial, 1);

                if (match.Groups[3].Value.Equals("o momento", StringComparison.OrdinalIgnoreCase))
                {
                    dataFinal = DateTime.MinValue;
                }
                else
                {
                    var mesFinal = meses[match.Groups[3].Value.ToLower()];
                    var anoFinal = match.Groups[4].Success ? int.Parse(match.Groups[4].Value) : anoInicial;
                    dataFinal = new DateTime(anoFinal, mesFinal, 1);
                }
            }
            else if (!string.IsNullOrEmpty(match.Groups[5].Value) && !string.IsNullOrEmpty(match.Groups[6].Value))
            {
                var anoInicial = int.Parse(match.Groups[5].Value);
                dataInicial = new DateTime(anoInicial, 1, 1);

                if (match.Groups[6].Value.Equals("o momento", StringComparison.OrdinalIgnoreCase))
                {
                    dataFinal = DateTime.MinValue;
                }
                else
                {
                    var anoFinal = int.Parse(match.Groups[6].Value);
                    dataFinal = new DateTime(anoFinal, 1, 1);
                }
            }
            else
            {
                dataInicial = DateTime.MinValue;
                dataFinal = DateTime.MinValue;
            }
        }
        else
        {
            dataInicial = DateTime.MinValue;
            dataFinal = DateTime.MinValue;
        }
    }
}
