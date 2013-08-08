using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CQRS_ByMe.Commands;
using CQRS_ByMe.EventBus;
using CQRS_ByMe.Events;
using WpfApplication1.Annotations;

namespace WpfApplication1
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IEnumerable<Event> _events;
        private IEnumerable<CustomerDto> _customers;

        public IEnumerable<Event> Events
        {
            get { return _events; }
            set { _events = value; OnPropertyChanged("Events"); }
        }

        public IEnumerable<CustomerDto> Customers
        {
            get { return _customers; }
            set { _customers = value; OnPropertyChanged("Customers"); }
        }

        public MainViewModel()
        {
            App.Bus.Send(new CreateItemCommand(Guid.NewGuid(), "Orlando", "Perri", 25));
            UpdateView();
        }

        private void UpdateView()
        {
            Events = from e in new InMemoryEventStore().GetAllEvents()
                     select new Event
                         {
                             EventName = e.GetType().Name,
                             Version = e.Version,
                             Properties = getPropertiesFrom(e)
                         };
            Customers = null;
            Customers = new InMemoryReportRepository().GetAll();
        }

        public string IdText { get; set; }
        public string NameChangedText { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }


        public ICommand SendNameChangedCommand
        {
            get
            {
                return new MyCommand(() =>
                    {
                        App.Bus.Send(new ChangeItemCommand(new Guid(IdText), NameChangedText, LastName, Age ));
                        UpdateView();
                    });
            }
        }
        public ICommand SendItemCreatedCommand
        {
            get
            {
                return new MyCommand(() =>
                {
                    App.Bus.Send(new CreateItemCommand(Guid.NewGuid(), NameChangedText, LastName, Age));
                    UpdateView();
                });
            }
        }

        public class MyCommand : ICommand
        {
            private Action action;

            public MyCommand(Action action)
            {
                this.action = action;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                action();
            }

            public event EventHandler CanExecuteChanged;
        }



        private string getPropertiesFrom(object e)
        {
            return string.Join(" <==> ", e.GetType().GetProperties().Select(x => string.Format("{0} : {1}", x.Name, x.GetValue(e))));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Event
    {
        public string EventName { get; set; }
        public int Version { get; set; }
        public string Properties { get; set; }

    }
}