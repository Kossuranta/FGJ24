public class Helper
{
    public static void Shuffle<T>(T[] _items)
    {
        System.Random rand = new();
        for (int i = 0; i < _items.Length - 1; i++)
        {
            int j = rand.Next(i, _items.Length);
            (_items[i], _items[j]) = (_items[j], _items[i]);
        }
    }
}