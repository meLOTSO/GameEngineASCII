namespace StateCore.Delegates;

public delegate void SetStateDelegate<TKey>(TKey state) where TKey : notnull;