﻿namespace _011Global.Shared.Utils
{
    public static class ObjectsMapper
    {
        public static TOut Map<TIn, TOut>(TIn source) where TOut : new()
        {
            var inPropDict = typeof(TIn).GetProperties()
                .Where(p => p.CanRead)
                .ToDictionary(p => p.Name);

            var outProps = typeof(TOut).GetProperties()
                .Where(p => p.CanWrite);

            var destination = new TOut();

            foreach (var outProp in outProps)
            {
                if (inPropDict.TryGetValue(outProp.Name, out var inProp))
                {
                    object? sourceValue = inProp.GetValue(source);

                    if (inProp.PropertyType == outProp.PropertyType)
                    {
                        outProp.SetValue(destination, sourceValue);
                    }
                }
            }

            return destination;
        }
    }
}
