using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace Evidence.ViewModel
{
    class ViewModelMain : INotifyPropertyChanged
    {
        public string _TxtKrestni { get; set; }
        public string _TxtPrijmeni { get; set; }
        public string _TxtTelefon { get; set; }

        private ObservableCollection<Zakaznik> _Files { get; set; }
        public ObservableCollection<Zakaznik> Files
        {
            get { return _Files; }
            set
            {
                _Files = value;
                OnPropertyChanged("Files");
            }
        }
        List<Zakaznik> items = new List<Zakaznik>();

        private Zakaznik _selected;
        public Zakaznik Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                SelectedKrestni = "";
                SelectedPrijmeni = "";
                SelectedTelefon = "";
                if(Selected != null)
                {
                    if (string.IsNullOrEmpty(SelectedKrestni))
                    {
                        SelectedKrestni = Selected.Krestni;
                        OnPropertyChanged("SelectedKrestni");
                    }
                    else
                    {
                        Console.WriteLine(SelectedKrestni);
                    }
                    if (string.IsNullOrEmpty(SelectedPrijmeni))
                    {
                        SelectedPrijmeni = Selected.Prijmeni;
                        OnPropertyChanged("SelectedPrijmeni");
                    }
                    else
                    {

                    }
                    if (string.IsNullOrEmpty(SelectedTelefon))
                    {
                        SelectedTelefon = Selected.Telefon;
                        OnPropertyChanged("SelectedTelefon");
                    }
                    else
                    {

                    }
                }
                
                OnPropertyChanged("Selected");
            }
        }
        private Zakaznik _selectedNew;
        public Zakaznik SelectedNew
        {
            get { return _selected; }
            set
            {
                _selectedNew = value;
                OnPropertyChanged("SelectedNew");
            }
        }
        private string _SelectedKrestni;
        public string SelectedKrestni
        {
            get => _SelectedKrestni;
            set
            {
                _SelectedKrestni = value;
                Console.WriteLine(SelectedKrestni);
                OnPropertyChanged("SelectedKrestni");
            }
        }
        private string _SelectedPrijmeni;
        public string SelectedPrijmeni
        {
            get => _SelectedPrijmeni;
            set
            {
                _SelectedPrijmeni = value;
                Console.WriteLine(SelectedPrijmeni);
                OnPropertyChanged("SelectedPrijmeni");
            }
        }
        private string _SelectedTelefon;
        public string SelectedTelefon
        {
            get => _SelectedTelefon;
            set
            {
                _SelectedTelefon = value;
                Console.WriteLine(SelectedTelefon);
                OnPropertyChanged("SelectedTelefon");
            }
        }

        public string TxtKrestni
        {
            get => _TxtKrestni;
            set
            {
                _TxtKrestni = value;
                Console.WriteLine(TxtKrestni);
                OnPropertyChanged("TxtKrestni");
            }
        }

        public string TxtPrijmeni
        {
            get => _TxtPrijmeni;
            set
            {
                _TxtPrijmeni = value;
                OnPropertyChanged("TxtPrijmeni");
            }
        }

        public string TxtTelefon
        {
            get => _TxtTelefon;
            set
            {
                _TxtTelefon = value;
                OnPropertyChanged("TxtTelefon");
            }
        }

        public RelayCommand ClickCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public ViewModelMain()
        {
            Files = new ObservableCollection<Zakaznik>();
            ClickCommand = new RelayCommand(ClickMethod);
            SaveCommand = new RelayCommand(SaveEdited);
            DeleteCommand = new RelayCommand(DeleteMethod);
            LoadData();
            
        }
        public void LoadData()
        {
            Zakaznik zakaznik = JsonConvert.DeserializeObject<Zakaznik>(File.ReadAllText("zaznam.json"));

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText("zaznam.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                Zakaznik movie2 = (Zakaznik)serializer.Deserialize(file, typeof(Zakaznik));
            }
            Files.Add(zakaznik);
            OnPropertyChanged("Files");
        }
        public void DeleteMethod()
        {
            Files.Remove(Selected);
            OnPropertyChanged("Files");
        }
        public void ClickMethod()
        {
            Zakaznik zakaznik = new Zakaznik();
            zakaznik.Krestni = TxtKrestni;
            zakaznik.Prijmeni = TxtPrijmeni;
            zakaznik.Telefon = TxtTelefon;
            
            Files.Add(zakaznik);
            File.WriteAllText("zaznam.json", JsonConvert.SerializeObject(zakaznik));
            using (StreamWriter file = File.CreateText(@"zaznam.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, zakaznik);
            }
            
        }
        public void SaveEdited()
        {
            SelectedNew.Krestni = SelectedKrestni;
            SelectedNew.Prijmeni = SelectedPrijmeni;
            SelectedNew.Telefon = SelectedTelefon;
            Files.Add(SelectedNew);
            Files.Remove(Selected);
            OnPropertyChanged("Files");
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
