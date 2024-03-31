#include <iostream>
using namespace std;

int brackets(size_t& i, char*& n1)
{
	if (i == 0)
	{
		i++;;
	}
	else
	{
		i += 2;
	}

	bool bracket = false;
	int number{};
	int degree{};
	int bracketNumber{};

	for (; n1[i] != ')'; i++)
	{
		if (int(n1[i]) >= 48 && int(n1[i] <= 57)) {
			degree = 10;
			number = int(n1[i] - 48);
			i++;
			for (; int(n1[i]) >= 48 && int(n1[i] <= 57); i++)
			{
				number *= degree;
				number += int(n1[i] - 48);

			}
		}

		if (n1[i] == '+')
		{
			if (int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 1] - 48);
				i++;
				for (; int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 1] - 48);

				}
				number += tmpNum;
				tmpNum = 0;
			}

			if (n1[i + 1] == '(')
			{
				bracketNumber = brackets(i, n1);
				number += bracketNumber;
			}

		}

		if (n1[i] == '-')
		{
			if (int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 1] - 48);
				i++;
				for (; int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 1] - 48);

				}
				number -= tmpNum;
				tmpNum = 0;
			}

			if (n1[i + 1] == '(')
			{
				bracketNumber = brackets(i, n1);
				number -= bracketNumber;
			}

		}

		if (n1[i] == '*')
		{
			if (int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 1] - 48);
				i++;
				for (; int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 1] - 48);

				}
				number *= tmpNum;
				tmpNum = 0;
			}

			if (n1[i + 1] == '(')
			{
				bracketNumber = brackets(i, n1);
				number *= bracketNumber;
			}

		}

		if (n1[i] == '/')
		{
			if (int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 1] - 48);
				i++;
				for (; int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 1] - 48);

				}
				number /= tmpNum;
				tmpNum = 0;
			}

			if (n1[i + 1] == '(')
			{
				bracketNumber = brackets(i, n1);
				number /= bracketNumber;

			}

		}

		if (n1[i] == '(')
		{
			bracketNumber = brackets(i, n1);
			number += bracketNumber;
		}

		if (n1[i] == ')' && n1[i + 1] == '+')
		{
			if (int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 2] - 48);
				i++;
				for (; int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 2] - 48);

				}
				number += tmpNum;
				tmpNum = 0;
				i += 2;
			}

			if (n1[i] == '(')
			{
				bracketNumber = brackets(i, n1);
				number += bracketNumber;
			}
		}

		if (n1[i] == ')' && n1[i + 1] == '-')
		{
			if (int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 2] - 48);
				i++;
				for (; int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 2] - 48);

				}
				number -= tmpNum;
				tmpNum = 0;
				i += 2;
			}

			if (n1[i] == '(')
			{
				bracketNumber = brackets(i, n1);
				number -= bracketNumber;
			}
		}

		if (n1[i] == ')' && n1[i + 1] == '*')
		{
			if (int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 2] - 48);
				i++;
				for (; int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 2] - 48);

				}
				number *= tmpNum;
				tmpNum = 0;
				i += 2;
			}

			if (n1[i] == '(')
			{
				bracketNumber = brackets(i, n1);
				number *= bracketNumber;
			}
		}

		if (n1[i] == ')' && n1[i + 1] == '/')
		{
			if (int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 2] - 48);
				i++;
				for (; int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 2] - 48);

				}
				number /= tmpNum;
				tmpNum = 0;
				i += 2;
			}

			if (n1[i] == '(')
			{
				bracketNumber = brackets(i, n1);
				number /= bracketNumber;
			}
		}
	}

	return number;
}


int main()
{
	char* n1 = new char [50] {};
	int number{};
	int len{};
	int finalNumber{};
	bool bracket = false;
	int bracketNumber{};
	int degree = 10;


	cout << "Enter number : ";
	cin >> n1;


	while (n1[len] != '\0')
	{
		len++;
	}


	for (size_t i = 0; i < len; i++)
	{
		if (int(n1[i]) >= 48 && int(n1[i] <= 57)) {
			degree = 10;
			number = int(n1[i] - 48);
			i++;
			for (; int(n1[i]) >= 48 && int(n1[i] <= 57); i++)
			{
				number *= degree;
				number += int(n1[i] - 48);

			}
		}

		if (n1[i] == '+')
		{
			if (int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 1] - 48);
				i++;
				for (; int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57); i++)
				{
					 tmpNum *= degree;
					 tmpNum += int(n1[i + 1] - 48);

				}
				number += tmpNum;
				tmpNum = 0;
			}

			if (n1[i + 1] == '(')
			{
				bracketNumber = brackets(i, n1);
				number += bracketNumber;
			}

		}

		if (n1[i] == '-')
		{
			if (int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 1] - 48);
				i++;
				for (; int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 1] - 48);

				}
				number -= tmpNum;
				tmpNum = 0;
			}

			if (n1[i + 1] == '(')
			{
				bracketNumber = brackets(i, n1);
				number -= bracketNumber;
			}

		}

		if (n1[i] == '*')
		{
			if (int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 1] - 48);
				i++;
				for (; int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 1] - 48);

				}
				number *= tmpNum;
				tmpNum = 0;
			}

			if (n1[i + 1] == '(')
			{
				bracketNumber = brackets(i, n1);
				number *= bracketNumber;
			}

		}

		if (n1[i] == '/')
		{
			if (int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 1] - 48);
				i++;
				for (; int(n1[i + 1]) >= 48 && int(n1[i + 1] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 1] - 48);

				}
				number /= tmpNum;
				tmpNum = 0;
			}

			if (n1[i + 1] == '(')
			{
				bracketNumber = brackets(i, n1);
				number /= bracketNumber;

			}

		}

		if (n1[i] == '(')
		{
			bracketNumber = brackets(i, n1);
			number += bracketNumber;
		}

		if (n1[i] == ')' && n1[i + 1] == '+')
		{
			if (int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 2] - 48);
				i++;
				for (; int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 2] - 48);

				}
				number += tmpNum;
				tmpNum = 0;
				i += 2;
			}

			if (n1[i] == '(')
			{
				bracketNumber = brackets(i, n1);
				number += bracketNumber;
			}
		}

		if (n1[i] == ')' && n1[i + 1] == '-')
		{
			if (int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 2] - 48);
				i++;
				for (; int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 2] - 48);

				}
				number -= tmpNum;
				tmpNum = 0;
				i += 2;
			}

			if (n1[i] == '(')
			{
				bracketNumber = brackets(i, n1);
				number -= bracketNumber;
			}
		}

		if (n1[i] == ')' && n1[i + 1] == '*')
		{
			if (int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 2] - 48);
				i++;
				for (; int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 2] - 48);

				}
				number *= tmpNum;
				tmpNum = 0;
				i += 2;
			}

			if (n1[i] == '(')
			{
				bracketNumber = brackets(i, n1);
				number *= bracketNumber;
			}
		}

		if (n1[i] == ')' && n1[i + 1] == '/')
		{
			if (int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57)) {
				degree = 10;
				int tmpNum{};
				tmpNum = int(n1[i + 2] - 48);
				i++;
				for (; int(n1[i + 2]) >= 48 && int(n1[i + 2] <= 57); i++)
				{
					tmpNum *= degree;
					tmpNum += int(n1[i + 2] - 48);

				}
				number /= tmpNum;
				tmpNum = 0;
				i += 2;
			}

			if (n1[i] == '(')
			{
				bracketNumber = brackets(i, n1);
				number /= bracketNumber;
			}
		}
	}

	cout << number;
	return 0;
}