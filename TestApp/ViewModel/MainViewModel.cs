using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace TestApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Binding properties

        private IPageViewModel _currentViewModel;
        /// <summary>
        /// <para>
        /// �����: ������ ������
        /// </para>
        /// 
        /// ������� ViewModel ��������. ����� ���� <see cref="calculationVM"/> ��� <see cref="settingsVM"/>.
        /// 
        /// </summary>
        public IPageViewModel currentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if(_currentViewModel == value)
                {
                    return;
                }

                _currentViewModel = value;
                RaisePropertyChanged("currentViewModel");
            }
        }

        private ICommand _switchToCalculationCommand;
        /// <summary>
        /// <para>
        /// �����: ������ ������
        /// </para>
        /// 
        /// ������� ��� ������������ � �������� ��������.
        /// 
        /// </summary>
        public ICommand switchToCalculationCommand
        {
            get
            {
                return _switchToCalculationCommand;
            }
            set
            {
                if(_switchToCalculationCommand == value)
                {
                    return;
                }

                _switchToCalculationCommand = value;
                RaisePropertyChanged("switchToCalculation");
            }
        }

        private ICommand _switchToSettingsCommand;
        /// <summary>
        /// <para>
        /// �����: ������ ������
        /// </para>
        /// 
        /// ������� ��� ������������ � �������� ��������.
        /// 
        /// </summary>
        public ICommand switchToSettingsCommand
        {
            get
            {
                return _switchToSettingsCommand;
            }
            set
            {
                if (_switchToSettingsCommand == value)
                {
                    return;
                }

                _switchToSettingsCommand = value;
                RaisePropertyChanged("switchToSettings");
            }
        }

        private ICommand _quitCommand;
        /// <summary>
        /// <para>
        /// �����: ������ ������
        /// </para>
        /// 
        /// ������� ��� ������ �� ���������.
        /// 
        /// </summary>
        public ICommand quitCommand
        {
            get
            {
                return _quitCommand;
            }
            set
            {
                if(_quitCommand == value)
                {
                    return;
                }

                _quitCommand = value;
                RaisePropertyChanged("quitCommand");
            }
        }

        #endregion

        private IPageViewModel calculationVM;
        private IPageViewModel settingsVM;

        /// <summary>
        /// <para>
        /// �����: ������ ������
        /// </para>
        /// </summary>
        /// <param name="calculationVM">ViewModel ��� �������� ��������</param>
        /// <param name="settingsVM">ViewModel ��� �������� ��������</param>
        public MainViewModel(IPageViewModel calculationVM, IPageViewModel settingsVM)
        {
            if (calculationVM == null || settingsVM == null)
                throw new ArgumentNullException();

            this.calculationVM = calculationVM;
            this.settingsVM = settingsVM;

            switchToCalculationCommand = new Command(() => currentViewModel = calculationVM);
            switchToSettingsCommand = new Command(() => currentViewModel = settingsVM);
            quitCommand = new Command(() => App.Current.Shutdown(0));

            currentViewModel = calculationVM;
        }
    }
}