using System;
using System.Reactive.Disposables;
using Prism.Mvvm;

namespace ChroniusXF.ViewModels
{
    public class ViewModelBase : BindableBase, IDisposable
    {
        protected CompositeDisposable ViewModelSubscriptions = new CompositeDisposable();

        public void Dispose()
        {
            ViewModelSubscriptions.Clear();
            ViewModelSubscriptions.Dispose();
            ViewModelSubscriptions = null;
        }
    }
}
