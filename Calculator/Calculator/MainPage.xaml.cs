using Calculator.Operations;
using Calculator.Operations.Unique;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        private enum State { FirstNumber, SecondNumber, SelectedOperator };

        private List<Operation> operations = new List<Operation>();
        private List<OperationUnique> operationsUnique = new List<OperationUnique>();

        private State currentState = State.FirstNumber;
        private string selectedOperator;
        private double firstNumber, secondNumber;

        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null);
            addOperation();
            addOperationUnique();
        }

        private void OnSelectNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            selectNumberAlterResultText(button);
            setNumber();
        }

        private void OnSelectOperatorUnique(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            double result = 0;

            foreach (OperationUnique operationUnique in operationsUnique)
            {
                if (operationUnique.verifyOperation(button.Text))
                {
                    result = operationUnique.action(Convert.ToDouble(resultText.Text));
                    break;
                }
            }
            setResultText(result);
        }

        private void OnSelectOperator(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            selectedOperator = button.Text;
            currentState = State.SelectedOperator;
        }

        private void OnCalculate(object sender, EventArgs e)
        {
            double result = 0;

            foreach (Operation operation in operations)
            {
                if (operation.verifyOperation(selectedOperator))
                {
                    result = operation.action(firstNumber, secondNumber);
                    break;
                }
            }
            setResultText(result);
        }

        private void OnClear(object sender, EventArgs e)
        {
            firstNumber = 0;
            secondNumber = 0;
            currentState = State.FirstNumber;
            resultText.Text = "";
        }

        private void selectNumberAlterResultText(Button button)
        {
            if (currentState.Equals(State.SelectedOperator))
            {
                resultText.Text = "";
                currentState = State.SecondNumber;
            }
            resultText.Text += button.Text;
        }

        private void setNumber()
        {
            if (currentState.Equals(State.FirstNumber))
                firstNumber = Convert.ToDouble(resultText.Text);
            else
                secondNumber = Convert.ToDouble(resultText.Text);
        }

        private void setResultText(double result)
        {
            resultText.Text = result.ToString();
            firstNumber = result;
            currentState = State.FirstNumber;
        }

        private void addOperation()
        {
            Addition addition = new Addition();
            Subtraction subtraction = new Subtraction();
            Multiplication multiplication = new Multiplication();
            Division division = new Division();

            operations.Add(addition);
            operations.Add(subtraction);
            operations.Add(multiplication);
            operations.Add(division);
        }

        private void addOperationUnique()
        {
            Percentage percentage = new Percentage();
            Root root = new Root();
            Square square = new Square();

            operationsUnique.Add(percentage);
            operationsUnique.Add(root);
            operationsUnique.Add(square);
        }
    }
}
