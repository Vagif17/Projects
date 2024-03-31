#pragma once
#include "Card.h"
#include <string.h>
#include<regex>
#include<vector>
#include <fstream> 
#include <sstream>

using namespace std;
using namespace VGA;


#pragma region Card
Card::Card() = default;


Card::Card(string card_number, uint16_t money, string name_of_owner, string surname_of_owner, string service_live, string CVV)
{
	this->card_number = card_number;
	this->money = money;
	this->name_of_owner = name_of_owner;
	this->surname_of_owner = surname_of_owner;
	this->service_life = service_live;
	this->CVV = CVV;

};



void Card::top_up_the_card(uint16_t add_money) {
	this->money += add_money;
}

bool Card::operator >=(uint16_t spent_money)
{
	return this->money > spent_money;
}

void Card::spent(uint16_t spent_money) {
	this->money -= spent_money;
}

bool Card::operator==(uint16_t spent_money)
{
	return this->money > spent_money;
}
#pragma endregion



#pragma region Wallet

Wallet::Wallet() = default;

Wallet::Wallet(uint16_t cash)
{
	this->cash = cash;
}

void Wallet::top_up_the_wallet(int num) {
	this->cash += cash;
}

bool Wallet::operator >=(uint16_t spent_money)
{
	return this->cash > spent_money;
}

void Wallet::spent(uint16_t spent_money) {
	this->cash -= spent_money;
}

bool Wallet::operator==(uint16_t spent_money)
{
	return this->cash > spent_money;
}
#pragma endregion



#pragma region Expenses
Expenses::Expenses() = default;

Expenses::Expenses(uint16_t day, uint16_t month, uint16_t year, uint16_t expenses_amount,uint16_t category)
{
	this->day = day;
	this->month = month;
	this->year = year;
	this->expenses_amount = expenses_amount;
	
	switch (category) {
		case 1: 
		{
			this->category = GAMES;
			break;
		}

		case 2:
		{
			this->category = CLOTHES;
			break;
		}

		case 3:
		{
			this->category = PRODUCTS;
			break;
		}

		case 4:
		{
			this->category = REAL_STATE;
			break;
		}

	}
}

void Expenses::add_expenses(uint16_t spent_money)
{
	this->expenses_amount = spent_money;
}

int Expenses::get_month()
{
	return this->month;
}

int Expenses::get_year()
{
	return this->year;
}

int Expenses::get_day()
{
	return this->day;
}

int Expenses::get_expenses()
{
	return this->expenses_amount;
}

int Expenses::get_category()
{
	return this->category;
}
#pragma endregion



#pragma region Non-Class Funciton

void VGA::top_3_day(VGA::Expenses* p1, int expenses_size)
{
	int index_top1 = -1;
	int index_top2 = -2;
	int index_top3 = -3;

	uint16_t top_expense{};
	uint16_t top_month{};
	uint16_t top_year{};


	for (size_t a = 0; a < 3; a++)
	{

		if (a == 0) {
			for (size_t i = 1; i < 13; i++)
			{
				uint16_t this_expenses{};
				uint16_t this_month{};
				uint16_t this_year{};
				for (size_t b = 0; b < expenses_size; b++)
				{
					if (this_month == 0) {
						if (i == p1[b].get_month())
						{
							this_month = p1[b].get_month();
							this_expenses = p1[b].get_expenses();
							this_year = p1[b].get_year();
						}
					}

					else
					{
						if (i == p1[b].get_month() && this_year == p1[b].get_year() && this_expenses < p1[b].get_expenses() && b != index_top1)
						{
							this_expenses = p1[b].get_expenses();
						}

						if (i == p1[b].get_month() && this_year != p1[b].get_year() && this_expenses < p1[b].get_expenses())
						{
							this_expenses = p1[b].get_expenses();
						}
					}

					if (this_expenses > top_expense)
					{
						top_expense = this_expenses;
						top_month = this_month;
						top_year = this_year;
						index_top1 = b;
					}
				}
			}
		}


		if (a == 1)
		{
			for (size_t i = 1; i < 13; i++)
			{
				uint16_t this_expenses{};
				uint16_t this_month{};
				uint16_t this_year{};
				uint16_t top_expense{};
				for (size_t b = 0; b < expenses_size; b++)
				{
					if (this_month == 0) {
						if (i == p1[b].get_month() && b != index_top1)
						{
							this_month = p1[b].get_month();
							this_expenses = p1[b].get_expenses();
							this_year = p1[b].get_year();
						}
					}

					else
					{
						if (i == p1[b].get_month() && this_year == p1[b].get_year() && this_expenses < p1[b].get_expenses() && b != index_top1)
						{
							this_expenses = p1[b].get_expenses();
						}

						if (i == p1[b].get_month() && this_year != p1[b].get_year() && this_expenses < p1[b].get_expenses())
						{
							this_expenses = p1[b].get_expenses();
						}
					}

					if (this_expenses > top_expense)
					{
						top_expense = this_expenses;
						top_month = this_month;
						top_year = this_year;
						index_top2 = b;
					}
				}

			}
		}

		if (a == 2)
		{
			for (size_t i = 1; i < 13; i++)
			{
				uint16_t this_expenses{};
				uint16_t this_month{};
				uint16_t this_year{};
				uint16_t top_expense{};
				for (size_t b = 0; b < expenses_size; b++)
				{
					if (this_month == 0) {
						if (i == p1[b].get_month() && b != index_top1 && b != index_top2)
						{
							this_month = p1[b].get_month();
							this_expenses = p1[b].get_expenses();
							this_year = p1[b].get_year();
						}
					}

					else
					{
						if (i == p1[b].get_month() && this_year == p1[b].get_year() && this_expenses < p1[b].get_expenses() && b != index_top1 && b != index_top2)
						{
							this_expenses = p1[b].get_expenses();
						}

						if (i == p1[b].get_month() && this_year != p1[b].get_year() && this_expenses < p1[b].get_expenses())
						{
							this_expenses = p1[b].get_expenses();
						}
					}

					if (this_expenses > top_expense)
					{
						top_expense = this_expenses;
						top_month = this_month;
						top_year = this_year;
						index_top3 = b;
					}
				}

			}
		}
	}

	cout << p1[index_top1] << "\n\n" << p1[index_top2] << "\n\n" << p1[index_top3];
}



void VGA::top_3_month(Expenses* p1, int expenses_size)
{
	int index_top1 = -1;
	int index_top2 = -2;
	int index_top3 = -3;

	uint16_t top_expense{};
	uint16_t top_month{};
	uint16_t top_year{};

	uint16_t top2_expense{};
	uint16_t top2_month{};
	uint16_t top2_year{};

	uint16_t top3_expense{};
	uint16_t top3_year{};
	uint16_t top3_month{};


	for (size_t a = 0; a < 3; a++)
	{

		if (a == 0) {
			for (size_t i = 1; i < 13; i++)
			{
				uint16_t this_expenses{};
				uint16_t this_month{};
				uint16_t this_year{};
				for (size_t b = 0; b < expenses_size; b++)
				{
					if (this_month == 0) {
						if (i == p1[b].get_month())
						{
							this_month = p1[b].get_month();
							this_expenses = p1[b].get_expenses();
							this_year = p1[b].get_year();
						}
					}

					else
					{
						if (i == p1[b].get_month() && this_year == p1[b].get_year() && b != index_top1)
						{
							this_expenses += p1[b].get_expenses();
						}

						if (i == p1[b].get_month() && this_year != p1[b].get_year() && this_expenses < p1[b].get_expenses() && b != index_top1)
						{
							this_expenses = p1[b].get_expenses();
						}
					}

					if (this_expenses > top_expense)
					{
						top_expense = this_expenses;
						top_month = this_month;
						top_year = this_year;
						index_top1 = b;
					}
				}
			}
		}


		if (a == 1)
		{
			for (size_t i = 1; i < 13; i++)
			{
				uint16_t this_expenses{};
				uint16_t this_month{};
				uint16_t this_year{};
				uint16_t top_expense{};
				for (size_t b = 0; b < expenses_size; b++)
				{
					if (this_month == 0) {
						if (i == p1[b].get_month() && b != index_top1)
						{
							this_month = p1[b].get_month();
							this_expenses = p1[b].get_expenses();
							this_year = p1[b].get_year();
						}
					}

					else
					{
						if (i == p1[b].get_month() && this_year == p1[b].get_year() && b != index_top1 && b != index_top2)
						{
							this_expenses += p1[b].get_expenses();
						}

						if (i == p1[b].get_month() && this_year != p1[b].get_year() && this_expenses < p1[b].get_expenses() && b != index_top1 && b != index_top2)
						{
							this_expenses = p1[b].get_expenses();
						}
					}

					if (this_expenses > top2_expense)
					{
						top2_expense = this_expenses;
						top2_month = this_month;
						top2_year = this_year;
						index_top2 = b;
					}
				}

			}
		}

		if (a == 2)
		{
			for (size_t i = 1; i < 13; i++)
			{
				uint16_t this_expenses{};
				uint16_t this_month{};
				uint16_t this_year{};
				uint16_t top_expense{};
				for (size_t b = 0; b < expenses_size; b++)
				{
					if (this_month == 0) {
						if (i == p1[b].get_month() && b != index_top1 && b != index_top2)
						{
							this_month = p1[b].get_month();
							this_expenses = p1[b].get_expenses();
							this_year = p1[b].get_year();
						}
					}

					else
					{
						if (i == p1[b].get_month() && this_year == p1[b].get_year() && b != index_top1 && b != index_top2 && b != index_top3)
						{
							this_expenses += p1[b].get_expenses();
						}

						if (i == p1[b].get_month() && this_year != p1[b].get_year() && this_expenses < p1[b].get_expenses() && b != index_top1 && b != index_top2 && b != index_top3)
						{
							this_expenses = p1[b].get_expenses();
						}
					}

					if (this_expenses > top3_expense)
					{
						top3_expense = this_expenses;
						top3_month = this_month;
						top3_year = this_year;
						index_top3 = b;
					}
				}

			}
		}
	}

	cout << "Top 3 month :" << endl << top_month << " Month In " << top_year << " year.Expenses : " << top_expense << endl << top2_month << "  Month In " << top2_year << "  year.Expenses : " << top2_expense << endl << top3_month << "  Month In " << top3_year << " year.Expenses : " << top3_expense;
}


void VGA::show_all_card(uint16_t size, VGA::Card* all_card)
{
	if (size == 0)
	{
		cout << "Error:You have no cards!\n";
		return;
	}
	for (size_t i = 0; i < size; i++)
	{
		cout << "Card number : " << i << endl << all_card[i] << endl;
	}
	return;
}


void VGA::show_all_wallet(uint16_t wallet_size, VGA::Wallet* all_wallet)
{
	if (wallet_size == 0)
	{
		cout << "Error:You have no wallet!\n";
		return;
	}
	if (wallet_size <= 1) {
		cout << "First wallet :\n";
		for (size_t i = 0; i < wallet_size; i++)
		{
			cout << "Index : " << i << endl << all_wallet[i] << endl;
		}
	}
	else
	{
		for (size_t i = 0; i < wallet_size; i++)
		{
			cout << "Wallet : " << i << endl;
			cout << all_wallet[i] << endl;
		}
	}

}


void VGA::top_3_category_month(Expenses* p1, uint16_t expenses_size, uint16_t month, uint16_t year)
{

	uint16_t game_expense{};

	uint16_t clothes_expense{};

	uint16_t products_expense{};

	uint16_t real_state_expense{};

	for (size_t i = 0; i < expenses_size; i++)
	{
		if (p1[i].get_month() == month && p1[i].get_year() == year)
		{
			switch (p1[i].get_category())
			{
			case 1:
			{
				game_expense += p1[i].get_expenses();
				break;
			}

			case 2:
			{
				clothes_expense += p1[i].get_expenses();
				break;
			}

			case 3:
			{
				products_expense += p1[i].get_expenses();
				break;
			}

			case 4:
			{
				real_state_expense += p1[i].get_expenses();
				break;
			}
			}
		}
	}

	cout << "1.";
	if (game_expense > clothes_expense && game_expense > products_expense && game_expense > real_state_expense)
	{
		cout << "Games : " << game_expense << endl;
	}

	if (clothes_expense > game_expense && clothes_expense > products_expense && clothes_expense > real_state_expense)
	{
		cout << "Clothets : " << clothes_expense << endl;
	}

	if (products_expense > game_expense && products_expense > clothes_expense && clothes_expense > real_state_expense)
	{
		cout << "Product : " << products_expense << endl;
	}

	if (real_state_expense > game_expense && real_state_expense > clothes_expense && real_state_expense > products_expense)
	{
		cout << "Real State : " << real_state_expense << endl;
	}

	cout << "2.";

	if (game_expense > clothes_expense && game_expense > products_expense && game_expense < real_state_expense || game_expense > clothes_expense && game_expense < products_expense && game_expense > real_state_expense || game_expense < clothes_expense && game_expense > products_expense && game_expense > real_state_expense)
	{
		cout << "Game : " << game_expense;
	}

	if (clothes_expense > game_expense && clothes_expense > products_expense && clothes_expense < real_state_expense || clothes_expense > game_expense && clothes_expense < products_expense && clothes_expense > real_state_expense || game_expense > clothes_expense && clothes_expense > products_expense && clothes_expense > real_state_expense)
	{
		cout << "Clothes : " << clothes_expense;
	}

	if (products_expense > game_expense && products_expense > clothes_expense && products_expense < real_state_expense || products_expense > game_expense && products_expense < clothes_expense && products_expense > real_state_expense || products_expense < clothes_expense && products_expense > game_expense && products_expense > real_state_expense)
	{
		cout << "Products : " << products_expense;
	}

	if (real_state_expense > game_expense && real_state_expense > clothes_expense && real_state_expense < products_expense || real_state_expense > game_expense && real_state_expense < clothes_expense && real_state_expense > products_expense || real_state_expense < clothes_expense && real_state_expense > game_expense && real_state_expense > products_expense)
	{
		cout << "Real State : " << real_state_expense;
	}

	cout << "\n3.";

	if (game_expense < clothes_expense && game_expense < products_expense && game_expense < real_state_expense)
	{
		cout << "Game : " << game_expense;
	}

	if (clothes_expense < game_expense && clothes_expense < products_expense && clothes_expense < real_state_expense)
	{
		cout << "Clothes : " << clothes_expense;
	}

	if (products_expense < game_expense && products_expense < clothes_expense && products_expense < real_state_expense)
	{
		cout << "Products : " << products_expense;
	}

	if (real_state_expense < game_expense && real_state_expense < clothes_expense && real_state_expense < products_expense)
	{
		cout << "Real State : " << real_state_expense;
	}

}



void VGA::top_3_category_day(Expenses* p1, uint16_t expenses_size, uint16_t month, uint16_t year, uint16_t day)
{

	uint16_t game_expense{};

	uint16_t clothes_expense{};

	uint16_t products_expense{};

	uint16_t real_state_expense{};

	for (size_t i = 0; i < expenses_size; i++)
	{
		if (p1[i].get_month() == month && p1[i].get_year() == year && p1[i].get_day() == day)
		{
			switch (p1[i].get_category())
			{
			case 1:
			{
				game_expense += p1[i].get_expenses();
				break;
			}

			case 2:
			{
				clothes_expense += p1[i].get_expenses();
				break;
			}

			case 3:
			{
				products_expense += p1[i].get_expenses();
				break;
			}

			case 4:
			{
				real_state_expense += p1[i].get_expenses();
				break;
			}
			}
		}
	}

	
	if (game_expense > clothes_expense && game_expense > products_expense && game_expense > real_state_expense)
	{
		cout << "Games : " << game_expense << endl;
	}

	if (clothes_expense > game_expense && clothes_expense > products_expense && clothes_expense > real_state_expense)
	{
		cout << "Clothets : " << clothes_expense << endl;
	}

	if (products_expense > game_expense && products_expense > clothes_expense && clothes_expense > real_state_expense)
	{
		cout << "Product : " << products_expense << endl;
	}

	if (real_state_expense > game_expense && real_state_expense > clothes_expense && real_state_expense > products_expense)
	{
		cout << "Real State : " << real_state_expense << endl;
	}

	cout << "2.";

	if (game_expense > clothes_expense && game_expense > products_expense && game_expense < real_state_expense || game_expense > clothes_expense && game_expense < products_expense && game_expense > real_state_expense || game_expense < clothes_expense && game_expense > products_expense && game_expense > real_state_expense)
	{
		cout << "Game : " << game_expense;
	}

	if (clothes_expense > game_expense && clothes_expense > products_expense && clothes_expense < real_state_expense || clothes_expense > game_expense && clothes_expense < products_expense && clothes_expense > real_state_expense || game_expense > clothes_expense && clothes_expense > products_expense && clothes_expense > real_state_expense)
	{
		cout << "Clothes : " << clothes_expense;
	}

	if (products_expense > game_expense && products_expense > clothes_expense && products_expense < real_state_expense || products_expense > game_expense && products_expense < clothes_expense && products_expense > real_state_expense || products_expense < clothes_expense && products_expense > game_expense && products_expense > real_state_expense)
	{
		cout << "Products : " << products_expense;
	}

	if (real_state_expense > game_expense && real_state_expense > clothes_expense && real_state_expense < products_expense || real_state_expense > game_expense && real_state_expense < clothes_expense && real_state_expense > products_expense || real_state_expense < clothes_expense && real_state_expense > game_expense && real_state_expense > products_expense)
	{
		cout << "Real State : " << real_state_expense;
	}

	cout << "\n3.";

	if (game_expense < clothes_expense && game_expense < products_expense && game_expense < real_state_expense)
	{
		cout << "Game : " << game_expense;
	}

	if (clothes_expense < game_expense && clothes_expense < products_expense && clothes_expense < real_state_expense)
	{
		cout << "Clothes : " << clothes_expense;
	}

	if (products_expense < game_expense && products_expense < clothes_expense && products_expense < real_state_expense)
	{
		cout << "Products : " << products_expense;
	}

	if (real_state_expense < game_expense && real_state_expense < clothes_expense && real_state_expense < products_expense)
	{
		cout << "Real State : " << real_state_expense;
	}

}

#pragma endregion 

