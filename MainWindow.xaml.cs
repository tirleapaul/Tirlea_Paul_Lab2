using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tirlea_Paul_Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    class PizzaMachine
    {
        private PizzaType mIngredients;
        public PizzaType Ingredients
        {
            get
            {
                return mIngredients;
            }
            set
            {
                mIngredients = value;
            }
        }
        private System.Collections.ArrayList mPizzas = new System.Collections.ArrayList();
        public Pizza this[int Index]
        {
            get
            {
                return (Pizza)mPizzas[Index];
            }
            set
            {
                mPizzas[Index] = value;
            }
        }
        public delegate void PizzaCompleteDelegate();
        public event PizzaCompleteDelegate PizzaComplete;
        DispatcherTimer pizzaBakeTimer;
        private void InitializeComponent()
        {
            this.pizzaBakeTimer = new DispatcherTimer();
            this.pizzaBakeTimer.Tick += new System.EventHandler(this.pizzaBakeTimer_Tick);
        }
        public PizzaMachine()
        {
            InitializeComponent();
        }

        private void pizzaBakeTimer_Tick(object sender, EventArgs e)
        {
            Pizza aPizza = new Pizza(this.Ingredients);
            mPizzas.Add(aPizza);
            PizzaComplete();
        }
        public bool Enabled
        {
            set
            {
                pizzaBakeTimer.IsEnabled = value;
            }
        }
        public int Interval
        {
            set
            {
                pizzaBakeTimer.Interval = new TimeSpan(0, 0, value);
            }
        }
        public void MakePizzas(PizzaType dIngredients)
        {
            Ingredients = dIngredients;

            switch (Ingredients)
            {
                case PizzaType.Canibale: Interval = 3; break;
                case PizzaType.Margherita: Interval = 2; break;
                case PizzaType.Pepperoni: Interval = 5; break;
                case PizzaType.Quattro_Stagioni: Interval = 7; break;
                case PizzaType.Veggie: Interval = 4; break;
            }
            pizzaBakeTimer.Start();
        }

    }
    enum PizzaType
    {
        Margherita,
        Pepperoni,
        Veggie,
        Quattro_Stagioni,
        Canibale
    }
    class Pizza
    {
        private PizzaType mIngredients; // câmp
        public PizzaType Ingredients // proprietate
        {
            get //metoda get
            {
                return mIngredients;
            }
            set //metoda set
            {
                mIngredients = value;
            }
        }
        private float mPrice = .50F;
        private float Price
        {
            get
            {
                return mPrice;
            }
            set
            {
                mPrice = value;
            }
        }
        private readonly DateTime mTimeOfCreation;
        public DateTime TimeOfCreation
        {
            get
            {
                return mTimeOfCreation;
            }
        }
        public Pizza(PizzaType aIngredients) // constructor
        {
            mTimeOfCreation = DateTime.Now;
            mIngredients = aIngredients;
        }
    }// end Pizza class

    public partial class MainWindow : Window
    {
        private PizzaMachine myPizzaMachine;

        private int mMargheritaPizza;
        private int mPepperoniPizza;
        private int mVeggiePizza;
        private int mQuattroStagioniPizza;
        private int mCanibalePizza;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            myPizzaMachine = new PizzaMachine();
            myPizzaMachine.PizzaComplete += new PizzaMachine.PizzaCompleteDelegate(PizzaCompleteHandler);
        }
        private void margPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = true;
            pepPizzaMenuItem.IsChecked = false;
            vegPizzaMenuItem.IsChecked = false;
            quatPizzaMenuItem.IsChecked = false;
            canPizzaMenuItem.IsChecked = false;
            myPizzaMachine.MakePizzas(PizzaType.Margherita);
        }
        private void pepPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = false;
            pepPizzaMenuItem.IsChecked = true;
            vegPizzaMenuItem.IsChecked = false;
            quatPizzaMenuItem.IsChecked = false;
            canPizzaMenuItem.IsChecked = false;
            myPizzaMachine.MakePizzas(PizzaType.Pepperoni);
        }
        private void vegPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = false;
            pepPizzaMenuItem.IsChecked = false;
            vegPizzaMenuItem.IsChecked = true;
            quatPizzaMenuItem.IsChecked = false;
            canPizzaMenuItem.IsChecked = false;
            myPizzaMachine.MakePizzas(PizzaType.Veggie);
        }
        private void quatPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = false;
            pepPizzaMenuItem.IsChecked = false;
            vegPizzaMenuItem.IsChecked = false;
            quatPizzaMenuItem.IsChecked = true;
            canPizzaMenuItem.IsChecked = false;
            myPizzaMachine.MakePizzas(PizzaType.Quattro_Stagioni);
         
        }
        private void canPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = false;
            pepPizzaMenuItem.IsChecked = false;
            vegPizzaMenuItem.IsChecked = false;
            quatPizzaMenuItem.IsChecked = false;
            canPizzaMenuItem.IsChecked = true;
            myPizzaMachine.MakePizzas(PizzaType.Canibale);
        }

        private void PizzaCompleteHandler()
        {
            switch (myPizzaMachine.Ingredients)
            {
                case PizzaType.Margherita:
                    mMargheritaPizza++;
                    txtMargheritaPizza.Text = mMargheritaPizza.ToString();
                    break;
                case PizzaType.Pepperoni:
                    mPepperoniPizza++;
                    txtPepperoniPizza.Text = mPepperoniPizza.ToString();
                    break;
                case PizzaType.Veggie:
                    mVeggiePizza++;
                    txtVeggiePizza.Text = mVeggiePizza.ToString();
                    break;
                case PizzaType.Canibale:
                    mCanibalePizza++;
                    txtCanibalePizza.Text = mCanibalePizza.ToString();
                    break;
                case PizzaType.Quattro_Stagioni:
                    mQuattroStagioniPizza++;
                    txtQuatroPizza.Text = mQuattroStagioniPizza.ToString();
                    break;
            }
        }
        private void stopMenuItem_Click(object sender, RoutedEventArgs e)
        {
            myPizzaMachine.Enabled = false;
        }

        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9))
            {
                MessageBox.Show("Numai cifre se pot introduce!", "Input Error", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
    }
}
