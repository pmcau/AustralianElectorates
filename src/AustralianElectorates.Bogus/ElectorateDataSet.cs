using Bogus;

namespace AustralianElectorates.Bogus;

public partial class ElectorateDataSet : DataSet
{
    public IEnumerable<string> Names(int num = 1)
    {
        Guard.AgainstNegative(num, nameof(num));
        for (var i = 0; i < num; i++)
        {
            yield return Name();
        }
    }

    public string Name() =>
        Electorate().Name;

    public IEnumerable<IElectorate> Electorate(int num = 1)
    {
        Guard.AgainstNegative(num, nameof(num));
        for (var i = 0; i < num; i++)
        {
            yield return Electorate();
        }
    }

    public IElectorate Electorate()
    {
        var index = Random.Number(DataLoader.Electorates.Count - 1);
        return DataLoader.Electorates[index];
    }
}