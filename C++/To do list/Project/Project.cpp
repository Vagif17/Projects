#include <iostream>
using namespace std;

int bracketsIntBractets(int ind, char* arif)
{
	int TmpNumber{};
	for (size_t i = ind; arif[i] != ')'; i++)
	{
		if (int(arif[i] >= 48) && int(arif[i] <= 57))
		{
			TmpNumber = int(arif[i] - 48);
		}

		if (arif[i] == '+')
		{
			TmpNumber += int(arif[i + 1] - 48);
			i++;
		}

		if (arif[i] == '-')
		{
			TmpNumber -= int(arif[i + 1] - 48);
			i++;
		}

		if (arif[i] == '/')
		{
			TmpNumber /= int(arif[i + 1] - 48);
			i++;
		}

		if (arif[i] == '*')
		{
			TmpNumber *= int(arif[i + 1] - 48);
			i++;
		}
	}
	return TmpNumber;
}

int main()
{
	char* n1 = new char [50] {};
	int number{};
	int len{};
	int	brackets{};
	int bracketsNow{};


	cout << "Enter number : ";
	cin >> n1;


	while (n1[len] != '\0')
	{
		len++;
	}

	for (size_t i = 0; i < len; i++)
	{
		if (n1[i] == '(')
		{
			brackets++;
		}
	}

	int tmpInd = -1;
	int finalNumber{};
	for (size_t i = 0; i < len; i++)
	{
		if (n1[i] == '(')
		{
			if (i == tmpInd)
			{
				break;
			}
			for (size_t b = i + 1; n1[b] != ')'; b++)
			{
				if (b == tmpInd)
				{
					break;
				}
				if (int(n1[b] >= 48) && int(n1[b] <= 57))
				{
					number = int(n1[b] - 48);
				}

				if (n1[b] == '+')
				{
					if (n1[b + 1] == '(')
					{
						number += bracketsIntBractets(b + 1, n1);
						b++;
						tmpInd = b + 1;
						bracketsNow++;

					}
					else
					{
						number += int(n1[b + 1] - 48);
						b++;
					}
				}

				if (n1[b] == '-')
				{
					if (n1[b + 1] == '(')
					{
						number -= bracketsIntBractets(b + 1, n1);
						b++;
						tmpInd = b + 1;
						bracketsNow++;

					}
					else
					{
						number -= int(n1[b + 1] - 48);
						b++;
					}

				}

				if (n1[b] == '/')
				{
					number /= int(n1[b + 1] - 48);
					b++;
					if (n1[b + 1] == '(')
					{
						number /= bracketsIntBractets(b + 1, n1);
						b++;
						tmpInd = b + 1;
						bracketsNow++;

					}
					else
					{
						number /= int(n1[b + 1] - 48);
						b++;
					}
				}

				if (n1[b] == '*')
				{
					if (n1[b + 1] == '(')
					{
						number *= bracketsIntBractets(b + 1, n1);
						b++;
						tmpInd = b + 1;
						bracketsNow++;
					}
					else
					{
						number *= int(n1[b + 1] - 48);
						b++;
					}
				}

			}
		}

	}

	cout << number;
	return 0;
}