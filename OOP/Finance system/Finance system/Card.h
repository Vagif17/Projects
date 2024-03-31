#pragma once
#include<string.h>
#include<iostream>
#include<regex>
#include<vector>
#include <fstream> 
#include <sstream>


#pragma region Categories

enum categories
{
	GAMES = 1,
	CLOTHES,
	PRODUCTS,
	REAL_STATE
};

#pragma endregion


using namespace std;
namespace VGA {


#pragma region Card
	class Card {
	private:
		string card_number{};
		uint16_t money{};
		string name_of_owner{};
		string surname_of_owner{};
		string service_life{}; //Date
		string CVV{};
	public:
		Card();

		Card(string card_number, uint16_t money, string name_of_owner, string surname_of_owner, string service_live, string CVV);

		friend ostream& operator <<(ostream& os, const Card& other)
		{
			os << "------------------------------\n";
			for (size_t i = 0; i < 16; i++)
			{
				if (i % 4 == 0)
				{
					os << " ";
				}
				os << other.card_number[i];
			}

			os << endl << other.surname_of_owner << " " << other.name_of_owner << "\t\tCVV:" << "\n\t\t\t" << other.CVV;
			os << "\n\t" << other.service_life;
			os << "\n------------------------------\n Money : " << other.money;
			return os;
		};

		friend istream& operator >>(istream& is, Card& other)
		{
			regex regex_card_number("^[0-9]{16}");
			regex regex_CVV("^[0-9]{3}");
			regex regex_service_life("^([0-9]{2})/([0-9]{2})");

			cout << "Enter card number : ";
			is >> other.card_number;
			while (!regex_match(other.card_number, regex_card_number))
			{
				cout << "Error:Card must have 16 digits!\nEnter right card number :";
				is >> other.card_number;
				system("cls");
			}
			system("cls");


			cout << "Enter CVV : ";
			is >> other.CVV;
			while (!regex_match(other.CVV, regex_CVV))
			{
				cout << "Error:CVV must have 3 digits!\nEnter right CVV : ";
				is >> other.CVV;
				system("cls");
			}
			system("cls");

			cout << "Enter money : ";
			is >> other.money;
			system("cls");

			cout << "Enter name : ";
			is >> other.name_of_owner;
			system("cls");

			cout << "Enter surname : ";
			is >> other.surname_of_owner;
			system("cls");

			cout << "Enter service life : ";
			is >> other.service_life;
			while (!regex_match(other.service_life, regex_service_life))
			{
				cout << "\nError: example of service live - 12(month)/12(year)\nEnter right service life : ";
				is >> other.service_life;
				system("cls");
			}
			system("cls");

			return is;
		}

		bool operator >=(uint16_t spent_money);
		void operator -(uint16_t spent_money);
		void top_up_the_card(uint16_t add_money);
		void spent(uint16_t spent_money);
		bool operator==(uint16_t spent_money);


	};


#pragma endregion



#pragma region Wallet

	class Wallet {
	private:
		uint16_t cash{};
		uint16_t max_cash = 10000;
	public:
		Wallet();

		Wallet(uint16_t cash);

		friend ostream& operator << (ostream& os, const Wallet& other)
		{
			os << "Cash in wallet : " << other.cash;
			return os;
		}


		friend istream& operator >> (istream& is, Wallet& other)
		{
			cout << "Enter cash in wallet : ";
			is >> other.cash;
			if (other.cash > other.max_cash)
			{
				cout << "Error:Wallet maximum capacity is 10000\nEnter cash in wallet : ";
				is >> other.cash;
			}
			system("cls");

			return is;
		}

		void top_up_the_wallet(int num);
		bool operator >=(uint16_t spent_money);
		void spent(uint16_t spent_money);
		bool operator ==(uint16_t spent_money);

	};


#pragma endregion 



#pragma region Expenses

	class Expenses {
	private:
		uint16_t day{};
		uint16_t month{};
		uint16_t year{};
		uint16_t expenses_amount{};
		uint16_t category{};
	public:
		Expenses();

		Expenses(uint16_t day, uint16_t month, uint16_t year, uint16_t expenses_amount, uint16_t category);

		void add_expenses(uint16_t spent_money);
		int get_month();
		int get_year();
		int get_day();
		int get_expenses();
		int get_category();

		friend ostream& operator <<(ostream& os, const Expenses& other)
		{
			os << "Date :" << other.day << "." << other.month << "." << other.year;
			os << "\nCategory : ";
			switch (other.category)
			{
			case 1:
			{
				os << "Games";
				break;
			}

			case 2:
			{
				os << "Clothes";
				break;
			}

			case 3:
			{
				os << "Products";
				break;
			}

			case 4:
			{
				os << "Real State";
				break;
			}
			}
			os << "\nAmount : " << other.expenses_amount;
			return os;
		}

		friend istream& operator >>(istream& in, Expenses& other)
		{
			cout << "Enter day : ";
			in >> other.day;
			system("cls");

			cout << "Enter month : ";
			in >> other.month;
			system("cls");

			cout << "Enter year : ";
			in >> other.year;
			system("cls");

			cout << "Choose category : \n1.Games\n2.Clothes\n3.Products\n4.Real State\n:";
			in >> other.category;
			return in;
		}

	};


#pragma endregion



#pragma region Non-Class Function

void top_3_day(Expenses* all_epxenses,int expenses_size);
void top_3_month(Expenses* p1, int expenses_size);
void top_3_category_month(Expenses* p1, uint16_t expenses_size, uint16_t month, uint16_t year);
void top_3_category_day(Expenses* p1, uint16_t expenses_size, uint16_t month, uint16_t year, uint16_t day);
void show_all_card(uint16_t size, VGA::Card* all_card);
void show_all_wallet(uint16_t wallet_size, VGA::Wallet* all_wallet);

#pragma endregion

}