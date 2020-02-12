using System;
using System.Reactive.Disposables;

namespace ChroniusXF.Utils
{
    public static class IDisposableExtensions
    {
        public static IDisposable DisposeWith(this IDisposable disposable, CompositeDisposable compositeDisposable)
        {
            if (disposable == null)
                return disposable;

            compositeDisposable?.Add(disposable);
            return disposable;
        }
    }
}
