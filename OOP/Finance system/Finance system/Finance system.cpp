#include <iostream>
#include "Card.h"
#include <string.h>
#include<regex>
#include<vector>
#include <fstream> 
#include <sstream>

using namespace std;


int main() {


	
	int choice = 1;
	uint16_t this_month{};
	uint16_t this_year{};
	uint16_t this_expenses{};

	uint16_t top_month{};
	uint16_t top_expenses{};

#pragma region File Variables

	ofstream file("data.txt", ios::app); // There will be data in the file when we finish the program!

#pragma endregion


#pragma region Card Variables
	
	uint16_t size{};
	uint16_t capacity = 2;
	VGA::Card* all_card = new VGA::Card[capacity];

#pragma endregion


#pragma region Wallet Variables
	
	uint16_t wallet_size{};
	uint16_t wallet_capacity = 2;
	VGA::Wallet* all_wallet = new VGA::Wallet[capacity];

#pragma endregion


#pragma region Expneses Variables

	uint16_t expenses_size{};
	uint16_t expenses_capacity = 2;
	VGA::Expenses* all_epxenses = new VGA::Expenses[capacity];

#pragma endregion

	while (choice != 0)
	{
		cout << "0.End\n1.Add card\n2.Show all cards\n3.Delete card\n4.Add wallet\n5.Show all wallets\n6.Delete wallet\n7.Top up the card \n8.Top up the wallet\n9.Add expenses\n10.Make expenses rating  ";
		cout << "\nEnter choose : ";
		cin >> choice;
		system("cls");
		switch (choice)
		{
		case 0:
		{
			cout << "End of programm!";
			break;
		}

		case 1:
		{
			if (size == capacity)
			{
				VGA::Card* tmp = new VGA::Card[capacity * 2];
				for (size_t i = 0; i < size; i++)
				{
					tmp[i] = all_card[i];
				}
				delete[] all_card;
				all_card = tmp;
			}
			cin >> all_card[size];
			size++;
			break;
		}

		case 2:
		{
			VGA::show_all_card(size, all_card);
			break;
		}

		case 3:
		{
			if (size == 0)
			{
				cout << "Error:You have no cards!\n";
				break;
			}
			uint16_t delete_index{};
			VGA::show_all_card(size, all_card);
			cout << "Choose index for delete : ";
			cin >> delete_index;

			VGA::Card* tmp = new VGA::Card[capacity * 2];
			for (size_t i = 0; i < size; i++)
			{
				if (i != delete_index) {
					tmp[i] = all_card[i];
				}
				if (i == delete_index && i + 1 < size)
				{
					tmp[i] = all_card[i + 1];
				}

			}
			delete[] all_card;
			all_card = tmp;
			size--;
			break;
		}

		case 4:
		{
			if (wallet_size == wallet_capacity)
			{
				VGA::Wallet* wallet_tmp = new VGA::Wallet[wallet_capacity * 2];
				for (size_t i = 0; i < size; i++)
				{
					wallet_tmp[i] = all_wallet[i];
				}
				delete[] all_wallet;
				all_wallet = wallet_tmp;
			}
			cin >> all_wallet[wallet_size];
			wallet_size++;
			break;
		}

		case 5:
		{
			if (size == 0)
			{
				cout << "Error:You have no wallets!\n";
				break;
			}
			VGA::show_all_wallet(wallet_size, all_wallet);
			break;
		}

		case 6:
		{
			if (size == 0)
			{
				cout << "Error:You have no wallets!\n";
				break;
			}
			int delete_index{};
			VGA::show_all_wallet(wallet_size, all_wallet);
			cout << "Choose index for delete : ";
			cin >> delete_index;

			VGA::Wallet* tmp = new VGA::Wallet[capacity * 2];
			for (size_t i = 0; i < size; i++)
			{
				if (i != delete_index) {
					tmp[i] = all_wallet[i];
				}
				if (i == delete_index && i + 1 < wallet_size)
				{
					tmp[i] = all_wallet[i + 1];
				}

			}
			delete[] all_wallet;
			all_wallet = tmp;
			wallet_size--;
			break;
		}

		case 7:
		{
			if (size == 0)
			{
				cout << "Error:You have no cards!\n";
				break;
			}
			int add_money{};
			int choice_add{};
			cout << "Enter the amount you want to top up : ";
			cin >> add_money;
			cout << endl;
			VGA::show_all_card(size, all_card);
			cout << "Enter choose : ";
			cin >> choice_add;
			all_card[choice_add].top_up_the_card(add_money);
			break;
		}

		case 8:
		{
			if (wallet_size == 0)
			{
				cout << "Error:You have no wallets!\n";
				break;
			}
			int add_money{};
			int choice_add{};
			cout << "Enter the amount you want to top up : ";
			cin >> add_money;
			cout << endl;
			VGA::show_all_wallet(wallet_size, all_wallet);
			cout << "Enter choose : ";
			cin >> choice_add;
			all_wallet[choice_add].top_up_the_wallet(add_money);
			break;
		}

		case 9:
		{
			if (size == 0 && wallet_size == 0)
			{
				cout << "Error:You have no any payment method !\n";
				break;
			}
			uint16_t tmp_expenses{};
			uint16_t payment_method{};
			uint16_t choice_card{};
			uint16_t choice_wallet{};

			while (payment_method <= 0 || payment_method > 2) {
				cout << "Choose payment method \n1.Card\n2.Wallet \n:";
				cin >> payment_method;
			}
			system("cls");

			switch (payment_method)
			{
			case 1:
			{
				if (size == 0)
				{
					cout << "Error:You have no card!\n";
					break;
				}
				VGA::show_all_card(size, all_card);
				while (choice_card > size || choice_card == 0)
				{
					cout << "Choose card : ";
					cin >> choice_card;
				}
				system("cls");
				cout << "Enter money to spend : ";
				cin >> tmp_expenses;
				if (all_card[choice_card] >= tmp_expenses && all_card[choice_card] == tmp_expenses)
				{
					system("cls");
					all_card[choice_card].spent(tmp_expenses);
					if (expenses_size == expenses_capacity)
					{
						VGA::Expenses* tmp_a = new VGA::Expenses[expenses_capacity * 2];
						for (size_t i = 0; i < size; i++)
						{
							tmp_a[i] = all_epxenses[i];
						}

						delete[] all_epxenses;
						all_epxenses = tmp_a;
					}
					cin >> all_epxenses[expenses_size];
					all_epxenses[expenses_size].add_expenses(tmp_expenses);
					expenses_size++;
				
				}
				else
				{
					cout << "Error:There are not enough funds on the balance sheet!\n";
					break;
				}
				break;

				system("cls");
			}

			case 2:
			{
				if (wallet_size == 0)
				{
					cout << "Error:You have no wallet!\n";
					break;
				}
				VGA::show_all_wallet(wallet_size, all_wallet);
				while (choice_wallet > wallet_size || choice_wallet < wallet_size)
				{
					cout << "Choose wallet : ";
					cin >> choice_wallet;
				}
				system("cls");
				cout << "Enter money to spend : ";
				cin >> tmp_expenses;
				if (all_wallet[choice_wallet] >= tmp_expenses && all_wallet[choice_wallet] == tmp_expenses)
				{
					system("cls");
					all_wallet[choice_wallet].spent(tmp_expenses);
					if (expenses_size == expenses_capacity)
					{
						VGA::Expenses* tmp_a = new VGA::Expenses[expenses_capacity * 2];
						for (size_t i = 0; i < expenses_size; i++)
						{
							tmp_a[i] = all_epxenses[i];
						}

						delete[] all_epxenses;
						all_epxenses = tmp_a;
					}
					cin >> all_epxenses[expenses_size];
					all_epxenses[expenses_size].add_expenses(tmp_expenses);
					expenses_size++;
				}
				else
				{
					cout << "Error:There are not enough funds on the balance sheet!\n";
					break;
				}

				system("cls");
			}
			break;
			}
			;
			break;
		}

		
		case 10:
		{

			if (expenses_size == 0)
			{
				cout << "Error:You have no expenses !\n";
				break;
			}
			int choice_type_of_rating{};
			cout << "Choose type :\n1.Top categories\n2.Top expenses\n:";
			cin >> choice_type_of_rating;
			
			if (choice_type_of_rating == 1)
			{
				cout << "Choose top \n1.Top day\n2.Top month\n:";
				int choice_rating{};
				cin >> choice_rating;
				switch (choice_rating)
				{
				case 1:
				{
					system("cls");
					int month{};
					int year{};
					int day{};

					cout << "Enter year : ";
					cin >> year;
					cout << "\nEnter month : ";
					cin >> month;
					cout << "Enter day : ";
					cin >> day;
					VGA::top_3_category_day(all_epxenses, expenses_size, month, year, day);
					break;
				}

				case 2:
				{
					system("cls");
					int month{};
					int year{};
					
					cout << "Enter year : ";
					cin >> year;
					cout << "\nEnter month : ";
					cin >> month;
					VGA::top_3_category_month(all_epxenses, expenses_size,month,year);

					cout << endl;
					break;
				}
				}
			}




			if (choice_type_of_rating == 2) {
				cout << "Choose top \n1.Top day\n2.Top month\n:";
				int choice_rating{};
				cin >> choice_rating;
				switch (choice_rating)
				{
				case 1:
				{
					VGA::top_3_day(all_epxenses, expenses_size);
					cout << endl;
					break;
				}

				case 2:
				{
					VGA::top_3_month(all_epxenses, expenses_size);
					
					cout << endl;
					break;
				}
				}
			}
		}

		}

	}


	if (file.is_open()) {
		for (size_t i = 0; i < expenses_size; i++)
		{
			file << all_epxenses[i] << "\n\n";
		}
	}

	system("cls");

	
	return 0;
}